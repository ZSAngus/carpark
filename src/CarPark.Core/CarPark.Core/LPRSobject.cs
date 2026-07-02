using System;
using System.Runtime.Serialization;

namespace CarPark.Core;

[DataContract]
public class LPRSobject
{
	[DataMember]
	public string GuID { get; set; }

	[DataMember]
	public int CurrGateID { get; set; }

	[DataMember]
	public string CurrCardNum { get; set; }

	[DataMember]
	public EnumPassDirection GateType { get; set; }

	[DataMember]
	public LPRSOptions LprsOpetions { get; set; }

	[DataMember]
	public EnumCardType CardType { get; set; }

	[DataMember]
	public string RegisterLicensePlate { get; set; }

	[DataMember]
	public int TimeOut { get; set; }

	[DataMember]
	public int TransactionData { get; set; }

	[DataMember]
	public int PassTraceID { get; set; }

	[DataMember]
	public DateTime CreateTime { get; set; }

	[DataMember]
	public string EnterResult { get; set; }

	[DataMember]
	public string EnterImagePath { get; set; }

	[DataMember]
	public string CurrResult { get; set; }

	[DataMember]
	public string ImagePath { get; set; }

	[DataMember]
	public EnumPassReturn PassReturn { get; set; }

	[DataMember]
	public double Similarity { get; set; }

	public string LicensePlate
	{
		get
		{
			if (string.IsNullOrEmpty(CurrResult) || "UNKNOWN" == CurrResult)
			{
				return "UNKNOW";
			}
			return CurrResult.Trim();
		}
	}

	public bool CheckPass()
	{
		bool result = false;
		switch (GateType)
		{
		case EnumPassDirection.OUT:
			if (LicensePlate == RegisterLicensePlate || LicensePlate == EnterResult)
			{
				result = true;
			}
			break;
		case EnumPassDirection.IN:
			result = (CardType != EnumCardType.SmartCard && CardType != EnumCardType.TempMonthCard) || CurrResult.Equals(RegisterLicensePlate);
			break;
		}
		return result;
	}
}
