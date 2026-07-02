using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract]
public interface ISystemExtendServiceCallBack : ICallback, ILongConnectionCallBack, ICallBackExtend
{
}
