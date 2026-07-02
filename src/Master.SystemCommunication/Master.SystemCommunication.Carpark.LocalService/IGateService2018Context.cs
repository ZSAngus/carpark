using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 車場閘機服務
/// </summary>
/// <typeparam name="T"></typeparam>
public class IGateService2018Context<T> where T : ICallback, new()
{
	private static IGateService2018 m_CommunicationChannel;

	private string m_Endpointname;

	private static T m_CallBack;

	private static ProgramInfo m_ProgramInfo;

	private static CommunicationState m_Connection;

	private static object obj;

	public IGateService2018 CommunicationChannel
	{
		get
		{
			lock (obj)
			{
				if (m_CommunicationChannel != null)
				{
					Console.WriteLine("1" + ((GateService2018Proxy)m_CommunicationChannel).State);
				}
				if (m_CommunicationChannel == null || ((GateService2018Proxy)m_CommunicationChannel).State != CommunicationState.Opened)
				{
					if (m_CommunicationChannel != null)
					{
						Console.WriteLine("2" + ((GateService2018Proxy)m_CommunicationChannel).State);
					}
					InstanceContext callbackInstance = new InstanceContext(m_CallBack);
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

	static IGateService2018Context()
	{
		m_Connection = CommunicationState.Faulted;
		obj = new object();
		m_CommunicationChannel = null;
		m_CallBack = default(T);
		m_ProgramInfo = null;
	}

	public IGateService2018Context(T t, ProgramInfo programInfo)
	{
		m_Endpointname = string.Empty;
		m_CallBack = t;
		m_ProgramInfo = programInfo;
		Start();
	}

	public IGateService2018Context(string endpointName, ProgramInfo programInfo)
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
				if (((GateService2018Proxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((GateService2018Proxy)CommunicationChannel).State;
					if (IGateService2018Context<T>.ConnectionChange != null)
					{
						IGateService2018Context<T>.ConnectionChange(m_Connection);
					}
				}
				Console.WriteLine("3");
				CommunicationChannel.RunListen();
			}
			catch (Exception value)
			{
				try
				{
					((GateService2018Proxy)CommunicationChannel).Close();
				}
				catch
				{
					((GateService2018Proxy)CommunicationChannel).Abort();
				}
				Console.WriteLine(value);
			}
			Thread.Sleep(8000);
		}
	}
}
