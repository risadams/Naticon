using System;
using System.Collections.Generic;
using System.Linq;

namespace Naticon.Handlers
{
	public class RepeatPattern : HandlerPattern
	{
		public const int Inifinite = -1;
		private readonly int _numberOfTimesToRepeat;
		private readonly HandlerPattern[] _pattern;

		public RepeatPattern(IEnumerable<HandlerPattern> pattern, int numberOfTimesToRepeat)
			: base(false)
		{
			_pattern = pattern.ToArray();
			_numberOfTimesToRepeat = numberOfTimesToRepeat;
		}

		public bool IsInfinite => _numberOfTimesToRepeat == Inifinite;

		public bool Match(IList<Token> tokens, out int advancement)
		{
			advancement = 0;
			var tokenIndex = 0;
			var iterations = 0;

			Func<bool> shouldIterate = () =>
				IsInfinite ||
				iterations < _numberOfTimesToRepeat;

			while (shouldIterate())
			{
				foreach (var pattern in _pattern)
				{
					var isOptional = pattern.IsOptional;
					var isRequired = !isOptional;

					if (pattern is TagPattern)
					{
						var match = tokenIndex < tokens.Count &&
						            tokens[tokenIndex].IsTaggedAs((pattern as TagPattern).TagType);
						if (match == false && isRequired)
						{
							advancement = tokenIndex;
							return iterations > 0;
						}

						if (match)
						{
							tokenIndex++;
						}
					}
					else
					{
						break;
					}
				}

				iterations += 1;
			}

			advancement = tokenIndex;
			return true;
		}

		public override string ToString() => string.Format("[RepeatPattern: [{0}]{1}]", _pattern, IsOptional ? "-?" : "");
	}
}