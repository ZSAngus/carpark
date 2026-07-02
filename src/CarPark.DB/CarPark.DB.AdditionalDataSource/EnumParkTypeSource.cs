using System;
using System.Collections.Generic;
using CarPark.Core;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.DB.AdditionalDataSource;

public class EnumParkTypeSource : IAdditionalDataSource
{
	public object GetAdditionanDataSource()
	{
		Type typeFromHandle = typeof(EnumParkType);
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		string text = typeFromHandle.FullName + ".";
		foreach (int value in Enum.GetValues(typeFromHandle))
		{
			if (EnumCompare.Compare(value, DataBuffer.CompanyInfo.AllowedParkType))
			{
				list.Add(new KeyValuePair<int, string>(value, LangManager.GetLangString(text + Enum.GetName(typeFromHandle, value))));
			}
		}
		return list;
	}
}
