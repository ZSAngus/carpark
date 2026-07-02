using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

public class SystemExtendServiceContext<T> where T : ICallback, new()
{
	private static ISystemExtendService m_CommunicationChannel;

	private string m_Endpointname;

	private static T m_CallBack;

	private static ProgramInfo m_ProgramInfo;

	private static CommunicationState m_Connection;

	public ISystemExtendService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((SystemExtendServiceProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(m_CallBack);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new SystemExtendServiceProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new SystemExtendServiceProxy(callbackInstance);
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

	static SystemExtendServiceContext()
	{
		m_Connection = CommunicationState.Faulted;
		m_CommunicationChannel = null;
		m_CallBack = default(T);
		m_ProgramInfo = null;
	}

	public SystemExtendServiceContext(T t, ProgramInfo programInfo)
	{
		m_Endpointname = string.Empty;
		m_CallBack = t;
		m_ProgramInfo = programInfo;
		Start();
	}

	public SystemExtendServiceContext(string endpointName, ProgramInfo programInfo)
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
				if (((SystemExtendServiceProxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((SystemExtendServiceProxy)CommunicationChannel).State;
					if (SystemExtendServiceContext<T>.ConnectionChange != null)
					{
						SystemExtendServiceContext<T>.ConnectionChange(m_Connection);
					}
				}
				CommunicationChannel.RunListen();
			}
			catch (Exception value)
			{
				try
				{
					((SystemExtendServiceProxy)CommunicationChannel).Close();
				}
				catch
				{
					((SystemExtendServiceProxy)CommunicationChannel).Abort();
				}
				Console.WriteLine(value);
			}
			Thread.Sleep(8000);
		}
	}
}
