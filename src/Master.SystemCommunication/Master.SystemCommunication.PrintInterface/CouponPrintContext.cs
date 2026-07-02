using System;
using System.ServiceModel;
using Master.Lib.Communication;

namespace Master.SystemCommunication.PrintInterface;

/// <summary>
/// 優惠券打印服務上下文
/// Roger Zhang
/// 20180413
/// </summary>
public class CouponPrintContext : ICallback
{
	private ICouponPrintService m_CommunicationChannel = null;

	private string m_Endpointname;

	public ICouponPrintService CommunicationChannel
	{
		get
		{
			if (m_CommunicationChannel == null || ((CouponPrintProxy)m_CommunicationChannel).State != CommunicationState.Opened)
			{
				InstanceContext callbackInstance = new InstanceContext(this);
				if (!string.IsNullOrEmpty(m_Endpointname))
				{
					m_CommunicationChannel = new CouponPrintProxy(callbackInstance, m_Endpointname);
				}
				else
				{
					m_CommunicationChannel = new CouponPrintProxy(callbackInstance);
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

	public CouponPrintContext()
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new CouponPrintProxy(callbackInstance);
		try
		{
			m_CommunicationChannel.Connect();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public CouponPrintContext(string endpointName)
	{
		m_Endpointname = string.Empty;
		InstanceContext callbackInstance = new InstanceContext(this);
		m_CommunicationChannel = new CouponPrintProxy(callbackInstance, endpointName);
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
		Console.WriteLine("Callback");
	}
}
