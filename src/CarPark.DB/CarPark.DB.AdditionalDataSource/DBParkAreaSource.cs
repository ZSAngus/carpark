using System.Collections.Generic;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class DBParkAreaSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (ParkArea parkArea in DataBuffer.ParkAreas)
		{
			list.Add(new KeyValuePair<int, string>(parkArea.AreaID, parkArea.AreaName));
		}
		return list;
	}
}
