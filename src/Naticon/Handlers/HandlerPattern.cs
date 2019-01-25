namespace Naticon.Handlers
{
	public class HandlerPattern
	{
		public HandlerPattern(bool isOptional) => IsOptional = isOptional;

		public bool IsOptional { get; }
	}
}