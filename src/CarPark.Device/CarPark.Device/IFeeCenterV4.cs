namespace CarPark.Device;

public interface IFeeCenterV4 : IFeeCenterV3, IFeeCenterV2, IFeeCenter
{
	bool ResetDevices(string args);
}
