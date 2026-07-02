using System;
using System.ServiceModel;
using CarPark.Core;
using CarPark.DB;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 車牌流程接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required)]
public interface ILPRSData
{
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX EnterHourly(int GateID, string ticketNo, int parkType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX ExitHourly(int GateID, string ticketNo, int parkType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX CheckMPassExit(int GateID, string cardNum, EnumParkType parkType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX CheckQPassExit(int GateID, string cardNum, EnumParkType parkType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX ExitMPassLPRS(ChargeRecord chargeRecord, MPass_Gate_Transaction mp_transaction, LPRSOptions lprsOptions);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX ExitQPassLPRS(ChargeRecord chargeRecord, BOC_Gate_TransactionExtend boc_transaction, LPRSOptions lprsOptions);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool SaveEnterPasstrace(string PassCode, string CardNum, int gateID, EnumPassStatus status, EnumParkType parktype, int? rentalTypeID, DateTime passTime, int billType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool SaveExitPasstrace(string PassCode, string CardNum, int gateID, EnumPassStatus status, EnumParkType parktype, int? rentalTypeID, DateTime passTime, int billType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool CheckIsOpenGate(LPRSobject obj);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UpdatePassRecord(LPRSobject obj);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	bool UpdatePassTrace(int passID);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX EnterMPassLPRSCarMove(int GateID, int parkType, MPass_Gate_Transaction mpass_Transaction);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX EnterQPassLPRSCarMove(int GateID, int parkType, BOC_Gate_TransactionExtend bocTransaction);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX EnterCarLPRSMove(int GateID, string realCardNum, int parkType);

	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GateResultEX ExitCarLPRSMove(int GateID, string realCardNum, int parkType);
}
