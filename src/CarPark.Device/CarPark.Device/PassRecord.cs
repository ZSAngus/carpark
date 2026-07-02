using System;
using CarPark.Core;

namespace CarPark.Device;

public class PassRecord
{
	private string m_GateID;

	private EnumParkType m_ParkType;

	private DateTime m_PassTime;

	private string m_TicketNo;

	private EnumCardType m_TicketType;

	private string m_TypeNo;

	public string GateID => m_GateID;

	public EnumParkType ParkType
	{
		get
		{
			return m_ParkType;
		}
		set
		{
			m_ParkType = value;
		}
	}

	public DateTime PassTime
	{
		get
		{
			return m_PassTime;
		}
		set
		{
			m_PassTime = value;
		}
	}

	public int? RentalType { get; set; }

	public string TicketNo
	{
		get
		{
			return m_TicketNo;
		}
		set
		{
			m_TicketNo = value;
		}
	}

	public EnumCardType TicketType
	{
		get
		{
			return m_TicketType;
		}
		set
		{
			m_TicketType = value;
		}
	}

	public string TypeNo => m_TypeNo;

	public PassRecord()
	{
		Class2.hEE203xzkPmdM();
		m_ParkType = EnumParkType.None;
		m_GateID = string.Empty;
		m_TicketType = EnumCardType.Ticket;
		m_TypeNo = string.Empty;
		m_TicketNo = string.Empty;
		m_PassTime = DateTime.MinValue;
	}

	public PassRecord(string string_0, string gateID)
	{
		Class2.hEE203xzkPmdM();
		m_ParkType = EnumParkType.None;
		m_GateID = string.Empty;
		m_TicketType = EnumCardType.Ticket;
		m_TypeNo = string.Empty;
		m_TicketNo = string.Empty;
		m_PassTime = DateTime.MinValue;
		if (string_0.Substring(0, 1) == "H")
		{
			m_TicketType = EnumCardType.Ticket;
		}
		else
		{
			m_TicketType = EnumCardType.SmartCard;
		}
		string typeNo = string_0.Substring(1, 1);
		m_TypeNo = typeNo;
		typeNo = string_0.Substring(2, 7);
		m_TicketNo = typeNo;
		typeNo = string_0.Substring(10, 17);
		m_PassTime = DateTime.ParseExact(typeNo.Replace("/", ""), "yyMMdd HH:mm:ss", null);
		m_GateID = gateID;
		try
		{
			m_ParkType = (EnumParkType)(int.Parse(m_TypeNo) - 5);
		}
		catch (Exception)
		{
		}
	}
}
