using System;

namespace CarPark.Device;

public class RecordContrastInfo
{
	public int GateID { get; set; }

	public string OnlyID { get; set; }

	public string GuID { get; set; }

	public string Registration { get; set; }

	public string ImagePath { get; set; }

	public string CurrResult { get; set; }

	public DateTime CallTimestamp { get; set; }

	public int ShowTime { get; set; }

	public string StaffCode { get; set; }

	public bool IsPass { get; set; }
}
