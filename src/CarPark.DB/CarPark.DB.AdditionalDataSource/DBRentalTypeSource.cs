using System.Collections.Generic;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class DBRentalTypeSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (RentalType rentalType in DataBuffer.RentalTypes)
		{
			list.Add(new KeyValuePair<int, string>(rentalType.RentalTypeID, rentalType.RentalName));
		}
		return list;
	}
}
