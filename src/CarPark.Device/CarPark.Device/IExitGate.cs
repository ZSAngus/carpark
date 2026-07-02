namespace CarPark.Device;

public interface IExitGate : IGate
{
	void ControlTicket(int GateID, TicketOperation Operation);
}
