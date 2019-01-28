using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Naticon.Tests
{
	public class MemberDataSerializer<T> : IXunitSerializable
	{
		public MemberDataSerializer()
		{
		}

		public MemberDataSerializer(T objectToSerialize) => Object = objectToSerialize;

		public T Object { get; private set; }

		public void Deserialize(IXunitSerializationInfo info)
		{
			Object = JsonConvert.DeserializeObject<T>(info.GetValue<string>("objValue"));
		}

		public void Serialize(IXunitSerializationInfo info)
		{
			var json = JsonConvert.SerializeObject(Object);
			info.AddValue("objValue", json);
		}
	}
}