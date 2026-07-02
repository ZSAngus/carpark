using System;

namespace CarPark2018;

public class SaveChargeRecordArgsEX
{
	public int SaveChargeRecordArgsID { get; set; }

	public int TimeChargeID { get; set; }

	public int CustomFreeID { get; set; }

	public string CustomFreeRecordRemark { get; set; }

	public int CustomFreeTenatID { get; set; }

	public string FreeImagePath { get; set; }

	public DateTime InTime { get; set; }

	public bool ISTimeOut { get; set; }

	public string TicketNumber { get; set; }

	public string TransactionDataRemark { get; set; }

	public string BarCode { get; set; }
}
