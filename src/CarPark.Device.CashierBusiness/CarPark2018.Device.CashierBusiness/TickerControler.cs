using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using CarPark.Device;
using CarPark.Device.SerialComm;
using log4net;

namespace CarPark2018.Device.CashierBusiness;

public class TickerControler : BaseComm
{
	private delegate string CommandresponseHandler(string command);

	private const string ReplyOK = "OK";

	private const byte StartFlag = 2;

	private const byte StopFlag = 13;

	private AutoResetEvent _autoResetEvent;

	private AutoResetEvent _cardNumWaiter;

	private new ComQueue _comQueue;

	private new int _milliseconds;

	private TicketReaderResponse Lastresp;

	private TicketState LastState;

	private ILog Logger;

	private static object m_CommunicationSync;

	private static object m_OperationSync;

	private Thread QueryCardStatusThread;

	private TicketMoveEventHandler m_TicketMoveEvent;

	private TicketReadEventHandler m_TicketReadEvent;

	public event TicketMoveEventHandler TicketMoveEvent
	{
		add
		{
			TicketMoveEventHandler ticketMoveEventHandler = m_TicketMoveEvent;
			TicketMoveEventHandler ticketMoveEventHandler2;
			do
			{
				ticketMoveEventHandler2 = ticketMoveEventHandler;
				TicketMoveEventHandler value2 = (TicketMoveEventHandler)Delegate.Combine(ticketMoveEventHandler2, value);
				ticketMoveEventHandler = Interlocked.CompareExchange(ref m_TicketMoveEvent, value2, ticketMoveEventHandler2);
			}
			while (ticketMoveEventHandler != ticketMoveEventHandler2);
		}
		remove
		{
			TicketMoveEventHandler ticketMoveEventHandler = m_TicketMoveEvent;
			TicketMoveEventHandler ticketMoveEventHandler2;
			do
			{
				ticketMoveEventHandler2 = ticketMoveEventHandler;
				TicketMoveEventHandler value2 = (TicketMoveEventHandler)Delegate.Remove(ticketMoveEventHandler2, value);
				ticketMoveEventHandler = Interlocked.CompareExchange(ref m_TicketMoveEvent, value2, ticketMoveEventHandler2);
			}
			while (ticketMoveEventHandler != ticketMoveEventHandler2);
		}
	}

	public event TicketReadEventHandler TicketReadEvent
	{
		add
		{
			TicketReadEventHandler ticketReadEventHandler = m_TicketReadEvent;
			TicketReadEventHandler ticketReadEventHandler2;
			do
			{
				ticketReadEventHandler2 = ticketReadEventHandler;
				TicketReadEventHandler value2 = (TicketReadEventHandler)Delegate.Combine(ticketReadEventHandler2, value);
				ticketReadEventHandler = Interlocked.CompareExchange(ref m_TicketReadEvent, value2, ticketReadEventHandler2);
			}
			while (ticketReadEventHandler != ticketReadEventHandler2);
		}
		remove
		{
			TicketReadEventHandler ticketReadEventHandler = m_TicketReadEvent;
			TicketReadEventHandler ticketReadEventHandler2;
			do
			{
				ticketReadEventHandler2 = ticketReadEventHandler;
				TicketReadEventHandler value2 = (TicketReadEventHandler)Delegate.Remove(ticketReadEventHandler2, value);
				ticketReadEventHandler = Interlocked.CompareExchange(ref m_TicketReadEvent, value2, ticketReadEventHandler2);
			}
			while (ticketReadEventHandler != ticketReadEventHandler2);
		}
	}

	static TickerControler()
	{
		m_CommunicationSync = new object();
		m_OperationSync = new object();
	}

