using System;
using System.Globalization;

namespace Naticon.Tests
{
	internal static class When
	{
		public static ISetParsingTestContext ItIs(DateTime date) => new ParsingTestContext(date);

		public static ISetParsingTestContext ItIs(string date) =>
			new ParsingTestContext(
				DateTime.Parse(date,
					CultureInfo.InvariantCulture));
	}

	internal interface ISetParsingTestContext
	{
		ISetParsingTestContext Parsing(string phrase);
		Span ReturnsSpan();
		ISetParsingTestContext WithOptions(object options);
	}

	internal class ParsingTestContext : ISetParsingTestContext
	{
		public ParsingTestContext() => Options = new Options();

		public ParsingTestContext(DateTime now)
			: this()
		{
			Now = () => now;
			Options.Clock = Now;
		}

		public ParsingTestContext(Func<DateTime> getNow)
			: this()
		{
			Now = getNow;
			Options.Clock = Now;
		}

		public Func<DateTime> Now { get; }
		public Options Options { get; private set; }
		public string Phrase { get; private set; }
		public Span Result { get; private set; }

		public ISetParsingTestContext Parsing(string phrase)
		{
			Phrase = phrase;
			return this;
		}

		public Span ReturnsSpan()
		{
			Options = Options ?? new Options {Clock = Now};
			Options.Clock = Options.Clock ?? Now;
			Parser.IsDebugMode = true;
			var parser = new Parser(Options);
			Result = parser.Parse(Phrase);
			return Result;
		}

		public ISetParsingTestContext WithOptions(object options)
		{
			Options = Options.Extend(options);
			return this;
		}
	}
}