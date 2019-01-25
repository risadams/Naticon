using System;

namespace Naticron.Tags.Repeaters
{
	public class RepeaterWeek : RepeaterUnit
	{
		public static readonly int WEEK_DAYS = 7;
		public static readonly int WEEK_SECONDS = 604800; // (7 * 24 * 60 * 60);
		private readonly Options _options;

		private DateTime? _start;

		public RepeaterWeek(Options options)
			: base(UnitName.Week)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}

			_options = options;
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer)
		{
			var direction = pointer == Pointer.Type.Future ? 1 : -1;
			return span.Add(direction * amount * WEEK_SECONDS);
		}

		public override int GetWidth() => WEEK_SECONDS;

		public override string ToString() => base.ToString() + "-week";

		protected override Span CurrentSpan(Pointer.Type pointer)
		{
			Span thisWeekSpan;
			DateTime thisWeekStart;
			DateTime thisWeekEnd;
			var now = Now.Value;

			if (pointer == Pointer.Type.Future)
			{
				thisWeekStart = Time.New(now, now.Hour).AddHours(1);
				var sundayRepeater = new RepeaterDayName(GetStartOfWeek());
				sundayRepeater.Now = now;
				var thisSundaySpan = sundayRepeater.GetCurrentSpan(Pointer.Type.Future);
				thisWeekEnd = thisSundaySpan.Start.Value;
				thisWeekSpan = new Span(thisWeekStart, thisWeekEnd);
			}
			else if (pointer == Pointer.Type.Past)
			{
				thisWeekEnd = now;
				var sundayRepeater = new RepeaterDayName(GetStartOfWeek());
				sundayRepeater.Now = now;
				var lastSundaySpan = sundayRepeater.GetNextSpan(Pointer.Type.Past);
				thisWeekStart = lastSundaySpan.Start.Value;
				thisWeekSpan = new Span(thisWeekStart, thisWeekEnd);
			}
			else if (pointer == Pointer.Type.None)
			{
				var sundayRepeater = new RepeaterDayName(GetStartOfWeek());
				sundayRepeater.Now = now;
				var lastSundaySpan = sundayRepeater.GetNextSpan(Pointer.Type.Past);
				thisWeekStart = lastSundaySpan.Start.Value;
				thisWeekEnd = thisWeekStart.AddDays(WEEK_DAYS);
				thisWeekSpan = new Span(thisWeekStart, thisWeekEnd);
			}
			else
			{
				throw new ArgumentException("Unable to handle pointer " + pointer + ".", "pointer");
			}

			return thisWeekSpan;
		}

		protected override Span NextSpan(Pointer.Type pointer)
		{
			if (_start == null)
			{
				if (pointer == Pointer.Type.Future)
				{
					var sundayRepeater = new RepeaterDayName(GetStartOfWeek());
					sundayRepeater.Now = Now;
					var nextSundaySpan = sundayRepeater
						.GetNextSpan(Pointer.Type.Future);
					_start = nextSundaySpan.Start.Value;
				}
				else if (pointer == Pointer.Type.Past)
				{
					var sundayRepeater = new RepeaterDayName(GetStartOfWeek());
					sundayRepeater.Now = Now.Value.AddDays(1);
					sundayRepeater.GetNextSpan(Pointer.Type.Past);
					var lastSundaySpan = sundayRepeater
						.GetNextSpan(Pointer.Type.Past);
					_start = lastSundaySpan.Start.Value;
				}
				else
				{
					throw new ArgumentException(
						"Unable to handle pointer " + pointer + ".",
						"pointer");
				}
			}
			else
			{
				var direction = pointer == Pointer.Type.Future ? 1 : -1;
				_start.Value.AddDays(WEEK_DAYS * direction);
			}

			return new Span(_start.Value, _start.Value.AddDays(WEEK_DAYS));
		}

		private DayOfWeek GetStartOfWeek() => _options.FirstDayOfWeek;
	}
}