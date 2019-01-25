using System;

namespace Naticron.Tags.Repeaters
{
	public class RepeaterSecond : RepeaterUnit
	{
		private const int SECOND_SECONDS = 1; // (60 * 60);

		private DateTime? _start;

		public RepeaterSecond()
			: base(UnitName.Second)
		{
		}

		public override Span GetOffset(Span span, int amount,
			Pointer.Type pointer)
		{
			var direction = pointer == Pointer.Type.Future ? 1 : -1;
			// WARN: Does not use Calendar
			return span.Add(direction * amount * SECOND_SECONDS);
		}

		public override int GetWidth() => SECOND_SECONDS;

		public override string ToString() => base.ToString() + "-second";

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			var now = Now.Value;
			return new Span(now, now.AddSeconds(1));
		}

		protected override Span NextSpan(Pointer.Type pointer)
		{
			var now = Now.Value;
			var direction = pointer == Pointer.Type.Future ? 1 : -1;
			if (_start == null)
			{
				_start = now.AddSeconds(direction * 1);
			}
			else
			{
				_start = _start.Value.AddSeconds(direction * 1);
			}

			return new Span(_start.Value, _start.Value.AddSeconds(1));
		}
	}
}