using System.Collections.Generic;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class DBCustomFreeTypeSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (CustomFreeType customFreeType in DataBuffer.CustomFreeTypes)
		{
			list.Add(new KeyValuePair<int, string>(customFreeType.CustomFreeTypeID, customFreeType.CustomFreeName));
		}
		return list;
	}
}
