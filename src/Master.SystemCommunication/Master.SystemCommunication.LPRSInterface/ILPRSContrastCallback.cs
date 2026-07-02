using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.LPRSInterface;

[ServiceContract]
public interface ILPRSContrastCallback
{
	[OperationContract]
	void ExitContrastCallBack(ExitContrastArgs exitContrast);

	[OperationContract]
	void RecordContrastCallBack(RecordContrastArgs recordContrast);
}
