using System;

namespace Naticron
{
	public abstract class Tag<T> : ITag
	{
		protected Tag(T value) => Value = value;

		public T Value { get; protected set; }
		public DateTime? Now { get; set; }

		public object RawValue => Value;
	}

	public interface IRepeater : ITag
	{
		Span GetCurrentSpan(Pointer.Type pointer);

		Span GetNextSpan(Pointer.Type pointer);

		Span GetOffset(Span span, int amount, Pointer.Type pointer);

		/// <summary>Returns the width (in seconds or months) of this repeatable.</summary>
		int GetWidth();
	}

	public interface ITag
	{
		DateTime? Now { get; set; }
		object RawValue { get; }
	}
}