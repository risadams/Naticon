namespace Naticron.Tags
{
	public class TimeZone : Tag<string>
	{
		public TimeZone(string value) : base(value)
		{
		}

		public override string ToString() => "timezone";
	}
}