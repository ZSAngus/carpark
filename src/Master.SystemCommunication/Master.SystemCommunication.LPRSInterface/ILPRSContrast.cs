using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.LPRSInterface;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ILPRSContrastCallback))]
public interface ILPRSContrast
{
	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void RecordContrast(RecordContrastArgs recordContrast);

	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void ExitContrast(ExitContrastArgs exitContrast);
}
