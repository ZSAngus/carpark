using CarPark.Core;

namespace CarPark.Device;

public interface IEnterGate : IGate
{
	void PrintTape(int GateID, string TicketNumber, EnumParkType parkTypeID);

	void SetNextTicketNo(int GateID, string TicketNo);

	void UpdateGateCapacity(int GateID, int countRemain, EnumParkType parkTypeID);

	void SetFreeExitTime(int GateID, int freeminute);
}
