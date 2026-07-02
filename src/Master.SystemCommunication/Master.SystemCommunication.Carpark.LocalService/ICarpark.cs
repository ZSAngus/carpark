using System;
using System.ServiceModel;
using CarPark.Core;
using CarPark.DB;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 車場普通時月租接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required)]
public interface ICarpark
{
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResult EnterCarMove(int GateID, string realCardNum, int parkType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResult ExitCarMove(int GateID, string realCardNum, int parkType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResult ExitQPassPURCHASE_CARD(ChargeRecord chargeRecord, BOC_Gate_TransactionExtend boc_transaction);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResult ExitMPassDeULMPC(ChargeRecord chargeRecord, MPass_Gate_Transaction mp_transaction);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResult EnterQPassCarMove(int GateID, int parkType, BOC_Gate_TransactionExtend bocTransaction);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResult EnterMPassCarMove(int GateID, int parkType, MPass_Gate_Transaction mpass_Transaction);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	int EnterTicket(int GateID, string TicketNum, EnumParkType parkType, DateTime PassTime, EnumCardType cardType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	int ExitTicket(int GateID, string TicketNum, EnumParkType parkType, DateTime PassTime, EnumCardType cardType);
}
