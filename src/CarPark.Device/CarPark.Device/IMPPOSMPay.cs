using MacauPass.POSCom.Package;

namespace CarPark.Device;

public interface IMPPOSMPay
{
	ReloadResult ReloadMPay(decimal amt, string cashType, string valType, string barcode);

	SaleResult SaleMPay(decimal amt, string barcode);

	VoidResult VoidTransactionMPay(string invoiceNo, string TerminalID, decimal amt);
}
