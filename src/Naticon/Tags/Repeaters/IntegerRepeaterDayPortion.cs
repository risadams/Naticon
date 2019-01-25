namespace Naticon.Tags.Repeaters
{
	public class IntegerRepeaterDayPortion : RepeaterDayPortion<int>
	{
		private const int SecondsInHour = 60 * 60;
		private readonly Range _range;

		public IntegerRepeaterDayPortion(int value)
			: base(value) =>
			_range = new Range(value * SecondsInHour, (value + 12) * SecondsInHour);

		protected override Range GetRange(int value) => _range;

		protected override int GetWidth(Range range) => 12 * SecondsInHour;
	}
}