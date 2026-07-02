using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.APSInterface;

public class ApsMXtraContext<T> where T : ICallback
{
	private static IAPSMXtraService m_CommunicationChannel;

	private string m_Endpointname;

	private static ProgramInfo m_ProgramInfo;

	private static T m_CallBack;

	private static CommunicationState m_Connection;

	public IAPSMXtraService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((APSMXtraProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(m_CallBack);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new APSMXtraProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new APSMXtraProxy(callbackInstance);
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

	static ApsMXtraContext()
	{
		m_Connection = CommunicationState.Faulted;
		m_CommunicationChannel = null;
		m_CallBack = default(T);
	}

	public ApsMXtraContext(ProgramInfo programInfo, T t)
	{
		m_Endpointname = string.Empty;
		m_ProgramInfo = programInfo;
		m_CallBack = t;
		Start();
	}

	public ApsMXtraContext(string endpointName, ProgramInfo programInfo, T t)
	{
		m_Endpointname = string.Empty;
		m_Endpointname = endpointName;
		m_ProgramInfo = programInfo;
		m_CallBack = t;
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
			Thread.Sleep(3000);
			try
			{
				if (((APSMXtraProxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((APSMXtraProxy)CommunicationChannel).State;
					if (ApsMXtraContext<T>.ConnectionChange != null)
					{
						ApsMXtraContext<T>.ConnectionChange(m_Connection);
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}
	}
}
