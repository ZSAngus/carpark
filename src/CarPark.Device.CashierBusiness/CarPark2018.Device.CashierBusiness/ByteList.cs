using System;
using System.Text;

namespace CarPark2018.Device.CashierBusiness;

internal class ByteList
{
	private const byte StartFlag = 2;

	private const byte StopFlag = 13;

	private byte[] m_InnerByte;

	public ByteList()
	{
		m_InnerByte = new byte[0];
	}

	public void AppendBytes(byte[] append)
	{
		if (append != null)
		{
			lock (m_InnerByte)
			{
				byte[] array = new byte[m_InnerByte.Length + append.Length];
				Array.Copy(m_InnerByte, 0, array, 0, m_InnerByte.Length);
				Array.Copy(append, 0, array, m_InnerByte.Length, append.Length);
				m_InnerByte = array;
			}
		}
	}

	public string GetMessage()
	{
		string text = string.Empty;
		lock (m_InnerByte)
		{
			try
			{
				if (Config.SystemConfig.CounterRFIDReaderMode == 1)
				{
					int sourceIndex = Indexof(m_InnerByte, 2);
					int num = Indexof(m_InnerByte, 3);
					byte[] array = new byte[13];
					if (m_InnerByte.Length < array.Length)
					{
						throw new IndexOutOfRangeException("RFID消息格式不正确");
					}
					Array.Copy(m_InnerByte, sourceIndex, array, 0, array.Length);
					byte[] array2 = new byte[m_InnerByte.Length];
					Array.Copy(m_InnerByte, 0, array2, 0, array2.Length);
					m_InnerByte = array2;
					string text2 = Convert.ToString(long.Parse(Encoding.ASCII.GetString(array, 1, array.Length - 3)), 16).PadLeft(8, '0');
					int num2;
					for (num2 = 0; num2 < text2.Length; num2++)
					{
						text = text2.Substring(num2, 2) + text;
						num2++;
					}
					text = text.ToUpper();
					m_InnerByte = new byte[0];
				}
				else
				{
					int sourceIndex = Indexof(m_InnerByte, 2);
					int num = Indexof(m_InnerByte, 13);
					byte[] array = new byte[11];
					if (m_InnerByte.Length < array.Length)
					{
						throw new IndexOutOfRangeException("RFID消息格式不正确");
					}
					Array.Copy(m_InnerByte, sourceIndex, array, 0, array.Length);
					byte[] array2 = new byte[m_InnerByte.Length];
					Array.Copy(m_InnerByte, 0, array2, 0, array2.Length);
					m_InnerByte = array2;
					string text2 = Encoding.ASCII.GetString(array, 1, array.Length - 3).PadLeft(8, '0');
					int num2;
					for (num2 = 0; num2 < text2.Length; num2++)
					{
						text = text2.Substring(num2, 2) + text;
						num2++;
					}
					text = text.ToUpper();
					m_InnerByte = new byte[0];
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return text;
		}
	}

	public bool HasMessage()
	{
		lock (m_InnerByte)
		{
			int num;
			int num2;
			if (Config.SystemConfig.CounterRFIDReaderMode == 1)
			{
				num = Indexof(m_InnerByte, 2);
				num2 = Indexof(m_InnerByte, 3);
				return num >= 0 && num2 >= 0 && num < num2 && num2 + 2 <= m_InnerByte.Length;
			}
			num = Indexof(m_InnerByte, 2);
			num2 = Indexof(m_InnerByte, 13);
			return num >= 0 && num2 > 0 && num < num2;
		}
	}

	private int Indexof(byte[] source, byte index)
	{
		for (int i = 0; i < source.Length; i++)
		{
			if (source[i] == index)
			{
				return i;
			}
		}
		return -1;
	}
}
