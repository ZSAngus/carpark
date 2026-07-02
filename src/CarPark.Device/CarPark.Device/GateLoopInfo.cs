using System;

namespace CarPark.Device;

public class GateLoopInfo
{
	private bool m_CarWasLefted;

	public string CARDNO { get; set; }

	public bool CarWasLefted
	{
		get
		{
			return m_CarWasLefted;
		}
		set
		{
			m_CarWasLefted = value;
		}
	}

	public byte[] ErrorMessage { get; set; }

	public int GateID { get; set; }

	public bool HeighSensor2 { get; set; }

	public bool HeightSensor1 { get; set; }

	public bool Loop { get; set; }

	public DateTime PassTime { get; set; }

	public bool Receipt { get; set; }

	public bool Spare1 { get; set; }

	public bool Spare2 { get; set; }

	public bool SpareA { get; set; }

	public bool SpareB { get; set; }

	public GateLoopInfo(byte[] byte_0)
	{
		Class2.hEE203xzkPmdM();
		m_CarWasLefted = true;
	}

	public override string ToString()
	{
		return string.Empty + $"{Environment.NewLine}Loop{Loop}" + $"{Environment.NewLine}Receipt{Receipt}" + $"{Environment.NewLine}SpareA{SpareA}" + $"{Environment.NewLine}SpareB{SpareB}" + $"{Environment.NewLine}HeightSensor1{HeightSensor1}" + $"{Environment.NewLine}HeighSensor2{HeighSensor2}" + $"{Environment.NewLine}Spare1{Spare1}" + $"{Environment.NewLine}Spare2{Spare2}" + $"{Environment.NewLine}CARDNO{CARDNO}";
	}
}
