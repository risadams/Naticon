using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Naticon
{
	public class SeparatorScanner : ITokenScanner
	{
		private static readonly PatternSeparator[] Patterns =
		{
			new PatternSeparator {Pattern = @"^,$".Compile(), Tag = new SeparatorComma()},
			new PatternSeparator {Pattern = @"^and$".Compile(), Tag = new SeparatorComma()},
			new PatternSeparator {Pattern = @"^(at|@)$".Compile(), Tag = new SeparatorAt()},
			new PatternSeparator {Pattern = @"^in$".Compile(), Tag = new SeparatorIn()},
			new PatternSeparator {Pattern = @"^/$".Compile(), Tag = new SeparatorDate(Separator.Type.Slash)},
			new PatternSeparator {Pattern = @"^-$".Compile(), Tag = new SeparatorDate(Separator.Type.Dash)},
			new PatternSeparator {Pattern = @"^on$".Compile(), Tag = new SeparatorOn()}
		};

		public IList<Token> Scan(IList<Token> tokens, Options options)
		{
			tokens.ForEach(ApplyTags);
			return tokens;
		}

		private static void ApplyTags(Token token)
		{
			foreach (var pattern in Patterns)
			{
				if (pattern.Pattern.IsMatch(token.Value))
				{
					token.Tag(pattern.Tag);
				}
			}
		}

		public class PatternSeparator
		{
			public Regex Pattern { get; set; }
			public Separator Tag { get; set; }
		}
	}
}