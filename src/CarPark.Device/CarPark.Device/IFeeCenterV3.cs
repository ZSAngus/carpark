using System;

namespace CarPark.Device;

public interface IFeeCenterV3 : IFeeCenterV2, IFeeCenter
{
	event Action<string> QRCodeScanEvent;
}
