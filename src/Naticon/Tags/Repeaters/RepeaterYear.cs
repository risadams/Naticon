using System;
using NaticonTags.Repeaters;

namespace Naticon.Tags.Repeaters
{
	public class RepeaterYear : RepeaterUnit
	{
		private DateTime? _start;

		public RepeaterYear()
			: base(UnitName.Year)
		{
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer)
		{
			var direction = (int) pointer;
			var newBegin = BuildOffsetTime(span.Start.Value, amount, direction);
			var newEnd = BuildOffsetTime(span.End.Value, amount, direction);
			return new Span(newBegin, newEnd);
		}

		public override int GetWidth() => 365 * 24 * 60 * 60;

		public override string ToString() => base.ToString() + "-year";

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			DateTime yearStart;
			DateTime yearEnd;
			var now = Now.Value;
			if (pointer == Pointer.Type.Future)
			{
				yearStart = now.Date.AddDays(1);
				yearEnd = Time.New(now.Date.Year + 1);
			}
			else if (pointer == Pointer.Type.Past)
			{
				yearStart = Time.New(now.Date.Year);
				yearEnd = now.Date;
			}
			else if (pointer == Pointer.Type.None)
			{
				yearStart = Time.New(now.Date.Year);
				yearEnd = Time.New(now.Date.Year + 1);
			}
			else
			{
				throw new ArgumentException("Unable to handle pointer " + pointer + ".", "pointer");
			}

			return new Span(yearStart, yearEnd);
		}

		protected override Span NextSpan(Pointer.Type pointer)
		{
			if (_start == null)
			{
				var now = Now.Value;
				if (pointer == Pointer.Type.Future)
				{
					_start = Time.New(now.Date.Year + 1);
				}
				else if (pointer == Pointer.Type.Past)
				{
					_start = Time.New(now.Date.Year - 1);
				}
				else
				{
					throw new ArgumentException("Unable to handle pointer " + pointer + ".", "pointer");
				}
			}
			else
			{
				var direction = (int) pointer;
				_start = _start.Value.AddYears(direction * 1);
			}

			return new Span(_start.Value, _start.Value.AddYears(1));
		}

		private DateTime BuildOffsetTime(DateTime time, int amount, int direction)
		{
			var year = time.Year + amount * direction;
			var days = Time.DaysInMonth(year, time.Month);
			var day = time.Day > days ? days : time.Day;
			return Time.New(year,
				time.Month,
				day,
				time.Hour,
				time.Minute,
				time.Second);
		}
	}
}