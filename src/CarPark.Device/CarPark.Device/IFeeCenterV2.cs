namespace CarPark.Device;

public interface IFeeCenterV2 : IFeeCenter
{
	event TicketStateChangeEvent TicketStateChangeEvent;
}
