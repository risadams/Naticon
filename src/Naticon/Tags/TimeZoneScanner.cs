using Naticron.Tags;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Naticron
{
	public class TimeZoneScanner : ITokenScanner
	{
		private static readonly Regex[] Patterns =
		{
			@"[PMCE][DS]T|UTC".Compile(),
			@"(tzminus)?\d{2}:?\d{2}".Compile()
		};

		public IList<Token> Scan(IList<Token> tokens, Options options)
		{
			tokens.ForEach(ApplyTags);
			return tokens;
		}

		public override string ToString() => "timezone";

		private static void ApplyTags(Token token)
		{
			Patterns.ForEach(pattern =>
			{
				var match = pattern.Match(token.Value);
				if (match.Success)
				{
					token.Tag(new TimeZone(match.Value));
				}
			});
		}
	}
}