using System.Text;
using StarMicronics.StarIO;

namespace CarPark2018.Device.CashierBusiness;

public class StarTSP600Printer
{
	private string ComPort;

	private IPort sPort;

	public StarTSP600Printer(string portNo, int Bauadrate)
	{
		ComPort = "LPT1";
		ComPort = portNo;
	}

	private void CutPaper()
	{
		SendCommand("\u001bd\u0002");
	}

	public void Open()
	{
		sPort = Factory.I.GetPort(ComPort, string.Empty, 10000);
		Factory.I.ReleasePort(sPort);
	}

	public void OpenDrawer()
	{
		SendCommand("\a");
	}

	public void Print(string toPrint)
	{
		SendCommand(toPrint);
		CutPaper();
	}

	private void SendCommand(string Command)
	{
		sPort = Factory.I.GetPort(ComPort, string.Empty, 10000);
		byte[] bytes = Encoding.GetEncoding("big5").GetBytes(Command);
		sPort.WritePort(bytes, 0u, (uint)bytes.Length);
		Factory.I.ReleasePort(sPort);
	}
}
