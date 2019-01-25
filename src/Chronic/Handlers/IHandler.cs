using System.Collections.Generic;
using Naticron.Handlers;

namespace Naticron.Handlers
{
    public interface IHandler
    {
        Span Handle(IList<Token> tokens, Options options);
    }



}