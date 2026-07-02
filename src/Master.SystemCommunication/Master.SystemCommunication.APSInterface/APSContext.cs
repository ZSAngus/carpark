using System;
using System.ServiceModel;
using Master.Lib.Communication;

namespace Master.SystemCommunication.APSInterface;

public class APSContext : IAPSServiceCallBack, ICallback
{
	private static IAPSService m_CommunicationChannel = null;

	private string m_Endpointname;

	public IAPSService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((APSProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(this);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new APSProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new APSProxy(callbackInstance);
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

	public APSContext()
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new APSProxy(callbackInstance);
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public APSContext(string endpointName)
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new APSProxy(callbackInstance, endpointName);
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
		Console.WriteLine("call IAPSServiceCallBack !");
	}
}
