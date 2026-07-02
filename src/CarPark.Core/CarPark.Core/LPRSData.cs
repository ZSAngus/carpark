using System;
using System.Runtime.Serialization;

namespace CarPark.Core;

[DataContract]
public class LPRSData
{
	[DataMember]
	public int LicensePlateID { get; set; }

	[DataMember]
	public string ImagePath { get; set; }

	[DataMember]
	public int PassTraceID { get; set; }

	[DataMember]
	public string CompareResult { get; set; }

	[DataMember]
	public DateTime CreateTime { get; set; }

	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public string CardNum { get; set; }

	[DataMember]
	public int ParkTypeID { get; set; }

	[DataMember]
	public int PassCardType { get; set; }

	[DataMember]
	public bool ISACK { get; set; }

	[DataMember]
	public int Status { get; set; }

	[DataMember]
	public string StaffCode { get; set; }

	[DataMember]
	public bool ISPass { get; set; }

	[DataMember]
	public EnumCardType CardType { get; set; }

	[DataMember]
	public int? RentalType { get; set; }

	[DataMember]
	public int PassDirection { get; set; }

	[DataMember]
	public string EnterImagePath { get; set; }

	[DataMember]
	public string ExitImagePath { get; set; }

	[DataMember]
	public string EnterResult { get; set; }

	[DataMember]
	public string ExitResult { get; set; }

	[DataMember]
	public string RegisterLicensePlate { get; set; }

	[DataMember]
	public EnumPassDirection GateType { get; set; }

	[DataMember]
	public int TimeOut { get; set; }

	[DataMember]
	public string GuID { get; set; }

	public string LicensePlate
	{
		get
		{
			if (string.IsNullOrEmpty(CompareResult) || "UNKNOWN" == CompareResult)
			{
				return "UNKNOW";
			}
			return CompareResult.Trim();
		}
	}

	public LPRSData(EnumPassDirection gateType)
	{
		GateType = gateType;
	}

	public bool CheckPass()
	{
		bool result = false;
		if (GateType == EnumPassDirection.OUT)
		{
			if (ExitResult == RegisterLicensePlate || ExitResult == EnterResult)
			{
				result = true;
			}
			return result;
		}
		return true;
	}
}
