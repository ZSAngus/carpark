using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CarPark.Core;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.RFIDInterface;

public class RFIDProxy : DuplexClientBase<IRFIDService>, IRFIDService, IService
{
	public RFIDProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public RFIDProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public RFIDProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public RFIDProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public RFIDProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public bool RFID_OpenGate(RFID_OpenGateArgs rfid_OpenGateArgs)
	{
		return base.Channel.RFID_OpenGate(rfid_OpenGateArgs);
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

	public bool ChangeTimeChargUse(ChangeTimeChargUseArgs changeTimeChargUseArgs, EnumParkType parkType)
	{
		return base.Channel.ChangeTimeChargUse(changeTimeChargUseArgs, parkType);
	}

	public int GetTimeChargRemain(GetTimeChargRemainArgs getTimeChargRemainArgs, EnumParkType parkType)
	{
		return base.Channel.GetTimeChargRemain(getTimeChargRemainArgs, parkType);
	}

	public List<GetParkAreaExtendArgs> GetParkAreaExtend()
	{
		return base.Channel.GetParkAreaExtend();
	}

	public void ShowMessage(ShowMessageArgs showMessageArgs)
	{
		base.Channel.ShowMessage(showMessageArgs);
	}

	public void Beep(BeepArgs beepArgs)
	{
		base.Channel.Beep(beepArgs);
	}
}
