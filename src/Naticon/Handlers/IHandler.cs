using System.Collections.Generic;

namespace Naticon.Handlers
{
	public interface IHandler
	{
		Span Handle(IList<Token> tokens, Options options);
	}
}