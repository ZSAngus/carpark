using System;
using CarPark.Core;

namespace CarPark.Device;

public class FeeInfo : TicketInfo
{
	private bool _NeedPrint;

	public decimal Fee { get; set; }

	public DateTime FeeTime { get; set; }

	public bool NeedPrint
	{
		get
		{
			return _NeedPrint;
		}
		set
		{
			_NeedPrint = value;
		}
	}

	public EnumTicketAction TicketAction { get; set; }

	public EnumTicketType TicketType { get; set; }

	public string LPRSNumber { get; set; }

	public string FieldStr { get; set; }

	public FeeInfo()
	{
		_NeedPrint = true;
	}

	public string GetPrintData()
	{
		string empty = string.Empty;
		return TicketType switch
		{
			EnumTicketType.Compensation => string.Concat(new object[4]
			{
				base.ParkTypeStr + " ",
				base.TicketNumber,
				" ",
				base.InTime.ToString("MMdd HH:mm")
			}), 
			EnumTicketType.CompensationFee => "                   *" + FeeTime.ToString("MMdd HH:mm") + " $" + Fee, 
			EnumTicketType.TempMonthTicket => string.Format("    ({0}){1} {2} {3} {4}", (base.ParkType == EnumParkType.MCycle) ? "M" : "P", (int)base.TempMonthType, base.TicketNumber, base.StartTime.ToString("yy/MM/dd"), base.EndTime.ToString("yy/MM/dd")), 
			_ => "     " + base.TicketNumber + " " + FeeTime.ToString("yyyy/MM/dd HH:mm:ss") + " $" + Fee, 
		};
	}

	public string GetWriteData()
	{
		string empty = string.Empty;
		return TicketType switch
		{
			EnumTicketType.Compensation => base.CarParkSerialNo + base.ParkTypeStr + base.TicketNumber + "1" + base.InTime.ToString("yyMMddHHmmss") + base.InTime.ToString("yyMMddHHmmss") + "000", 
			EnumTicketType.TempMonthTicket => base.CarParkSerialNo + 1 + base.TicketNumber + base.StartTime.ToString("yyMMddHH") + base.EndTime.ToString("yyMMddHH"), 
			_ => base.CarParkSerialNo + base.ParkTypeStr + base.TicketNumber + "0" + base.InTime.ToString("yyMMddHHmmss") + FeeTime.ToString("yyMMddHHmmss") + "1", 
		};
	}
}
