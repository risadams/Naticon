using System;
using NaticonTags.Repeaters;

namespace Naticon.Tags.Repeaters
{
	public class RepeaterTime : Repeater<Tick>
	{
		private const int SecondsInHour = 60 * 60;
		private DateTime? _currentTime;

		public RepeaterTime(string value)
			: base(null)
		{
			var t = value.Replace(":", "");
			Tick tick;
			if (t.Length <= 2)
			{
				var hours = int.Parse(t);
				tick = new Tick((hours == 12 ? 0 : hours) * SecondsInHour, true);
			}
			else if (t.Length == 3)
			{
				var hoursInSeconds = int.Parse(t.Substring(0, 1)) * SecondsInHour;
				var minutesInSeconds = int.Parse(t.Substring(1)) * 60;
				tick = new Tick(hoursInSeconds + minutesInSeconds, true);
			}
			else if (t.Length == 4)
			{
				var ambiguous = value.Contains(":") &&
				                int.Parse(t.Substring(0, 1)) != 0 &&
				                int.Parse(t.Substring(0, 2)) <= 12;
				var hours = int.Parse(t.Substring(0, 2));
				var hoursInSeconds = hours * 60 * 60;
				var minutesInSeconds = int.Parse(t.Substring(2)) * 60;
				if (hours == 12)
				{
					tick = new Tick(0 * 60 * 60 + minutesInSeconds, ambiguous);
				}
				else
				{
					tick = new Tick(hoursInSeconds + minutesInSeconds, ambiguous);
				}
			}
			else if (t.Length == 5)
			{
				var hoursInSeconds = int.Parse(t.Substring(0, 1)) * 60 * 60;
				var minutesInSeconds = int.Parse(t.Substring(1, 2)) * 60;
				var seconds = int.Parse(t.Substring(3));
				tick = new Tick(hoursInSeconds + minutesInSeconds + seconds,
					true);
			}
			else if (t.Length == 6)
			{
				var ambiguous = value.Contains(":") &&
				                int.Parse(t.Substring(0, 1)) != 0 &&
				                int.Parse(t.Substring(0, 2)) <= 12;
				var hours = int.Parse(t.Substring(0, 2));
				var hoursInSeconds = hours * 60 * 60;
				var minutesInSeconds = int.Parse(t.Substring(2, 2)) * 60;
				var seconds = int.Parse(t.Substring(4, 2));
				//type = new Tick(hoursInSeconds + minutesInSeconds + seconds, ambiguous);
				if (hours == 12)
				{
					tick = new Tick(0 * 60 * 60 + minutesInSeconds + seconds,
						ambiguous);
				}
				else
				{
					tick = new Tick(
						hoursInSeconds + minutesInSeconds + seconds,
						ambiguous);
				}
			}
			else
			{
				throw new ArgumentException("Time cannot exceed six digits");
			}

			Value = tick;
		}

		public override Span GetOffset(Span span, int amount,
			Pointer.Type pointer) =>
			throw new NotImplementedException();

		public override int GetWidth() => 1;

		public override string ToString() => base.ToString() + "-time-" + Value;

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			if (pointer == Pointer.Type.None)
			{
				pointer = Pointer.Type.Future;
			}

			return NextSpan(pointer);
		}

		protected override Span NextSpan(Pointer.Type pointer)
		{
			var halfDay = RepeaterDay.DAY_SECONDS / 2;
			var fullDay = RepeaterDay.DAY_SECONDS;

			var now = Now.Value;
			var tick = Value;
			var first = false;

			if (_currentTime == null)
			{
				first = true;
				var midnight = now.Date;
				var yesterdayMidnight = midnight.AddDays(-1);
				var tomorrowMidnight = midnight.AddDays(1);

				var dstFix = (midnight - midnight.ToUniversalTime()).Seconds -
				             (tomorrowMidnight - tomorrowMidnight.ToUniversalTime()).Seconds;

				var done = false;
				DateTime[] candidateDates = null;

				if (pointer == Pointer.Type.Future)
				{
					candidateDates = tick.IsAmbiguous
						? new[]
						{
							midnight.AddSeconds(tick.ToInt32() + dstFix),
							midnight.AddSeconds(tick.ToInt32() + halfDay + dstFix),
							tomorrowMidnight.AddSeconds(tick.ToInt32())
						}
						: new[]
						{
							midnight.AddSeconds(tick.ToInt32() + dstFix),
							tomorrowMidnight.AddSeconds(tick.ToInt32())
						};

					foreach (var date in candidateDates)
					{
						if (date >= now)
						{
							_currentTime = date;
							done = true;
							break;
						}
					}
				}
				else
				{
					candidateDates = tick.IsAmbiguous
						? new[]
						{
							midnight.AddSeconds(tick.ToInt32() + halfDay + dstFix),
							midnight.AddSeconds(tick.ToInt32() + dstFix),
							yesterdayMidnight.AddSeconds(tick.ToInt32() + halfDay)
						}
						: new[]
						{
							midnight.AddSeconds(tick.ToInt32() + dstFix),
							yesterdayMidnight.AddSeconds(tick.ToInt32())
						};

					foreach (var date in candidateDates)
					{
						if (date <= now)
						{
							_currentTime = date;
							done = true;
							break;
						}
					}
				}

				if (!done && _currentTime == null)
				{
					throw new IllegalStateException(
						"Current time cannot be null at this point.");
				}
			}

			if (!first)
			{
				var increment = tick.IsAmbiguous ? halfDay : fullDay;
				var direction = (int) pointer;
				_currentTime = _currentTime.Value.AddSeconds(direction * increment);
			}

			return new Span(_currentTime.Value,
				_currentTime.Value.AddSeconds(GetWidth()));
		}
	}
}