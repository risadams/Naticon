namespace Naticron.Tags.Repeaters
{
	public abstract class Repeater<T> : Tag<T>, IRepeater
	{
		protected Repeater(T type)
			: base(type)
		{
		}

		public Span GetCurrentSpan(Pointer.Type pointer)
		{
			if (Now == null)
			{
				throw new IllegalStateException("StartSecond point must be set before calling #this");
			}

			return CurrentSpan(pointer);
		}

		/// <summary>Returns the next occurance of this repeatable.</summary>
		public Span GetNextSpan(Pointer.Type pointer)
		{
			if (Now == null)
			{
				throw new IllegalStateException("StartSecond point must be set before calling #next");
			}

			var span = NextSpan(pointer);
			return span;
		}

		public abstract Span GetOffset(Span span, int amount, Pointer.Type pointer);

		public abstract int GetWidth();

		public override string ToString() => "repeater";

		protected abstract Span CurrentSpan(Pointer.Type pointer);

		protected abstract Span NextSpan(Pointer.Type pointer);
	}
}