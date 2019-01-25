using System;

namespace Naticron.Handlers
{
	public static class Handle
	{
		public static HandlerBuilder Optional<THandler>() => new HandlerBuilder().Optional<THandler>();

		public static Repetition Repeat(Action<HandlerBuilder> pattern) => new HandlerBuilder().Repeat(pattern);

		public static HandlerBuilder Required<THandler>() => new HandlerBuilder().Required<THandler>();
	}
}