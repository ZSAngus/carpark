using System;
using System.ServiceModel;
using Master.Lib.Communication;

namespace Master.SystemCommunication.RFIDInterface;

public class RFIDContext : IRFIDServiceCallBack, ICallback
{
	private static IRFIDService m_CommunicationChannel = null;

	private string m_Endpointname;

	public IRFIDService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((RFIDProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(this);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new RFIDProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new RFIDProxy(callbackInstance);
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

	public RFIDContext()
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new RFIDProxy(callbackInstance);
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public RFIDContext(string endpointName)
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new RFIDProxy(callbackInstance, endpointName);
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
