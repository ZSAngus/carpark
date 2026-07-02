using System;

namespace CarPark.Device;

public class ExitContrastInfo
{
	public int GateID { get; set; }

	public string OnlyID { get; set; }

	public string GuID { get; set; }

	public string EnterImagePath { get; set; }

	public string EnterValue { get; set; }

	public string ExitImagePath { get; set; }

	public string ExitValue { get; set; }

	public DateTime CallTimestamp { get; set; }

	public int ShowTime { get; set; }

	public string StaffCode { get; set; }

	public bool IsPass { get; set; }
}
