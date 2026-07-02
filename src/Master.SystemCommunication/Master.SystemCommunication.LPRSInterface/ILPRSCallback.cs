using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.LPRSInterface;

/// <summary>
/// 車牌識別回調
/// </summary>
[ServiceContract]
public interface ILPRSCallback : ICallback, ILPRSContrastCallback, ILongConnectionCallBack
{
	[OperationContract]
	void CameraCallBack(CameraArgs cameraCallBackArgs);

	[OperationContract]
	void CameraRecognitionCallBack(CameraRecognitionArgs cameraRecognitionCallBackArgs);

	[OperationContract]
	void RecognitionLicensePlateCallBack(RecognitionLicenseArgs recognitionLicenseArgs);
}
