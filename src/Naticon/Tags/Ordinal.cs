namespace Naticon
{
	public class OrdinalDay : Ordinal
	{
		public OrdinalDay(int value) : base(value)
		{
		}

		public override string ToString() => base.ToString() + "-day-" + Value;
	}

	public class Ordinal : Tag<int>
	{
		public Ordinal(int value)
			: base(value)
		{
		}

		public override string ToString() => "ordinal";
	}
}