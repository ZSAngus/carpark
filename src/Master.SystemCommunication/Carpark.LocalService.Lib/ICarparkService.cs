using System.ServiceModel;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Carpark.LocalService.Lib;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICarparkCallback))]
public interface ICarparkService : IService
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
}
