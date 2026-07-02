using System.ServiceModel;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract]
public interface IDisabilityCallBack
{
	[OperationContract]
	void DisabilityPressCallBack(DisabilityPressArgs disabilityPressArgs);
}
