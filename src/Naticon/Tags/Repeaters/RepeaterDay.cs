using System;

namespace Naticon.Tags.Repeaters
{
	public class RepeaterDay : RepeaterUnitName
	{
		public static readonly int DAY_SECONDS = 24 * 60 * 60;

		private DateTime? _currentDayStart;

		public RepeaterDay()
			: base(UnitName.Day)
		{
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer)
		{
			var direction = (int) pointer;
			return span.Add(direction * amount * DAY_SECONDS);
		}

		public override int GetWidth() => DAY_SECONDS;

		public override string ToString() => base.ToString() + "-day";

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			DateTime dayBegin;
			DateTime dayEnd;
			if (pointer == Pointer.Type.Future)
			{
				dayBegin = Time.New(Now.Value.Date, Now.Value.Hour);
				dayEnd = Now.Value.Date.AddDays(1);
			}
			else if (pointer == Pointer.Type.Past)
			{
				dayBegin = Now.Value.Date;
				dayEnd = Time.New(Now.Value.Date, Now.Value.Hour);
			}
			else if (pointer == Pointer.Type.None)
			{
				dayBegin = Now.Value.Date;
				dayEnd = Now.Value.Date.AddDays(1);
			}
			else
			{
				throw new ArgumentException("Unable to handle pointer " + pointer + ".", "pointer");
			}

			return new Span(dayBegin, dayEnd);
		}

		protected override Span NextSpan(Pointer.Type pointer)
		{
			if (_currentDayStart == null)
			{
				_currentDayStart = Now.Value.Date;
			}

			var direction = (int) pointer;
			_currentDayStart = _currentDayStart.Value.AddDays(direction);

			return new Span(_currentDayStart.Value, _currentDayStart.Value.AddDays(1));
		}
	}
}