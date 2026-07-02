using System;

namespace CarPark.Device;

public interface IFeeCenterCommunication : IHandContrast
{
	event Action<ParkingSpacesInfo> ParkingSpacesChangeEvent;

	event Action<NoticeInfo> NoticeEvent;

	event Action<DeviceStatusInfo> GateStatusChangeEvent;

	event Action<DisabilityPressInfo> DisabilityPressEvent;

	void ManualChange(ManualChangeInfo args);

	void ManualUpBar(ManualUpBarInfo manualUpBarInfo);

	void UpdateParkAreaExtend(UpdateParkAreaExtendInfo updateParkAreaExtendInfo);

	void RefreshSystem(int args);

	void DisabilityPress(DisabilityPressInfo disabilityPressInfo);
}
