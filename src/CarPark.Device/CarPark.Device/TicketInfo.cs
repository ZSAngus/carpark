using System;
using CarPark.Core;

namespace CarPark.Device;

public class TicketInfo
{
	private string _ParkTypeStr;

	public string CarParkSerialNo { get; set; }

	public DateTime InTime { get; set; }

	public bool IsEmptyOrInValid => TicketNumber == "UNKNOW";

	public EnumParkType ParkType
	{
		get
		{
			return (EnumParkType)(int.Parse(_ParkTypeStr) - 5);
		}
		set
		{
			switch (value)
			{
			case EnumParkType.Private:
			case EnumParkType.MCycle:
			case EnumParkType.Van:
				_ParkTypeStr = ((int)(value + 5)).ToString();
				break;
			default:
				throw new NotSupportedException();
			}
		}
	}

	public string ParkTypeStr
	{
		get
		{
			return _ParkTypeStr;
		}
		set
		{
			_ParkTypeStr = value;
		}
	}

	public string TicketNumber { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime EndTime { get; set; }

	public EnumCardType CardType { get; set; }

	public EnumTempMonthType TempMonthType { get; set; }

	public TicketInfo()
	{
		_ParkTypeStr = "6";
	}

	public static TicketInfo FromString(string Barcode)
	{
		TicketInfo ticketInfo = new TicketInfo();
		if (Barcode.Length < 10)
		{
			ticketInfo.TicketNumber = "UNKNOW";
			return ticketInfo;
		}
		if (Barcode.Length >= 28 && Barcode.Length <= 30)
		{
			ticketInfo.CardType = EnumCardType.TempMonthCard;
			ticketInfo.CarParkSerialNo = Barcode.Substring(0, 4);
			ticketInfo.TicketNumber = Barcode.Substring(5, 7);
			ticketInfo.TempMonthType = (EnumTempMonthType)int.Parse(Barcode.Substring(4, 1));
			ticketInfo.StartTime = DateTime.ParseExact(Barcode.Substring(12, 8), "yyMMddHH", null);
			ticketInfo.EndTime = DateTime.ParseExact(Barcode.Substring(20, 8), "yyMMddHH", null);
		}
		else
		{
			ticketInfo.CardType = EnumCardType.Ticket;
			ticketInfo.CarParkSerialNo = Barcode.Substring(0, 4);
			ticketInfo.InTime = DateTime.ParseExact(Barcode.Substring(13, 12), "yyMMddHHmmss", null);
			ticketInfo.ParkType = (EnumParkType)(int.Parse(Barcode.Substring(4, 1)) - 5);
			ticketInfo.TicketNumber = Barcode.Substring(5, 7);
		}
		return ticketInfo;
	}
}
