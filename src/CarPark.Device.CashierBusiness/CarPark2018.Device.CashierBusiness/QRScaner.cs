using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using CarPark.Device.SerialComm;

namespace CarPark2018.Device.CashierBusiness;

public class QRScaner : BaseComm
{
	public delegate void QRCodeEventHandler(string str);

	private const string ReplyOK = "OK";

	private const byte StartFlag = 2;

	private const byte StopFlag = 13;

	private AutoResetEvent _autoResetEvent;

	private AutoResetEvent _cardNumWaiter;

	private new ComQueue _comQueue;

	private new int _milliseconds;

	private static object m_CommunicationSync;

	private static object m_OperationSync;

	private Thread QueryCardStatusThread;

	private ByteList m_ByteList;

	public QRCodeEventHandler m_QRCodeEvent;

	private string qrcord = "";

	private byte num;

	private string str2;

	private List<byte> list = new List<byte>();

	public event QRCodeEventHandler QRCodeEvent
	{
		add
		{
			QRCodeEventHandler qRCodeEventHandler = m_QRCodeEvent;
			QRCodeEventHandler qRCodeEventHandler2;
			do
			{
				qRCodeEventHandler2 = qRCodeEventHandler;
				QRCodeEventHandler value2 = (QRCodeEventHandler)Delegate.Combine(qRCodeEventHandler2, value);
				qRCodeEventHandler = Interlocked.CompareExchange(ref m_QRCodeEvent, value2, qRCodeEventHandler2);
			}
			while (qRCodeEventHandler != qRCodeEventHandler2);
		}
		remove
		{
			QRCodeEventHandler qRCodeEventHandler = m_QRCodeEvent;
			QRCodeEventHandler qRCodeEventHandler2;
			do
			{
				qRCodeEventHandler2 = qRCodeEventHandler;
				QRCodeEventHandler value2 = (QRCodeEventHandler)Delegate.Remove(qRCodeEventHandler2, value);
				qRCodeEventHandler = Interlocked.CompareExchange(ref m_QRCodeEvent, value2, qRCodeEventHandler2);
			}
			while (qRCodeEventHandler != qRCodeEventHandler2);
		}
	}

	public QRScaner(string Comport, int BauadRate)
		: base(Comport, BauadRate)
	{
		_autoResetEvent = new AutoResetEvent(initialState: false);
		_cardNumWaiter = new AutoResetEvent(initialState: false);
		_milliseconds = 2000;
		QueryCardStatusThread = null;
		m_ByteList = new ByteList();
	}

	public override void m_CommunicationPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
	{
		qrcord += m_CommunicationPort.ReadExisting();
		int num = qrcord.LastIndexOf("\r");
		if (num <= 0)
		{
			return;
		}
		try
		{
			string str = qrcord;
			m_QRCodeEvent(str);
			str = "";
			qrcord = "";
		}
		catch (Exception)
		{
			qrcord = "";
		}
	}

	private string ProcessByte(byte[] Process)
	{
		if (Process.Length <= 0)
		{
			return null;
		}
		string text = "";
		m_ByteList.AppendBytes(Process);
		while (m_ByteList.HasMessage())
		{
			string message = m_ByteList.GetMessage();
			if (!string.IsNullOrEmpty(message))
			{
				text += message;
			}
		}
		return text;
	}

	private string CheckTicket()
	{
		string result = "";
		try
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			while (true)
			{
				byte b = (byte)base.CommunicationPort.ReadByte();
				if (b != 2)
				{
					if (flag)
					{
						list.Add(b);
					}
					if (b != 13)
					{
						continue;
					}
					string text = Encoding.ASCII.GetString(list.ToArray(), 0, list.Count - 1);
					if (text == null)
					{
						break;
					}
					if (text.Length < 13)
					{
						continue;
					}
					int num = text.Length - 13;
					string text2 = "";
					for (int i = 0; i < num; i++)
					{
						try
						{
							text2 = text.Substring(i, 13);
						}
						catch
						{
							text2 = "";
						}
						if (text2 != "")
						{
							try
							{
								DateTime dateTime = Convert.ToDateTime(text2.Substring(0, 8));
								return text2;
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.ToString());
							}
						}
					}
				}
				else
				{
					flag = true;
				}
			}
			return "";
		}
		catch (Exception)
		{
		}
		finally
		{
			base.CommunicationPort.DiscardInBuffer();
			base.CommunicationPort.DiscardOutBuffer();
		}
		return result;
	}

	private string CheckTicket2()
	{
		string result = "";
		try
		{
			str2 += base.CommunicationPort.ReadLine();
			if (str2.Length >= 13)
			{
				int num = str2.Length - 13;
				string text = "";
				for (int i = 0; i < num; i++)
				{
					try
					{
						text = str2.Substring(i, 13);
					}
					catch
					{
						text = "";
					}
					if (text != "")
					{
						try
						{
							DateTime dateTime = Convert.ToDateTime(text.Substring(0, 8));
							str2 = "";
							return text;
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			base.CommunicationPort.DiscardInBuffer();
			base.CommunicationPort.DiscardOutBuffer();
		}
		return result;
	}
}
