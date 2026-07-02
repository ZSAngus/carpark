using System.ServiceModel;
using System.ServiceModel.Channels;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 车场状态
/// </summary>
public class CarparkStatusServiceProxy : DuplexClientBase<ICarparkStatusService>, ICarparkStatusService, IService, ILongConnection, IGateStatusEvent, ISystemEvent, IParkingSpaces
{
	public CarparkStatusServiceProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public CarparkStatusServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public CarparkStatusServiceProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public CarparkStatusServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public CarparkStatusServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public bool Join(ProgramInfo programInfo)
	{
		return base.Channel.Join(programInfo);
	}

	public void RunListen()
	{
		base.Channel.RunListen();
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
}
