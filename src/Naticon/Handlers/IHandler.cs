using System.Collections.Generic;

namespace Naticron.Handlers
{
	public interface IHandler
	{
		Span Handle(IList<Token> tokens, Options options);
	}
}