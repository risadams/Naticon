using Naticon.Tags.Repeaters;

namespace NaticonTags.Repeaters
{
	public abstract class RepeaterUnit : Repeater<UnitName>
	{
		protected RepeaterUnit(UnitName type)
			: base(type)
		{
		}
	}
}