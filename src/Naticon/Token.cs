using System;
using System.Collections.Generic;
using System.Linq;

namespace Naticron
{
	public class Token
	{
		private readonly List<ITag> _tags = new List<ITag>();

		public Token(string value) => Value = value;

		public string Value { get; }

		public T GetTag<T>()
			where T : ITag
		{
			return (T) _tags.FirstOrDefault(tag => tag is T);
		}

		public bool HasTags() => _tags.Count > 0;

		public bool IsNotTaggedAs<T>()
			where T : ITag =>
			GetTag<T>() == null;

		public bool IsTaggedAs<T>()
			where T : ITag =>
			GetTag<T>() != null;

		public void Tag(ITag tag)
		{
			_tags.Add(tag);
		}

		public void Tag(params ITag[] tags)
		{
			tags.ForEach(tag => _tags.Add(tag));
		}

		public void Tag(IEnumerable<ITag> tags)
		{
			tags.ForEach(tag => _tags.Add(tag));
		}

		public override string ToString()
		{
			return string.Format("{0} [{1}]", Value, string.Join(",", _tags.Select(tag => tag.ToString())));
		}

		public void Untag<T>()
			where T : ITag
		{
			_tags.RemoveAll(tag => tag is T);
		}

		internal bool IsTaggedAs(Type type)
		{
			return _tags.Any(tag => type.IsAssignableFrom(tag.GetType()));
		}
	}
}