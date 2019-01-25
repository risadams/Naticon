namespace Naticron
{
	public class Tick
	{
		public Tick(int time, bool isAmbiguous)
		{
			Time = time;
			IsAmbiguous = isAmbiguous;
		}

		public bool IsAmbiguous { get; set; }
		public int Time { get; set; }

		public Tick Times(int multiplier) => new Tick(Time * multiplier, IsAmbiguous);

		public float ToFloat() => Time;

		public int ToInt32() => Time;

		public override string ToString() => Time + (IsAmbiguous ? "?" : "");
	}
}