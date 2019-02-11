using System;
using Socha3.BlinkStick;

namespace TurnOff
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Turn Off.\r\n");

			BlinkStick device = BlinkStick.FindFirst ();

			if (device != null) {
				if (device.OpenDevice ()) {
					device.TurnOff ();
					Console.WriteLine ("BlinkStick was turned off");
				} else {
					Console.WriteLine ("Could not open the device");
				}
			} else {
				Console.WriteLine ("BlinkStick not found");
			}
		}
	}
}
