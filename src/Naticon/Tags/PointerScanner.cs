using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Naticron
{
	public class PointerScanner : ITokenScanner
	{
		private static readonly PatternPointer[] Patterns =
		{
			new PatternPointer {Pattern = new Regex(@"\bin\b"), Tag = new Pointer(Pointer.Type.Future)},
			new PatternPointer {Pattern = new Regex(@"\bfuture\b"), Tag = new Pointer(Pointer.Type.Future)},
			new PatternPointer {Pattern = new Regex(@"\bpast\b"), Tag = new Pointer(Pointer.Type.Past)}
		};

		public IList<Token> Scan(IList<Token> tokens, Options options)
		{
			tokens.ForEach(ApplyTags);
			return tokens;
		}

		private void ApplyTags(Token token)
		{
			foreach (var pattern in Patterns)
			{
				if (pattern.Pattern.IsMatch(token.Value))
				{
					token.Tag(pattern.Tag);
				}
			}
		}

		public class PatternPointer
		{
			public Regex Pattern { get; set; }
			public Pointer Tag { get; set; }
		}
	}
}