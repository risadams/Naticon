namespace Naticon.Handlers
{
	public class HandlerTypePattern : HandlerPattern
	{
		public HandlerTypePattern(HandlerType type)
			: this(type, false)
		{
		}

		public HandlerTypePattern(HandlerType type, bool optional)
			: base(optional) =>
			Type = type;

		public HandlerType Type { get; }

		public override string ToString() => "[Handler:" + Type.GetType().Name + "]" + (IsOptional ? "?" : "");
	}
}