using System.Collections.Generic;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class DBParkSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (Park park in DataBuffer.parks)
		{
			list.Add(new KeyValuePair<int, string>(park.ParkID, park.ParkCode));
		}
		return list;
	}
}
