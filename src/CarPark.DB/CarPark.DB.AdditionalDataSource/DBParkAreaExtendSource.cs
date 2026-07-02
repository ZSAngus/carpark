using System.Collections.Generic;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class DBParkAreaExtendSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (ParkAreaExtend parkAreaExtend in DataBuffer.ParkAreaExtends)
		{
			list.Add(new KeyValuePair<int, string>(parkAreaExtend.AreaID, parkAreaExtend.ExtendName));
		}
		return list;
	}
}
