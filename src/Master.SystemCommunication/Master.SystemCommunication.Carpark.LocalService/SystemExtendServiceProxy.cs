using System.ServiceModel;
using System.ServiceModel.Channels;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

public class SystemExtendServiceProxy : DuplexClientBase<ISystemExtendService>, ISystemExtendService, IService, ILongConnection, ICommunicationExtend
{
	public SystemExtendServiceProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public SystemExtendServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public SystemExtendServiceProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public SystemExtendServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public SystemExtendServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
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

	public void ExtendRequestInterface(RequestArgs requestArgs)
	{
		base.Channel.ExtendRequestInterface(requestArgs);
	}

	public ResponseArgs ExtendRequestResponseInterface(RequestArgs requestArgs)
	{
		return base.Channel.ExtendRequestResponseInterface(requestArgs);
	}
}
