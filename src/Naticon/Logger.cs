using System;
using System.Diagnostics;

namespace Naticon
{
	internal static class Logger
	{
		public static void Log(Func<string> message)
		{
			if (Parser.IsDebugMode)
			{
				Debug.WriteLine(message());
			}
		}
	}
}