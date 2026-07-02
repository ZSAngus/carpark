using System;
using System.Text;
using System.Web;
using log4net;

namespace N910POSDll;

public class N910POS : BaseComm
{
	private const int bizTimeout = 30000;

	private const int logTimeout = 360000;

	private const int m_retyyTimes = 4;

	private const int normalTimeout = 10000;

	private ILog Logger;

	private static object m_SynLock;

	public int PortID { get; set; }

	static N910POS()
	{
		m_SynLock = new object();
	}

	public N910POS()
	{
		Logger = LogManager.GetLogger("N910POSLogger");
	}

	public N910POS(string ComPort, int BauadRate)
		: base(ComPort, BauadRate)
	{
		Logger = LogManager.GetLogger("N910POSLogger");
	}

	public override void Open()
	{
		Logger.Info($"Initial Comport connection with port: {base.Comport} Baudrate: {base.BauadRate}");
		base.Open();
		Logger.Info("Port Open Successful");
		m_CommunicationPort.Encoding = Encoding.ASCII;
		m_CommunicationPort.NewLine = CommandConsts.ETX;
		m_CommunicationPort.ReadTimeout = 61000;
	}

	public CheckLineResult CHECKLINE()
	{
		lock (m_SynLock)
		{
			Logger.Info("Start to CheckLine");
			CheckLineResult checkLineResult = new CheckLineResult();
			CheckLineCommand checkLineCommand = new CheckLineCommand();
			m_CommunicationPort.Encoding = Encoding.ASCII;
			m_CommunicationPort.NewLine = CommandConsts.ETX;
			m_CommunicationPort.ReadTimeout = 5000;
			try
			{
				if (!m_CommunicationPort.IsOpen)
				{
					Logger.Info("Port Re Open Begin..");
					base.Open();
					Logger.Info("Port Re Open End..");
				}
				WriteCommand(checkLineCommand.ToCommand);
				string receivedPackage = ReadLine();
				CheckLineResult checkLineResult2 = checkLineResult.FromString<CheckLineResult>(receivedPackage);
				if (!(checkLineResult2.REQUESTID == checkLineCommand.REQUESTID))
				{
					checkLineResult.MESSAGE = "REQUESTID not Match";
					throw new ArgumentException("REQUESTID not Match");
				}
				checkLineResult = checkLineResult2;
				checkLineResult.MESSAGE = HttpUtility.UrlDecode(checkLineResult2.MESSAGE.ToString(), Encoding.GetEncoding("utf-8"));
			}
			catch (Exception message)
			{
				Logger.Info("Communication fail CHECKLINE");
				Logger.Info(message);
			}
			return checkLineResult;
		}
	}

	public LogonResult LOGON()
	{
		lock (m_SynLock)
		{
			Logger.Info("Start to Logon");
			LogonResult logonResult = new LogonResult();
			LogonCommand logonCommand = new LogonCommand();
			m_CommunicationPort.ReadTimeout = 30000;
			try
			{
				WriteCommand(logonCommand.ToCommand);
				string receivedPackage = ReadLine();
				LogonResult logonResult2 = logonResult.FromString<LogonResult>(receivedPackage);
				if (!(logonResult2.REQUESTID == logonCommand.REQUESTID))
				{
					logonResult2.MESSAGE = "REQUESTID not Match";
					throw new ArgumentException("REQUESTID not Match");
				}
				logonResult = logonResult2;
				logonResult.MESSAGE = HttpUtility.UrlDecode(logonResult2.MESSAGE.ToString(), Encoding.GetEncoding("utf-8"));
			}
			catch (Exception message)
			{
				Logger.Info("Communication fail LOGON");
				Logger.Info(message);
			}
			return logonResult;
		}
	}

	public SaleResult SALE(decimal amt)
	{
		lock (m_SynLock)
		{
			DateTime now = DateTime.Now;
			Logger.Info("Start to Sale，Amount : " + amt);
			SaleResult saleResult = new SaleResult();
			SaleCommand saleCommand = new SaleCommand(amt);
			m_CommunicationPort.ReadTimeout = 65000;
			try
			{
				WriteCommand(saleCommand.ToCommand);
				string receivedPackage = ReadLine();
				SaleResult saleResult2 = saleResult.FromString<SaleResult>(receivedPackage);
				if (!(saleResult2.REQUESTID == saleCommand.REQUESTID))
				{
					saleResult.MESSAGE = "REQUESTID not Match";
					throw new ArgumentException("REQUESTID not Match");
				}
				saleResult = saleResult2;
				saleResult.MESSAGE = HttpUtility.UrlDecode(saleResult2.MESSAGE.ToString(), Encoding.GetEncoding("utf-8"));
			}
			catch (Exception message)
			{
				Logger.Info("Communication fail SALE");
				Logger.Info(message);
			}
			finally
			{
			}
			return saleResult;
		}
	}

	private string ReadLine()
	{
		string text = m_CommunicationPort.ReadLine();
		Logger.Info($"Receive:<=={text}{m_CommunicationPort.NewLine}");
		return text;
	}

	private void WriteCommand(string command)
	{
		m_CommunicationPort.DiscardInBuffer();
		m_CommunicationPort.Write(command);
		Logger.Info("Send:==>" + command);
	}
}
