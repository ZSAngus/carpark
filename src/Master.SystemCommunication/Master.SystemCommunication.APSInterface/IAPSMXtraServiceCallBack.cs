using System.Collections.Generic;
using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.APSInterface;

public interface IAPSMXtraServiceCallBack : ICallback, ILongConnectionCallBack
{
	[OperationContract]
	bool ControlAPS(ControlAPSArgs controlAPSArgs);

	[OperationContract]
	List<CashBoxStatusArgs> GetCashBoxStatus();
}
