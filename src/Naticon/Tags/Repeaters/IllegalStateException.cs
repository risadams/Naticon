using System;

namespace Naticon.Tags.Repeaters
{
	public class IllegalStateException : Exception
	{
		public IllegalStateException(string message)
			: base(message)
		{
		}
	}
}