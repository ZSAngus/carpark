using System.ServiceModel;
using System.ServiceModel.Channels;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.APSInterface;

public class APSMXtraProxy : DuplexClientBase<IAPSMXtraService>, IAPSMXtraService, IService, ILongConnection
{
	public APSMXtraProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public APSMXtraProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public APSMXtraProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public APSMXtraProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public APSMXtraProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public void UploadAPSRunningStatusArgs(APSRunningStatusArgs apsRunningStatusArgs)
	{
		base.Channel.UploadAPSRunningStatusArgs(apsRunningStatusArgs);
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

	public void RunListen()
	{
		base.Channel.RunListen();
	}
}
