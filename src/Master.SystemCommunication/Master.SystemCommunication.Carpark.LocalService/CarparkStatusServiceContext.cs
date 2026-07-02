using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
///             车场状态上下文
/// </summary>
/// <typeparam name="T"></typeparam>
public class CarparkStatusServiceContext<T> where T : ICallback, new()
{
	private static ICarparkStatusService m_CommunicationChannel;

	private string m_Endpointname;

	private static T m_CallBack;

	private static ProgramInfo m_ProgramInfo;

	private static CommunicationState m_Connection;

	public ICarparkStatusService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((CarparkStatusServiceProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(m_CallBack);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new CarparkStatusServiceProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new CarparkStatusServiceProxy(callbackInstance);
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

	static CarparkStatusServiceContext()
	{
		m_Connection = CommunicationState.Faulted;
		m_CommunicationChannel = null;
		m_CallBack = default(T);
		m_ProgramInfo = null;
	}

	public CarparkStatusServiceContext(T t, ProgramInfo programInfo)
	{
		m_Endpointname = string.Empty;
		m_CallBack = t;
		m_ProgramInfo = programInfo;
		Start();
	}

	public CarparkStatusServiceContext(string endpointName, ProgramInfo programInfo)
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
				if (((CarparkStatusServiceProxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((CarparkStatusServiceProxy)CommunicationChannel).State;
					if (CarparkStatusServiceContext<T>.ConnectionChange != null)
					{
						CarparkStatusServiceContext<T>.ConnectionChange(m_Connection);
					}
				}
				CommunicationChannel.RunListen();
			}
			catch (Exception value)
			{
				try
				{
					((CarparkStatusServiceProxy)CommunicationChannel).Close();
				}
				catch
				{
					((CarparkStatusServiceProxy)CommunicationChannel).Abort();
				}
				Console.WriteLine(value);
			}
			Thread.Sleep(8000);
		}
	}
}
