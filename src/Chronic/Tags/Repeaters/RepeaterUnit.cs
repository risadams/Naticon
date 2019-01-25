using Naticron.Tags.Repeaters;

namespace Naticron.Tags.Repeaters
{
    public abstract class RepeaterUnit : Repeater<UnitName>
    {
        protected RepeaterUnit(UnitName type)
            : base(type)
        {
        }
    }
}
