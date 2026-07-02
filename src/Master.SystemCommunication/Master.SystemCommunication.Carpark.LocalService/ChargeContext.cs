using System;
using System.ServiceModel;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 收費上下文
/// </summary>
public class ChargeContext
{
	private ICashierService m_CommunicationChannel = null;

	private string m_Endpointname;

	private static ChargeContextCallBack m_callBack;

	public ICashierService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((CashierServiceProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(m_callBack);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new CashierServiceProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new CashierServiceProxy(callbackInstance);
				}
				try
				{
					m_CommunicationChannel.Connect();
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
				}
			}
			return m_CommunicationChannel;
		}
		set
		{
			m_CommunicationChannel = value;
		}
	}

	public ChargeContext()
	{
		if (m_callBack == null)
		{
			m_callBack = new ChargeContextCallBack();
		}
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(m_callBack);
		m_CommunicationChannel = new CashierServiceProxy(callbackInstance);
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public ChargeContext(string endpointName)
	{
		if (m_callBack == null)
		{
			m_callBack = new ChargeContextCallBack();
		}
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(m_callBack);
		m_CommunicationChannel = new CashierServiceProxy(callbackInstance, endpointName);
		m_Endpointname = endpointName;
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public void Callback()
	{
		Console.WriteLine("call ICarparkCallback !");
	}
}
