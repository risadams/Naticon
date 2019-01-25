using System;

namespace Naticon
{
	public class Span
	{
		public DateTime? End;
		public DateTime? Start;

		public Span(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
		}

		public int Width => (int) Math.Truncate((End.Value - Start.Value).TotalSeconds);

		public Span Add(long seconds) =>
			new Span(Start.Value.AddSeconds(seconds),
				End.Value.AddSeconds(seconds));

		public bool Contains(DateTime? value) => Start <= value && value <= End;

		public Span Subtract(long seconds) => Add(-seconds);

		public override string ToString() => $"({Start} - {End})";

		public DateTime ToTime() => Width > 1 ? Start.Value.AddSeconds((double) Width / 2) : Start.Value;
	}
}