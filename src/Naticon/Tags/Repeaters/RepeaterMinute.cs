using System;
using NaticonTags.Repeaters;

namespace Naticon.Tags.Repeaters
{
	public class RepeaterMinute : RepeaterUnit
	{
		public static readonly int MINUTE_SECONDS = 60;

		private DateTime? _start;

		public RepeaterMinute()
			: base(UnitName.Minute)
		{
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer)
		{
			var direction = pointer == Pointer.Type.Future ? 1 : -1;
			return span.Add(direction * amount * MINUTE_SECONDS);
		}

		public override int GetWidth() => MINUTE_SECONDS;

		public override string ToString() => base.ToString() + "-minute";

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			var now = Now.Value;
			DateTime minuteBegin;
			DateTime minuteEnd;
			if (pointer == Pointer.Type.Future)
			{
				minuteBegin = now;
				minuteEnd = Time.New(now, now.Hour, now.Minute);
			}
			else if (pointer == Pointer.Type.Past)
			{
				minuteBegin = Time.New(now, now.Hour, now.Minute);
				minuteEnd = now;
			}
			else if (pointer == Pointer.Type.None)
			{
				minuteBegin = Time.New(now, now.Hour, now.Minute);
				minuteEnd = Time.New(now, now.Hour, now.Minute).AddSeconds(MINUTE_SECONDS);
			}
			else
			{
				throw new ArgumentException("Unable to handle pointer " + pointer + ".");
			}

			return new Span(minuteBegin, minuteEnd);
		}

		protected override Span NextSpan(Pointer.Type pointer)
		{
			var now = Now.Value;

			if (_start == null)
			{
				if (pointer == Pointer.Type.Future)
				{
					_start = Time.New(now, now.Hour, now.Minute).AddMinutes(1);
				}
				else if (pointer == Pointer.Type.Past)
				{
					_start = Time.New(now, now.Hour, now.Minute).AddMinutes(-1);
				}
				else
				{
					throw new ArgumentException("Unable to handle pointer " + pointer + ".");
				}
			}
			else
			{
				var direction = pointer == Pointer.Type.Future ? 1 : -1;
				_start.Value.AddMinutes(direction);
			}

			return new Span(_start.Value, _start.Value.AddSeconds(MINUTE_SECONDS));
		}
	}
}