namespace CarPark.Device;

public delegate void LoopEventHandler(int gateID, bool switchMotor, bool switchPrivate, bool switchRFID, bool ready);
