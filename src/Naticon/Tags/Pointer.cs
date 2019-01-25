namespace Naticron
{
	public class Pointer : Tag<Pointer.Type>
	{
		public enum Type
		{
			Past = -1,
			Future = 1,
			None = 0
		}

		public Pointer(Type value) : base(value)
		{
		}
	}
}