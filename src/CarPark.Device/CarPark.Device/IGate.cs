using System;

namespace CarPark.Device;

public interface IGate
{
	event LackOfPaperEventHandler LackOfPaperEvent;

	event NewVehiclePassEventHandler NewVehiclePassEvent;

	event PassTraceEventHandler PassTraceEvent;

	void Beep(int GateID);

	void CloseGate(int GateID);

	GateLoopInfo CurrtntGateStatus(int GateID);

	GateStatus GetLastError(int GateID);

	void InitDevices();

	void OpenGate(int GateID);

	void PassGate(int GateID, bool isPass);

	void SynTime(DateTime Time);

	void Test();

	void UpdateGateDisplay(int GateID, string content);
}
