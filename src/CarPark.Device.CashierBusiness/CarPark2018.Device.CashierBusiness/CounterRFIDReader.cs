using System;
using System.IO.Ports;
using System.Threading;
using CarPark.Device.SerialComm;

namespace CarPark2018.Device.CashierBusiness;

public class CounterRFIDReader : BaseComm
{
	public delegate void SmartCardReadEventHandler(string cardNum);

	private ByteList m_ByteList;

	private SmartCardReadEventHandler m_SmartCardReadEvent;

	public string RFIDID { get; set; }

	public event SmartCardReadEventHandler SmartCardReadEvent
	{
		add
		{
			SmartCardReadEventHandler smartCardReadEventHandler = m_SmartCardReadEvent;
			SmartCardReadEventHandler smartCardReadEventHandler2;
			do
			{
				smartCardReadEventHandler2 = smartCardReadEventHandler;
				SmartCardReadEventHandler value2 = (SmartCardReadEventHandler)Delegate.Combine(smartCardReadEventHandler2, value);
				smartCardReadEventHandler = Interlocked.CompareExchange(ref m_SmartCardReadEvent, value2, smartCardReadEventHandler2);
			}
			while (smartCardReadEventHandler != smartCardReadEventHandler2);
		}
		remove
		{
			SmartCardReadEventHandler smartCardReadEventHandler = m_SmartCardReadEvent;
			SmartCardReadEventHandler smartCardReadEventHandler2;
			do
			{
				smartCardReadEventHandler2 = smartCardReadEventHandler;
				SmartCardReadEventHandler value2 = (SmartCardReadEventHandler)Delegate.Remove(smartCardReadEventHandler2, value);
				smartCardReadEventHandler = Interlocked.CompareExchange(ref m_SmartCardReadEvent, value2, smartCardReadEventHandler2);
			}
			while (smartCardReadEventHandler != smartCardReadEventHandler2);
		}
	}

	public CounterRFIDReader()
	{
		m_ByteList = new ByteList();
	}

	public CounterRFIDReader(string Comport, int BauadRate)
		: base(Comport, BauadRate)
	{
		m_ByteList = new ByteList();
	}

	private void DispachMessage(string message)
	{
		if (m_SmartCardReadEvent != null)
		{
			m_SmartCardReadEvent(message);
		}
	}

	public override void m_CommunicationPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
	{
		if (e.EventType == SerialData.Chars)
		{
			byte[] array = new byte[m_CommunicationPort.BytesToRead];
			m_CommunicationPort.Read(array, 0, array.Length);
			ProcessByte(array);
		}
	}

	private void ProcessByte(byte[] Process)
	{
		m_ByteList.AppendBytes(Process);
		while (m_ByteList.HasMessage())
		{
			string message = m_ByteList.GetMessage();
			if (!string.IsNullOrEmpty(message))
			{
				DispachMessage(message);
			}
		}
	}
}
