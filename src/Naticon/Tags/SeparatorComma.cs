namespace Naticron
{
	public class SeparatorComma : Separator
	{
		public SeparatorComma() : base(Type.Comma)
		{
		}
	}

	public class SeparatorAt : Separator
	{
		public SeparatorAt() : base(Type.At)
		{
		}
	}

	public class SeparatorIn : Separator
	{
		public SeparatorIn() : base(Type.In)
		{
		}
	}

	public class SeparatorDate : Separator
	{
		public SeparatorDate(Type value) : base(value)
		{
		}
	}

	public class SeparatorOn : Separator
	{
		public SeparatorOn() : base(Type.On)
		{
		}
	}
}