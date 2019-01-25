using System.Collections.Generic;

namespace Naticron
{
	public class GrabberScanner : ITokenScanner
	{
		private static readonly PatternGrabber[] _matches =
		{
			new PatternGrabber {Pattern = "last", Tag = new Grabber(Grabber.Type.Last)},
			new PatternGrabber {Pattern = "next", Tag = new Grabber(Grabber.Type.Next)},
			new PatternGrabber {Pattern = "this", Tag = new Grabber(Grabber.Type.This)}
		};

		public IList<Token> Scan(IList<Token> tokens, Options options)
		{
			tokens.ForEach(ApplyGrabberTags);
			return tokens;
		}

		private static void ApplyGrabberTags(Token token)
		{
			foreach (var match in _matches)
			{
				if (match.Pattern == token.Value)
				{
					token.Tag(match.Tag);
				}
			}
		}

		public class PatternGrabber
		{
			public string Pattern { get; set; }
			public Grabber Tag { get; set; }
		}
	}
}