namespace Naticon
{
	public class Separator : Tag<Separator.Type>
	{
		public enum Type
		{
			Comma,
			Dash,
			Slash,
			At,
			NewLine,
			In,
			On
		}

		public Separator(Type value)
			: base(value)
		{
		}
	}
}