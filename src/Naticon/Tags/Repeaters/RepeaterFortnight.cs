using System;

namespace Naticron.Tags.Repeaters
{
	public class RepeaterFortnight : RepeaterUnit
	{
		public RepeaterFortnight()
			: base(UnitName.Fortnight)
		{
		}

		public override Span GetOffset(Span span, int amount, Pointer.Type pointer) => throw new NotImplementedException();

		public override int GetWidth() => throw new NotImplementedException();

		protected override Span CurrentSpan(Pointer.Type pointer) => throw new NotImplementedException();

		protected override Span NextSpan(Pointer.Type pointer) => throw new NotImplementedException();
	}
}