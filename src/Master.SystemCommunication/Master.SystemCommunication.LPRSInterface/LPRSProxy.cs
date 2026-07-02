using System.ServiceModel;
using System.ServiceModel.Channels;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.LPRSInterface;

public class LPRSProxy : DuplexClientBase<ILPRSService>, ILPRSService, IService, ILPRSContrast, ILongConnection
{
	public LPRSProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public LPRSProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public LPRSProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public LPRSProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public LPRSProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public bool Join(ProgramInfo programInfo)
	{
		return base.Channel.Join(programInfo);
	}

	public void Connect()
	{
		base.Channel.Connect();
	}

	public void Disconnect()
	{
		base.Channel.Disconnect();
	}

	public void Test()
	{
		base.Channel.Test();
	}

	public void RecordContrast(RecordContrastArgs c_RecordContrast)
	{
		base.Channel.RecordContrast(c_RecordContrast);
	}

	public void ExitContrast(ExitContrastArgs c_ExitContrast)
	{
		base.Channel.ExitContrast(c_ExitContrast);
	}

	public void Camera(CameraArgs cameraArgs)
	{
		base.Channel.Camera(cameraArgs);
	}

	public void CameraRecognition(CameraRecognitionArgs cameraRecognitionArgs)
	{
		base.Channel.CameraRecognition(cameraRecognitionArgs);
	}

	public void RecognitionLicensePlate(RecognitionLicenseArgs recognitionLicenseArgs)
	{
		base.Channel.RecognitionLicensePlate(recognitionLicenseArgs);
	}

	public void RunListen()
	{
		base.Channel.RunListen();
	}
}
