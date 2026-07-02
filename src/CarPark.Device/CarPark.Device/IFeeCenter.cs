using System.Windows.Forms;
using CarPark.Core;

namespace CarPark.Device;

public interface IFeeCenter
{
	UserControl GetInnerControl { get; }

	event SmartCardReadEvent SmartCardReadEvent;

	event TicketScanEventHandler TicketScanEvent;

	void CalcCurrentQtyInfo();

	EnumParkType CheckParkType(GateLoopInfo status);

	void DisplayFee(string Fee);

	void EjectTicket();

	string GetCarParkSerial(EnumParkType parkType);

	void InitDevices();

	void MakeTicket(FeeInfo info);

	void OpenCash();

	void Print(object printStac);

	void PrintTicket(string TicketNumber, string CardType);

	void UpdateCarCount();
}
