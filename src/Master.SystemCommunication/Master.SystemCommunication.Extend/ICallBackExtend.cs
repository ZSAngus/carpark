using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Extend;

[ServiceContract]
public interface ICallBackExtend
{
	[OperationContract]
	void CallBackExtend(CallBallArgs args);

	[OperationContract]
	void CallBadkExtendResponse(CallBallArgs args);

	[OperationContract]
	void CallBadkExtendByte(CallBallArgs args);
}
