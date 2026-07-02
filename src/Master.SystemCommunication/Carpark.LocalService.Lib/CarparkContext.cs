using System;
using System.ServiceModel;
using Master.Lib.Communication;

namespace Carpark.LocalService.Lib;

public class CarparkContext : ICarparkCallback, ICallback
{
	private ICarparkService m_CommunicationChannel = null;

	private string m_Endpointname;

	public ICarparkService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((CarparkServiceProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(this);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new CarparkServiceProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new CarparkServiceProxy(callbackInstance);
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

	public CarparkContext()
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new CarparkServiceProxy(callbackInstance);
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public CarparkContext(string endpointName)
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new CarparkServiceProxy(callbackInstance, endpointName);
		m_Endpointname = endpointName;
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception)
		{
		}
	}

	public void Callback()
	{
		Console.WriteLine("call ICarparkCallback !");
	}
}
