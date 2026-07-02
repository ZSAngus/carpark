using System.Collections.Generic;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class DBStaffTypeSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (StaffType staffType in DataBuffer.StaffTypes)
		{
			list.Add(new KeyValuePair<int, string>(staffType.StaffTypeID, staffType.StaffTypeName));
		}
		return list;
	}
}
