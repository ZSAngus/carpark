using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
///             收費處上下文
/// </summary>
/// <typeparam name="T"></typeparam>
public class CashierServiceContext<T> where T : ICallback, new()
{
	private static ICashierService m_CommunicationChannel;

	private string m_Endpointname;

	private static T m_CallBack;

	private static ProgramInfo m_ProgramInfo;

	private static CommunicationState m_Connection;

	private static object obj;

	public ICashierService CommunicationChannel
	{
		get
		{
			lock (obj)
			{
				if (m_CommunicationChannel == null || ((CashierServiceProxy)m_CommunicationChannel).State != CommunicationState.Opened)
				{
					InstanceContext callbackInstance = new InstanceContext(m_CallBack);
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

	static CashierServiceContext()
	{
		m_Connection = CommunicationState.Faulted;
		obj = new object();
		m_CommunicationChannel = null;
		m_CallBack = default(T);
		m_ProgramInfo = null;
	}

	public CashierServiceContext(T t, ProgramInfo programInfo)
	{
		m_Endpointname = string.Empty;
		m_CallBack = t;
		m_ProgramInfo = programInfo;
		Start();
	}

	public CashierServiceContext(string endpointName, ProgramInfo programInfo)
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
				if (((CashierServiceProxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((CashierServiceProxy)CommunicationChannel).State;
					if (CashierServiceContext<T>.ConnectionChange != null)
					{
						CashierServiceContext<T>.ConnectionChange(m_Connection);
					}
				}
				Console.WriteLine(" ");
				CommunicationChannel.RunListen();
			}
			catch (Exception value)
			{
				try
				{
					((CashierServiceProxy)CommunicationChannel).Close();
				}
				catch
				{
					((CashierServiceProxy)CommunicationChannel).Abort();
				}
				Console.WriteLine(value);
			}
			Thread.Sleep(8000);
		}
	}
}
