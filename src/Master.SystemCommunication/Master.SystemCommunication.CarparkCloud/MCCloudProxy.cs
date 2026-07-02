using System.ServiceModel;
using System.ServiceModel.Channels;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.CarparkCloud;

public class MCCloudProxy : DuplexClientBase<IMCCloudService>, IMCCloudService, IService, ILongConnection, IDeviceMonitoring
{
	public MCCloudProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public MCCloudProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public MCCloudProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public MCCloudProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public MCCloudProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
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

	public bool UPloadQPassChargeRecord(BOCFileArgs Record)
	{
		return base.Channel.UPloadQPassChargeRecord(Record);
	}

	public void ParkingSpacesChange(ParkingSpacesChangeArgs parkingSpacesChangeArgs)
	{
		base.Channel.ParkingSpacesChange(parkingSpacesChangeArgs);
	}

	public bool UploadPassTrace(PassTraceArgs gatePassTracee)
	{
		return base.Channel.UploadPassTrace(gatePassTracee);
	}

	public bool UploadChargeRecord(ChargeRecordArgs chargeRecord)
	{
		return base.Channel.UploadChargeRecord(chargeRecord);
	}

	public void UploadGateStatus(GateStatusArgs gateStatus)
	{
		base.Channel.UploadGateStatus(gateStatus);
	}

	public void UploadSingleGateStatus(DeviceStatus deviceStatus)
	{
		base.Channel.UploadSingleGateStatus(deviceStatus);
	}

	public void RunListen()
	{
		base.Channel.RunListen();
	}
}
