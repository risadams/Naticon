using System.Collections.Generic;

namespace Naticron.Handlers
{
	public class SmSdHandler : IHandler
	{
		public Span Handle(IList<Token> tokens, Options options)
		{
			var month = tokens[0].GetTag<ScalarMonth>().Value;
			var day = tokens[1].GetTag<ScalarDay>().Value;
			var now = options.Clock();
			var start = Time.New(now.Year, month, day);
			var end = start.AddMonths(1);
			return new Span(start, end);
		}
	}
}