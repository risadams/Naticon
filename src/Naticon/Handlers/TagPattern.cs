using System;

namespace Naticon.Handlers
{
	public class TagPattern : HandlerPattern
	{
		public TagPattern(Type tagType)
			: this(tagType, false)
		{
		}

		public TagPattern(Type tagType, bool isOptional)
			: base(isOptional) =>
			TagType = tagType;

		public Type TagType { get; }

		public override string ToString() => "[Tag:" + TagType.Name + (IsOptional ? "-?" : "") + "]";
	}
}