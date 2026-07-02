using System;
using System.ServiceModel;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;

namespace CarPark2018;

public class Common
{
	public static CashierServiceContext<CashierServiceCallBack> m_CashierContext;

	public static CashierServiceCallBack m_callBack;

	public static CashierServiceContext<CashierServiceCallBack> _Carpark2018ServiceContext
	{
		get
		{
			if (m_CashierContext == null)
			{
				Console.WriteLine("Init _Carpark2018ServiceContext");
				if (m_callBack == null)
				{
					m_callBack = new CashierServiceCallBack();
				}
				m_CashierContext = new CashierServiceContext<CashierServiceCallBack>(m_callBack, new ProgramInfo(Settings.Default.OnlyID, SystemType.Sys_Fee));
				CashierServiceContext<CashierServiceCallBack>.ConnectionChange += Common_ConnectionChange;
			}
			return m_CashierContext;
		}
	}

	public static void Common_ConnectionChange(CommunicationState state)
	{
		Console.WriteLine("CommunicationState:" + state);
		if (state == CommunicationState.Opened)
		{
			Console.WriteLine("Open");
		}
		else
		{
			Console.WriteLine("error");
		}
	}

	public static string EncryptedCardNumber(string CardNo)
	{
		string result = CardNo;
		try
		{
			result = $"{CardNo.Substring(0, 6)}******{CardNo.Substring(CardNo.Length - 4, 4)}";
		}
		catch (Exception)
		{
		}
		return result;
	}
}
