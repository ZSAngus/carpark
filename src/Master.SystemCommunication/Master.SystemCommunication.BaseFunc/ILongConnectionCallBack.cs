using System.ServiceModel;

namespace Master.SystemCommunication.BaseFunc;

/// <summary>
/// 長連回調
/// </summary>
[ServiceContract]
public interface ILongConnectionCallBack
{
	[OperationContract]
	int Listen(string programId);
}
