using System;
using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.DB;

public class ShiftRecordExtend
{
	public decimal Amount { get; set; }

	public DateTime BillTime { get; set; }

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumBillTypeSource))]
	public int BillTypeName { get; set; }

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumParkTypeSource))]
	public int ParkType { get; set; }
}
