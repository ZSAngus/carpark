using System;
using System.IO;

namespace N910POSDll;

public class CommandConsts
{
	private static long m_TransactionSEQ;

	internal static string ETX => "\"<END>\"";

	public static string POS_IN_UPDATE => "307";

	public static string POS_NOT_SIGN => "305";

	internal static string STX => "\"<START>\"";

	public static string TransactionSEQ
	{
		get
		{
			m_TransactionSEQ = Convert.ToInt64(DateTime.Now.ToString("yyMMddhhmmss"));
			if (File.Exists("N910POSSEQ.dat"))
			{
				long num = 0L;
				try
				{
					string s = File.ReadAllText("N910POSSEQ.dat");
					num = long.Parse(s);
				}
				catch (Exception)
				{
					num = 0L;
				}
				if (m_TransactionSEQ == num)
				{
					m_TransactionSEQ++;
				}
			}
			File.WriteAllText("N910POSSEQ.dat", m_TransactionSEQ.ToString());
			return m_TransactionSEQ.ToString().PadLeft(12, '0');
		}
	}

	static CommandConsts()
	{
		m_TransactionSEQ = 0L;
	}
}
