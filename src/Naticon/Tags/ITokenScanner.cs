using System.Collections.Generic;

namespace Naticron
{
	public interface ITokenScanner
	{
		IList<Token> Scan(IList<Token> tokens, Options options);
	}
}