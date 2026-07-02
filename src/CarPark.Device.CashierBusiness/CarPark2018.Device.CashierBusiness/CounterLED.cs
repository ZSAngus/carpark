using System.Text;
using CarPark.Device.SerialComm;

namespace CarPark2018.Device.CashierBusiness;

public class CounterLED : BaseComm
{
	public CounterLED()
	{
	}

	public CounterLED(string Comport, int BauadRate)
		: base(Comport, BauadRate)
	{
	}

	public void DisplayCash(string cash)
	{
		byte[] array = SendCommand(cash);
		base.CommunicationPort.Write(array, 0, array.Length);
	}

	private byte[] SendCommand(string content)
	{
		char c = '\r';
		byte[] bytes = Encoding.ASCII.GetBytes("D01");
		byte[] bytes2 = Encoding.ASCII.GetBytes(content);
		byte[] array = new byte[bytes2.Length + 5];
		int num = 0;
		array[0] = 2;
		num = 1;
		for (int i = 0; i < bytes.Length; i++)
		{
			array[num] = bytes[i];
			num++;
		}
		for (int i = 0; i < bytes2.Length; i++)
		{
			array[num] = bytes2[i];
			num++;
		}
		array[num] = (byte)c;
		return array;
	}
}
