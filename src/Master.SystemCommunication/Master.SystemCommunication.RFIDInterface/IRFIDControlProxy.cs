using System.ServiceModel;
using System.ServiceModel.Channels;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.RFIDInterface;

public class IRFIDControlProxy : DuplexClientBase<IRFIDControlService>, IRFIDControlService, IService, ILongConnection
{
	public IRFIDControlProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public IRFIDControlProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public IRFIDControlProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public IRFIDControlProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public IRFIDControlProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public void ScanRFID(ScanRFIDArgs scanRFIDArgs)
	{
		base.Channel.ScanRFID(scanRFIDArgs);
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

	public bool Join(ProgramInfo programInfo)
	{
		return base.Channel.Join(programInfo);
	}

	public void RunListen()
	{
		base.Channel.RunListen();
	}
}
