using System;

namespace CarPark.Device;

public interface IFeeCenterV5 : IFeeCenterV4, IFeeCenterV3, IFeeCenterV2, IFeeCenter
{
	event Action<string> QRCodeScanPayEvent;
}
