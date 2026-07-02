using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.LPRSInterface;

/// <summary>
/// 車牌識別通信接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ILPRSCallback))]
public interface ILPRSService : IService, ILPRSContrast, ILongConnection
{
	[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
	void Camera(CameraArgs cameraArgs);

	[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
	void CameraRecognition(CameraRecognitionArgs cameraRecognitionArgs);

	[OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
	void RecognitionLicensePlate(RecognitionLicenseArgs recognitionLicenseArgs);
}
