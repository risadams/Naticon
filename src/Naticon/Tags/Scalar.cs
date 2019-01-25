namespace Naticron
{
	public class Scalar : Tag<int>
	{
		public Scalar(int value)
			: base(value)
		{
		}

		public override string ToString() => "scalar";
	}

	public class ScalarDay : Scalar
	{
		public ScalarDay(int value)
			: base(value)
		{
		}

		public override string ToString() => base.ToString() + "-day-" + Value;
	}

	public class ScalarMonth : Scalar
	{
		public ScalarMonth(int value)
			: base(value)
		{
		}

		public override string ToString() => base.ToString() + "-month-" + Value;
	}

	public class ScalarYear : Scalar
	{
		public ScalarYear(int value)
			: base(value)
		{
		}

		public override string ToString() => base.ToString() + "-year-" + Value;
	}
}