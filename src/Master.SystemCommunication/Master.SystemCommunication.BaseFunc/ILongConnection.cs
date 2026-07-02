using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.BaseFunc;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ILongConnectionCallBack))]
public interface ILongConnection
{
	[OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
	bool Join(ProgramInfo programInfo);

	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void RunListen();
}
