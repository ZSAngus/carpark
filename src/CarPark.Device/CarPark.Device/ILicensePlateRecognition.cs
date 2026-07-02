using CarPark.Core;

namespace CarPark.Device;

public interface ILicensePlateRecognition
{
	event LPRecognitionErrorEventHandler LPRecognitionErrorEvent;

	void LPRecognitionError(LPRSData obj);
}
