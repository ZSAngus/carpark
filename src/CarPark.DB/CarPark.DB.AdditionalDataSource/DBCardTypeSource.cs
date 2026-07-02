using System.Collections.Generic;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class DBCardTypeSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (CardType cardType in DataBuffer.CardTypes)
		{
			list.Add(new KeyValuePair<int, string>(cardType.CardTypeID, cardType.CardName));
		}
		return list;
	}
}
