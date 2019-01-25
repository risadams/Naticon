using System;
using NaticonTags.Repeaters;

namespace Naticon.Tags.Repeaters
{
	public class RepeaterSeason : RepeaterUnit
	{
		public static readonly int SEASON_SECONDS = 7862400; // (91 * 24 * 60 * 60);

		public RepeaterSeason() : base(UnitName.Season)
		{
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer) => throw new NotImplementedException();

		public override int GetWidth() => SEASON_SECONDS;

		public override string ToString() => base.ToString() + "-season";

		protected override Span CurrentSpan(Pointer.Type pointer) => throw new NotImplementedException();

		protected override Span NextSpan(Pointer.Type pointer) => throw new IllegalStateException("Not implemented.");
	}
}