using Naticon;

namespace NaticonTests.Utils
{
	internal class DummyTag1 : Tag<string>
	{
		public DummyTag1(string value) : base(value)
		{
		}
	}

	internal class DummyTag2 : Tag<string>
	{
		public DummyTag2(string value)
			: base(value)
		{
		}
	}
}