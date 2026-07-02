using System;
using System.ServiceModel;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 閘機單向服務
/// </summary>
public class GateSingleChannelContext
{
	private IGateService2018 m_CommunicationChannel = null;

	private string m_Endpointname;

	private static GateSingleChannelCallBack m_callBack;

	public IGateService2018 CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((GateService2018Proxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(m_callBack);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new GateService2018Proxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new GateService2018Proxy(callbackInstance);
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

	public GateSingleChannelContext()
	{
		if (m_callBack == null)
		{
			m_callBack = new GateSingleChannelCallBack();
		}
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(m_callBack);
		m_CommunicationChannel = new GateService2018Proxy(callbackInstance);
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public GateSingleChannelContext(string endpointName)
	{
		if (m_callBack == null)
		{
			m_callBack = new GateSingleChannelCallBack();
		}
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(m_callBack);
		m_CommunicationChannel = new GateService2018Proxy(callbackInstance, endpointName);
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
