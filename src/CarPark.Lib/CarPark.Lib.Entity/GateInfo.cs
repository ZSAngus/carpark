using CarPark.DB.AdditionalDataSource;
using SkyInno.UI.BindingText;

namespace CarPark.Lib.Entity;

public class GateInfo
{
	[BindingControlEditStyle(EnumEditStyle.MultiCheckBox, typeof(DBParkAreaSource))]
	public int Area { get; set; }

	public string AreaName => "A區";

	public string ControlMode => "自動";

	public int GateID { get; set; }

	public string GateName { get; set; }

	[BindingControlEditStyle(EnumEditStyle.EnumComboBox, typeof(EnumPassDirectionSource))]
	public int PassDirection { get; set; }

	public GateInfo()
	{
		Class2.sKBPqdpzNwCBA();
	}
}
