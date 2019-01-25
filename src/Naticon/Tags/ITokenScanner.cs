using System.Collections.Generic;

namespace Naticon
{
	public interface ITokenScanner
	{
		IList<Token> Scan(IList<Token> tokens, Options options);
	}
}