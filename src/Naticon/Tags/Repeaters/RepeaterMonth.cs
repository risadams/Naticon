using System;

namespace Naticron.Tags.Repeaters
{
	public class RepeaterMonth : RepeaterUnit
	{
		private static readonly int MONTH_SECONDS = 2592000; // 30 * 24 * 60 * 60

		private DateTime? _start;

		public RepeaterMonth()
			: base(UnitName.Month)
		{
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer)
		{
			var direction = pointer == Pointer.Type.Future ? 1 : -1;
			return new Span(
				span.Start.Value.AddMonths(amount * direction),
				span.End.Value.AddMonths(amount * direction)
			);
		}

		public override int GetWidth() => MONTH_SECONDS;

		public override string ToString() => base.ToString() + "-month";

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			DateTime monthStart;
			DateTime monthEnd;
			var now = Now.Value;

			if (pointer == Pointer.Type.Future)
			{
				monthStart = now.Date.AddDays(1);
				monthEnd = Time.New(now.Year, now.Month).AddMonths(1);
			}
			else if (pointer == Pointer.Type.Past)
			{
				monthStart = Time.New(now.Year, now.Month);
				monthEnd = Time.New(now.Year, now.Month).AddMonths(1);
			}
			else if (pointer == Pointer.Type.None)
			{
				monthStart = Time.New(now.Year, now.Month);
				monthEnd = Time.New(now.Year, now.Month).AddMonths(1);
			}
			else
			{
				throw new ArgumentException("Unable to handle pointer " + pointer + ".", "pointer");
			}

			return new Span(monthStart, monthEnd);
		}

		protected override Span NextSpan(Pointer.Type pointer)
		{
			var now = Now.Value;

			var direction = pointer == Pointer.Type.Future ? 1 : -1;
			if (_start == null)
			{
				_start = Time.New(now.Year, now.Month).AddMonths(direction * 1);
			}
			else
			{
				_start = _start.Value.AddMonths(direction * 1);
			}

			return new Span(_start.Value, _start.Value.AddMonths(1));
		}
	}
}