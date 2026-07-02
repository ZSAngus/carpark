using CarPark.Core;
using MacauPass.POSCom.Package;

namespace CarPark.Device;

public interface IMPPOSTranscation
{
	ActiveResult Active();

	string CalcPayment(decimal source, EnumPaymentRate sourceRate, EnumPaymentRate targetRate);

	LogOffResult Logoff();

	LogOnResult Logon();

	QueryResult QueryCard(int TransactionCount);

	ReloadResult Reload(decimal amt, string cashType, string valType);

	SaleResult Sale(decimal amt);

	SignTransactionsResult SignInTransactions();
}
