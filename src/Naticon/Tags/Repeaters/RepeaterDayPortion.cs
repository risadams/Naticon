using System;
using NaticonTags.Repeaters;

namespace Naticon.Tags.Repeaters
{
	public abstract class RepeaterDayPortion<T> : Repeater<T>, IRepeaterDayPortion
	{
		private static readonly int FULL_DAY_SECONDS = 60 * 60 * 24;

		private Span _currentSpan;

		protected RepeaterDayPortion(T value)
			: base(value)
		{
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer)
		{
			Now = span.Start;
			var portionSpan = NextSpan(pointer);
			var direction = pointer == Pointer.Type.Future ? 1 : -1;
			portionSpan = portionSpan.Add(direction * (amount - 1) * RepeaterDay.DAY_SECONDS);
			return portionSpan;
		}

		public override int GetWidth()
		{
			var range = GetRange(Value);
			if (range == null)
			{
				throw new IllegalStateException("Range has not been set");
			}

			int width;
			if (_currentSpan != null)
			{
				width = _currentSpan.Width;
			}
			else
			{
				width = GetWidth(range);
			}

			return width;
		}

		public override string ToString() => base.ToString() + "-dayportion-" + Value;

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			var range = GetRange(Value);
			var rangeStart =
				Time.New(Now.Value.Year, Now.Value.Month, Now.Value.Day)
					.AddSeconds(
						range.StartSecond);
			_currentSpan = new Span(rangeStart, rangeStart.AddSeconds(range.Width));
			return _currentSpan;
		}

		protected abstract Range GetRange(T value);

		protected abstract int GetWidth(Range range);

		protected override Span NextSpan(Pointer.Type pointer)
		{
			var range = GetRange(Value);
			if (_currentSpan == null)
			{
				var now = Now.Value;
				var nowSeconds = (long) Math.Truncate(now.TimeOfDay.TotalSeconds);

				DateTime rangeStart;
				if (nowSeconds < range.StartSecond)
				{
					if (pointer == Pointer.Type.Future)
					{
						rangeStart = now.Date.AddSeconds(range.StartSecond);
					}
					else if (pointer == Pointer.Type.Past)
					{
						rangeStart = now.Date.AddDays(-1).AddSeconds(range.StartSecond);
					}
					else
					{
						throw new ArgumentException("Unable to handle pointer type " + pointer, "pointer");
					}
				}
				else if (nowSeconds > range.EndSecond)
				{
					if (pointer == Pointer.Type.Future)
					{
						rangeStart = now.Date.AddDays(1).AddSeconds(range.StartSecond);
					}
					else if (pointer == Pointer.Type.Past)
					{
						rangeStart = now.Date.AddSeconds(range.StartSecond);
					}
					else
					{
						throw new ArgumentException("Unable to handle pointer type " + pointer, "pointer");
					}
				}
				else
				{
					if (pointer == Pointer.Type.Future)
					{
						rangeStart = now.Date.AddDays(1).AddSeconds(range.StartSecond);
					}
					else if (pointer == Pointer.Type.Past)
					{
						rangeStart = now.Date.AddSeconds(range.StartSecond);
					}
					else
					{
						throw new ArgumentException("Unable to handle pointer type " + pointer, "pointer");
					}
				}

				_currentSpan = new Span(rangeStart, rangeStart.AddSeconds(range.Width));
			}
			else
			{
				if (pointer == Pointer.Type.Future)
				{
					_currentSpan = _currentSpan.Add(FULL_DAY_SECONDS);
				}
				else if (pointer == Pointer.Type.Past)
				{
					_currentSpan = _currentSpan.Add(-FULL_DAY_SECONDS);
				}
				else
				{
					throw new ArgumentException("Unable to handle pointer type " + pointer, "pointer");
				}
			}

			return _currentSpan;
		}
	}

	public interface IRepeaterDayPortion : ITag
	{
	}
}