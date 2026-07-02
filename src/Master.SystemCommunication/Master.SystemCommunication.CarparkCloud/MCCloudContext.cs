using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.CarparkCloud;

public class MCCloudContext<T> where T : ICallback, new()
{
	private static IMCCloudService m_CommunicationChannel;

	private string m_Endpointname;

	private static T m_CallBack;

	private static ProgramInfo m_ProgramInfo;

	private static CommunicationState m_Connection;

	public IMCCloudService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((MCCloudProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(m_CallBack);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new MCCloudProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new MCCloudProxy(callbackInstance);
				}
				try
				{
					m_CommunicationChannel.Join(m_ProgramInfo);
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

	public static event Action<CommunicationState> ConnectionChange;

	static MCCloudContext()
	{
		m_Connection = CommunicationState.Faulted;
		m_CommunicationChannel = null;
		m_CallBack = default(T);
		m_ProgramInfo = null;
	}

	public MCCloudContext(T t, ProgramInfo programInfo)
	{
		m_Endpointname = string.Empty;
		m_CallBack = t;
		m_ProgramInfo = programInfo;
		Start();
	}

	public MCCloudContext(string endpointName, ProgramInfo programInfo)
	{
		m_Endpointname = string.Empty;
		m_Endpointname = endpointName;
		m_ProgramInfo = programInfo;
	}

	private void Start()
	{
		Thread thread = new Thread(CheckConnection);
		thread.IsBackground = true;
		Thread thread2 = thread;
		thread2.Start();
	}

	private void CheckConnection()
	{
		while (true)
		{
			bool flag = true;
			try
			{
				Thread.Sleep(2000);
				if (((MCCloudProxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((MCCloudProxy)CommunicationChannel).State;
					if (MCCloudContext<T>.ConnectionChange != null)
					{
						MCCloudContext<T>.ConnectionChange(m_Connection);
					}
				}
				CommunicationChannel.RunListen();
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
				try
				{
					((MCCloudProxy)CommunicationChannel).Close();
				}
				catch
				{
					((MCCloudProxy)CommunicationChannel).Abort();
				}
			}
			Thread.Sleep(8000);
		}
	}
}
