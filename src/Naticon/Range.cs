namespace Naticron
{
	public class Range
	{
		public Range(long start, long end)
		{
			StartSecond = start;
			EndSecond = end;
		}

		public long EndSecond { get; }
		public long StartSecond { get; }

		public long Width => EndSecond - StartSecond;

		public bool Contains(long point) => StartSecond <= point && EndSecond >= point;
	}
}