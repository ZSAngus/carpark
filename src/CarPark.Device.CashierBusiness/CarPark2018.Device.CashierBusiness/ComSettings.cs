using System;

namespace CarPark2018.Device.CashierBusiness;

[Serializable]
public class ComSettings
{
	public int BauadRate { get; set; }

	public string ComPort { get; set; }

	public string GateID { get; set; }
}
