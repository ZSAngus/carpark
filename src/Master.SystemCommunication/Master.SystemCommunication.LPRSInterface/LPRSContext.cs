using System;
using System.ServiceModel;
using System.Threading;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.LPRSInterface;

public class LPRSContext<T> where T : ICallback
{
	private static ILPRSService m_CommunicationChannel;

	private string m_Endpointname;

	private static ProgramInfo m_ProgramInfo;

	private static T m_CallBack;

	private static CommunicationState m_Connection;

	public ILPRSService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((LPRSProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(m_CallBack);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new LPRSProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new LPRSProxy(callbackInstance);
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

	static LPRSContext()
	{
		m_Connection = CommunicationState.Faulted;
		m_CommunicationChannel = null;
		m_CallBack = default(T);
	}

	public LPRSContext(ProgramInfo programInfo, T t)
	{
		m_Endpointname = string.Empty;
		m_ProgramInfo = programInfo;
		m_CallBack = t;
		Start();
	}

	public LPRSContext(string endpointName, ProgramInfo programInfo, T t)
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
			Thread.Sleep(2000);
			try
			{
				if (((LPRSProxy)CommunicationChannel).State != m_Connection)
				{
					m_Connection = ((LPRSProxy)CommunicationChannel).State;
					if (LPRSContext<T>.ConnectionChange != null)
					{
						LPRSContext<T>.ConnectionChange(m_Connection);
					}
				}
				CommunicationChannel.RunListen();
			}
			catch (Exception value)
			{
				try
				{
					((LPRSProxy)CommunicationChannel).Close();
				}
				catch
				{
					((LPRSProxy)CommunicationChannel).Abort();
				}
				Console.WriteLine(value);
			}
			Thread.Sleep(8000);
		}
	}
}
