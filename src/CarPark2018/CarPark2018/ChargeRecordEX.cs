using System;

namespace CarPark2018;

public class ChargeRecordEX
{
	public int BillType { get; set; }

	public int? BufferTime { get; set; }

	public string CardCode { get; set; }

	public int ChargeMin { get; set; }

	public DateTime ChargeTime { get; set; }

	public string Currency { get; set; }

	public decimal? Fine { get; set; }

	public int? FirstTime { get; set; }

	public decimal FreeCharge { get; set; }

	public int FreeMin { get; set; }

	public string FromStation { get; set; }

	public bool IsDelete { get; set; }

	public int ParkMin { get; set; }

	public TimeSpan ParkTimeSpan { get; set; }

	public int ParkTypeID { get; set; }

	public string PeriodofTime { get; set; }

	public string Remark { get; set; }

	public int ShiftID { get; set; }

	public string StaffCode { get; set; }

	public int TimeChargeID { get; set; }

	public decimal TotalCharge { get; set; }

	public int? TransactionID { get; set; }
}
