namespace CarPark.Device;

public interface IPBOCBillSync
{
	void SyncBill(int GateID);

	void SyncStatus(int GateID);
}
