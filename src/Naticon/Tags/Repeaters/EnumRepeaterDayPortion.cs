using System;

namespace Naticron.Tags.Repeaters
{
	public class EnumRepeaterDayPortion : RepeaterDayPortion<DayPortion>
	{
		private static readonly Range AFTERNOON_RANGE = new Range(13 * 60 * 60, 17 * 60 * 60); // 1pm-5pm
		private static readonly Range AM_RANGE = new Range(0, 12 * 60 * 60); // 12am-12pm
		private static readonly Range EVENING_RANGE = new Range(17 * 60 * 60, 20 * 60 * 60); // 5pm-8pm
		private static readonly Range MORNING_RANGE = new Range(6 * 60 * 60, 12 * 60 * 60); // 6am-12pm
		private static readonly Range NIGHT_RANGE = new Range(20 * 60 * 60, 24 * 60 * 60); // 8pm-12pm
		private static readonly Range PM_RANGE = new Range(12 * 60 * 60, 24 * 60 * 60 - 1); // 12pm-12am
		private readonly Range _range;

		public EnumRepeaterDayPortion(DayPortion value)
			: base(value)
		{
			if (value == DayPortion.AM)
			{
				_range = AM_RANGE;
			}
			else if (value == DayPortion.PM)
			{
				_range = PM_RANGE;
			}
			else if (value == DayPortion.MORNING)
			{
				_range = MORNING_RANGE;
			}
			else if (value == DayPortion.AFTERNOON)
			{
				_range = AFTERNOON_RANGE;
			}
			else if (value == DayPortion.EVENING)
			{
				_range = EVENING_RANGE;
			}
			else if (value == DayPortion.NIGHT)
			{
				_range = NIGHT_RANGE;
			}
			else
			{
				throw new ArgumentException("Unknown day portion value " + value,
					"value");
			}
		}

		protected override Range GetRange(DayPortion value) => _range;

		protected override int GetWidth(Range range) => (int) range.Width;
	}
}