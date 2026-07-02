using System;
using System.Text;

namespace CarPark.Device.SerialComm;

public class ACECommand : BaseCommand
{
	private string m_CMD;

	private byte[] m_DATA;

	private string m_TargetAddress;

	public string CMD => base.CMD_STATUS;

	public string TargetAddress
	{
		get
		{
			return m_TargetAddress;
		}
		set
		{
			m_TargetAddress = value;
			RefreshCommand(m_CMD, m_DATA);
		}
	}

	public ACECommand(string CMD, byte[] DATA)
	{
		Class2.WKJkUh2zLspup();
		m_TargetAddress = "IO1";
		m_CMD = string.Empty;
		m_DATA = new byte[0];
		m_CMD = CMD;
		m_DATA = DATA;
		RefreshCommand(m_CMD, m_DATA);
	}

	public ACECommand(string CMD, string data)
	{
		Class2.WKJkUh2zLspup();
		m_TargetAddress = "IO1";
		m_CMD = string.Empty;
		m_DATA = new byte[0];
		m_CMD = CMD;
		m_DATA = Encoding.ASCII.GetBytes(data);
		RefreshCommand(m_CMD, m_DATA);
	}

	private void RefreshCommand(string CMD, byte[] DATA)
	{
		if (DATA == null)
		{
			DATA = new byte[0];
		}
		_CMD_STATUS = Encoding.ASCII.GetBytes(CMD);
		_DATA = DATA;
		if (_DATA.Length > 120)
		{
			throw new ArgumentOutOfRangeException("DATA数据长度不符");
		}
		_Address = Encoding.ASCII.GetBytes(m_TargetAddress);
		short value = (short)(6 + _CMD_STATUS.Length + _DATA.Length);
		_LEN = BitConverter.GetBytes(value)[0];
		_XOR = 0;
		byte[] address = _Address;
		foreach (byte b in address)
		{
			_XOR ^= b;
		}
		_XOR ^= _LEN;
		for (int j = 0; j < _CMD_STATUS.Length; j++)
		{
			_XOR ^= _CMD_STATUS[j];
		}
		byte[] dATA = _DATA;
		foreach (byte b2 in dATA)
		{
			_XOR ^= b2;
		}
		_XOR ^= _ETX;
	}

	public static byte[] smethod_0(string cmd)
	{
		return Encoding.ASCII.GetBytes(cmd);
	}
}
