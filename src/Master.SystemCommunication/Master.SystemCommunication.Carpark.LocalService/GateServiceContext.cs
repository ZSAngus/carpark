using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

public class GateServiceContext<T> where T : ICallback, new()
{
	private static IGateService m_CommunicationChannel;

	private string m_Endpointname;

	private static T m_CallBack;

	private static ProgramInfo m_ProgramInfo;

	private static CommunicationState m_Connection;

	private static object obj;

	public IGateService CommunicationChannel
	{
		get
		{
			lock (obj)
			{
				if (m_CommunicationChannel == null || ((GateServiceProxy)m_CommunicationChannel).State != CommunicationState.Opened)
				{
					InstanceContext callbackInstance = new InstanceContext(m_CallBack);
					if (!string.IsNullOrEmpty(m_Endpointname))
					{
						m_CommunicationChannel = new GateServiceProxy(callbackInstance, m_Endpointname);
					}
					else
					{
						m_CommunicationChannel = new GateServiceProxy(callbackInstance);
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
			}
			return m_CommunicationChannel;
		}
		set
		{
			m_CommunicationChannel = value;
		}
	}

	public static event Action<CommunicationState> ConnectionChange;

	static GateServiceContext()
	{
		m_Connection = CommunicationState.Faulted;
		obj = new object();
		m_CommunicationChannel = null;
		m_CallBack = default(T);
		m_ProgramInfo = null;
	}

	public GateServiceContext(T t, ProgramInfo programInfo)
	{
		m_Endpointname = string.Empty;
		m_CallBack = t;
		m_ProgramInfo = programInfo;
		Start();
	}

	public GateServiceContext(string endpointName, ProgramInfo programInfo)
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
			Thread.Sleep(2000);
			try
			{
				if (((GateServiceProxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((GateServiceProxy)CommunicationChannel).State;
					if (GateServiceContext<T>.ConnectionChange != null)
					{
						GateServiceContext<T>.ConnectionChange(m_Connection);
					}
				}
				CommunicationChannel.RunListen();
			}
			catch (Exception value)
			{
				try
				{
					((GateServiceProxy)CommunicationChannel).Close();
				}
				catch
				{
					((GateServiceProxy)CommunicationChannel).Abort();
				}
				Console.WriteLine(value);
			}
			Thread.Sleep(8000);
		}
	}
}