	public TickerControler()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		_comQueue = new ComQueue();
		_autoResetEvent = new AutoResetEvent(initialState: false);
		_cardNumWaiter = new AutoResetEvent(initialState: false);
		_milliseconds = 2000;
		LastState = TicketState.Unknow;
		QueryCardStatusThread = null;
		Lastresp = null;
	}

	public TickerControler(string Comport, int BauadRate)
		: base(Comport, BauadRate)
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		_comQueue = new ComQueue();
		_autoResetEvent = new AutoResetEvent(initialState: false);
		_cardNumWaiter = new AutoResetEvent(initialState: false);
		_milliseconds = 2000;
		LastState = TicketState.Unknow;
		QueryCardStatusThread = null;
		Lastresp = null;
	}

	public void AcceptTicket()
	{
		ProcessSimpleCommand("EJECTB");
	}

	private TicketState CheckTicket()
	{
		TicketState result = TicketState.Unknow;
		Monitor.Enter(m_OperationSync);
		try
		{
			WriteCom("CKSLOT");
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
					if (b == 13)
					{
						break;
					}
				}
				else
				{
					flag = true;
				}
			}
			result = Encoding.ASCII.GetString(list.ToArray(), 0, list.Count - 1) switch
			{
				"NO CARD" => TicketState.Non, 
				"YES CARD" => TicketState.Ticket, 
				_ => TicketState.Unknow, 
			};
			base.CommunicationPort.DiscardInBuffer();
			base.CommunicationPort.DiscardOutBuffer();
		}
		catch (Exception)
		{
		}
		finally
		{
			Monitor.Exit(m_OperationSync);
		}
		return result;
	}

	public void EjectTicket()
	{
		ProcessSimpleCommand("EJECTF");
	}

	private void Free()
	{
		if (QueryCardStatusThread != null && QueryCardStatusThread.IsAlive)
		{
			QueryCardStatusThread.Abort();
		}
		QueryCardStatusThread = null;
	}

	private TicketReaderResponse GetBackCard(int replayLen)
	{
		bool flag = false;
		TicketReaderResponse ticketReaderResponse;
		do
		{
			if ((ticketReaderResponse = _comQueue.Dequeue()) != null)
			{
				if (replayLen == 2 && ticketReaderResponse.Response == "OK")
				{
					flag = true;
				}
				else if (replayLen == 11 && ticketReaderResponse.Length > 11)
				{
					flag = true;
				}
				else if (replayLen == 5 && (ticketReaderResponse.Length == 10 || ticketReaderResponse.Length == 9))
				{
					flag = true;
				}
			}
		}
		while (!flag);
		return ticketReaderResponse;
	}

	private TicketReaderResponse GetBackPacket(int replayLen)
	{
		TicketReaderResponse ticketReaderResponse = null;
		bool flag = false;
		do
		{
			bool flag2 = true;
			if (!_autoResetEvent.WaitOne(_milliseconds))
			{
				throw new TimeoutException("接收超时");
			}
			while ((ticketReaderResponse = _comQueue.Dequeue()) != null)
			{
				if (replayLen == 2 && ticketReaderResponse.Response == "OK")
				{
					flag = true;
					break;
				}
				if (replayLen == 11 && ticketReaderResponse.Length > 11)
				{
					flag = true;
					break;
				}
				if (replayLen == 5 && (ticketReaderResponse.Length == 10 || ticketReaderResponse.Length == 9))
				{
					flag = true;
					break;
				}
			}
		}
		while (!flag);
		return ticketReaderResponse;
	}

	private bool HasMessage()
	{
		int num = FirstIndexof(2);
		int num2 = FirstIndexof(13);
		return num >= 0 && num2 > 0 && num < num2;
	}

	public override void m_CommunicationPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
	{
	}

	private void OnTicketMoveEvent(TicketState curState)
	{
		if (m_TicketMoveEvent != null)
		{
			m_TicketMoveEvent(curState);
		}
	}

	public override void Open()
	{
		base.Open();
		m_CommunicationPort.ReadTimeout = 10000;
		m_CommunicationPort.WriteTimeout = 10000;
		Free();
		Thread thread = new Thread((ThreadStart)delegate
		{
			while (true)
			{
				bool flag = true;
				try
				{
					TicketState ticketState = CheckTicket();
					if (ticketState != LastState)
					{
						LastState = ticketState;
						if (m_TicketMoveEvent != null)
						{
							m_TicketMoveEvent(ticketState);
						}
					}
				}
				catch (Exception)
				{
				}
				Thread.Sleep(500);
			}
		});
		thread.IsBackground = true;
		Thread queryCardStatusThread = thread;
		QueryCardStatusThread = queryCardStatusThread;
		QueryCardStatusThread.Start();
	}

	private byte[] PrepareCommand(string Command)
	{
		byte[] array = new byte[Command.Length + 2];
		array[0] = 2;
		Array.Copy(Encoding.ASCII.GetBytes(Command), 0, array, 1, Command.Length);
		array[array.Length - 1] = 13;
		return array;
	}

	public void Print()
	{
		ProcessSimpleCommand("PRINT");
	}

	private void ProcessSimpleCommand(string command)
	{
		Monitor.Enter(m_OperationSync);
		try
		{
			Logger.Debug("Process Command" + command);
			WriteCom(command);
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
					if (b == 13)
					{
						break;
					}
				}
				else
				{
					flag = true;
				}
			}
			string text = Encoding.ASCII.GetString(list.ToArray(), 0, list.Count - 1);
			Logger.Debug("Response：" + text);
			if (text != "OK")
			{
				list.Clear();
				throw new OperationCanceledException();
			}
			base.CommunicationPort.DiscardInBuffer();
			base.CommunicationPort.DiscardOutBuffer();
			list.Clear();
		}
		catch (Exception)
		{
			throw;
		}
		finally
		{
			Monitor.Exit(m_OperationSync);
		}
	}

	public void PutPrintData(string data)
	{
		ProcessSimpleCommand("PPD" + data);
	}

	public void PutWriteData(string data)
	{
		ProcessSimpleCommand("PWD" + data);
	}

	public string ReadFullTruck()
	{
		string result = string.Empty;
		Monitor.Enter(m_OperationSync);
		try
		{
			WriteCom("READ");
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
					if (b == 13)
					{
						break;
					}
				}
				else
				{
					flag = true;
				}
			}
			string text = Encoding.ASCII.GetString(list.ToArray(), 0, list.Count - 1);
			Thread.Sleep(1000);
			if (text != "OK")
			{
				throw new Exception("讀卡錯誤" + text);
			}
			flag = false;
			list.Clear();
			while (true)
			{
				byte b = (byte)base.CommunicationPort.ReadByte();
				if (b != 2)
				{
					if (flag)
					{
						list.Add(b);
					}
					if (b == 13)
					{
						break;
					}
				}
				else
				{
					flag = true;
				}
			}
			result = Encoding.ASCII.GetString(list.ToArray(), 0, list.Count - 1);
			base.CommunicationPort.DiscardInBuffer();
			base.CommunicationPort.DiscardOutBuffer();
		}
		catch (Exception)
		{
			throw;
		}
		finally
		{
			Monitor.Exit(m_OperationSync);
		}
		return result;
	}

	public void Write_Print()
	{
		ProcessSimpleCommand("WRPRN");
	}

	private void WriteCom(string pf)
	{
		byte[] array = PrepareCommand(pf);
		base.CommunicationPort.DiscardInBuffer();
		base.CommunicationPort.DiscardOutBuffer();
		lock (m_CommunicationSync)
		{
			base.CommunicationPort.Write(array, 0, array.Length);
		}
	}

	public void WriteFullTruck()
	{
		ProcessSimpleCommand("WRITE");
	}
}
