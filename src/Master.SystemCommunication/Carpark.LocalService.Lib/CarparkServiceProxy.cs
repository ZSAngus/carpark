using System.ServiceModel;
using System.ServiceModel.Channels;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Carpark.LocalService.Lib;

public class CarparkServiceProxy : DuplexClientBase<ICarparkService>, ICarparkService, IService
{
	public CarparkServiceProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public CarparkServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public CarparkServiceProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public CarparkServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public CarparkServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public GateResult EnterCarMove(int GateID, string realCardNum, int parkType)
	{
		return base.Channel.EnterCarMove(GateID, realCardNum, parkType);
	}

	public GateResult ExitCarMove(int GateID, string realCardNum, int parkType)
	{
		return base.Channel.ExitCarMove(GateID, realCardNum, parkType);
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

	public GateResult ExitQPassPURCHASE_CARD(ChargeRecord chargeRecord, BOC_Gate_TransactionExtend boc_transaction)
	{
		return base.Channel.ExitQPassPURCHASE_CARD(chargeRecord, boc_transaction);
	}

	public GateResult ExitMPassDeULMPC(ChargeRecord chargeRecord, MPass_Gate_Transaction mp_transaction)
	{
		return base.Channel.ExitMPassDeULMPC(chargeRecord, mp_transaction);
	}

	public GateResult EnterQPassCarMove(int GateID, int parkType, BOC_Gate_TransactionExtend bocTransaction)
	{
		return base.Channel.EnterQPassCarMove(GateID, parkType, bocTransaction);
	}

	public GateResult EnterMPassCarMove(int GateID, int parkType, MPass_Gate_Transaction mpass_Transaction)
	{
		return base.Channel.EnterMPassCarMove(GateID, parkType, mpass_Transaction);
	}
}
