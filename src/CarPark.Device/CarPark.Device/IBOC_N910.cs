using N910POSDll;

namespace CarPark.Device;

public interface IBOC_N910
{
	CheckLineResult CHECKLINE();

	LogonResult LOGON();

	SaleResult SALE(decimal amt);
}
