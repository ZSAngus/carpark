using System;
using System.IO.Ports;

namespace CarPark.Device.SerialComm;

public abstract class BaseComm
{
	protected ComQueue _comQueue;

	protected int _milliseconds;

	protected static object _writeLocker;

	protected int m_BauadRate;

	protected byte[] m_Buffer;

	protected SerialPort m_CommunicationPort;

	protected string m_Comport;

	public int BauadRate
	{
		get
		{
			return m_BauadRate;
		}
		set
		{
			m_BauadRate = value;
		}
	}

	public SerialPort CommunicationPort => m_CommunicationPort;

	public string Comport
	{
		get
		{
			return m_Comport;
		}
		set
		{
			m_Comport = value;
		}
	}

	public bool IsConnected
	{
		get
		{
			if (m_CommunicationPort != null)
			{
				return m_CommunicationPort.IsOpen;
			}
			return false;
		}
	}

	public string LastError { get; set; }

	static BaseComm()
	{
		Class2.WKJkUh2zLspup();
		_writeLocker = new object();
	}

	public BaseComm()
	{
		Class2.WKJkUh2zLspup();
		m_CommunicationPort = null;
		_milliseconds = 200;
		_comQueue = new ComQueue();
		m_Buffer = new byte[0];
		m_Comport = "com1";
		m_BauadRate = 9600;
	}

	public BaseComm(string ComPort, int BauadRate)
	{
		Class2.WKJkUh2zLspup();
		m_CommunicationPort = null;
		_milliseconds = 200;
		_comQueue = new ComQueue();
		m_Buffer = new byte[0];
		m_Comport = "com1";
		m_BauadRate = 9600;
		m_Comport = ComPort;
		m_BauadRate = BauadRate;
	}

	public void AppendBuff(byte[] Data)
	{
		byte[] array = new byte[m_Buffer.Length + Data.Length];
		Array.Copy(m_Buffer, 0, array, 0, m_Buffer.Length);
		Array.Copy(Data, 0, array, m_Buffer.Length, Data.Length);
		m_Buffer = array;
	}

	public virtual void Close()
	{
		if (m_CommunicationPort != null && m_CommunicationPort.IsOpen)
		{
			m_CommunicationPort.DataReceived -= m_CommunicationPort_DataReceived;
			m_CommunicationPort.Close();
			m_CommunicationPort.Dispose();
		}
		m_CommunicationPort = null;
	}

	protected int FirstIndexof(byte byt)
	{
		for (int i = 0; i < m_Buffer.Length; i++)
		{
			if (m_Buffer[i] == byt)
			{
				return i;
			}
		}
		return -1;
	}

	public virtual void m_CommunicationPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
	{
	}

	public virtual void Open()
	{
		try
		{
			Close();
			SerialPort serialPort = new SerialPort(m_Comport, m_BauadRate);
			serialPort.ReadTimeout = 2000;
			serialPort.WriteTimeout = 2000;
			SerialPort communicationPort = serialPort;
			m_CommunicationPort = communicationPort;
			m_CommunicationPort.WriteTimeout = 5000;
			m_CommunicationPort.ReadTimeout = 5000;
			m_CommunicationPort.DataReceived += m_CommunicationPort_DataReceived;
			m_CommunicationPort.Open();
		}
		catch (Exception ex)
		{
			LastError = ex.Message;
			throw;
		}
	}

	public void Open(string Comport)
	{
		m_Comport = Comport;
		Open();
	}

	public void SendCommand(byte[] command)
	{
		try
		{
			lock (_writeLocker)
			{
				m_CommunicationPort.Write(command, 0, command.Length);
			}
		}
		catch (Exception ex)
		{
			LastError = ex.Message;
			throw;
		}
	}
}
