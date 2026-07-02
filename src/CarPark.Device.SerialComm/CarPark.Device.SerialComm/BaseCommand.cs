namespace CarPark.Device.SerialComm;

public abstract class BaseCommand
{
	protected byte[] _Address;

	protected byte[] _CMD_STATUS;

	protected byte[] _DATA;

	protected byte _ETX;

	protected byte _LEN;

	protected byte _STX;

	protected byte _XOR;

	public string Address
	{
		get
		{
			string text = string.Empty;
			byte[] address = _Address;
			foreach (byte b in address)
			{
				text += b.ToString("X2");
			}
			return text;
		}
	}

	public string CMD_STATUS
	{
		get
		{
			string text = string.Empty;
			byte[] cMD_STATUS = _CMD_STATUS;
			foreach (byte b in cMD_STATUS)
			{
				text += b.ToString("X2");
			}
			return text;
		}
	}

	public string DATA
	{
		get
		{
			string text = string.Empty;
			byte[] dATA = _DATA;
			foreach (byte b in dATA)
			{
				text += b.ToString("X2");
			}
			return text;
		}
	}

	public string ETX => _ETX.ToString("X2");

	public short LEN_Short => _LEN;

	public string LEN_String => _LEN.ToString();

	public string STX => _STX.ToString("X2");

	public string XOR => _XOR.ToString("X2");

	protected BaseCommand()
	{
		Class2.WKJkUh2zLspup();
		_STX = 2;
		_XOR = 1;
		_ETX = 3;
	}

	public byte[] toBytes()
	{
		int num = 0;
		byte[] array = new byte[LEN_Short + 1];
		array[num++] = _STX;
		byte[] address = _Address;
		foreach (byte b in address)
		{
			array[num++] = b;
		}
		array[num++] = _LEN;
		for (int j = 0; j < _CMD_STATUS.Length; j++)
		{
			array[num++] = _CMD_STATUS[j];
		}
		byte[] dATA = _DATA;
		foreach (byte b2 in dATA)
		{
			array[num++] = b2;
		}
		array[num++] = _ETX;
		array[num++] = _XOR;
		return array;
	}
}
