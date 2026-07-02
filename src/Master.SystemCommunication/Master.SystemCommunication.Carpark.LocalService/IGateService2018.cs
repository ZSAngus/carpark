using System.ServiceModel;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IGateService2018CallBack))]
public interface IGateService2018 : ICarpark2018, ILongConnection, IService, IReceiptData, IDeviceMonitoring, ILPRSContrast, IDisability, IParkingSpaces, ICommunicationExtend
{
	/// <summary>
	/// 取閘機信息
	/// </summary>
	/// <param name="getParkGateArgs"></param>
	/// <param name="parkAreaExtends"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = false, IsInitiating = false, IsTerminating = false)]
	GetParkGateReturn GetParkGate(GetParkGateArgs getParkGateArgs, out ParkGate parkAreaExtends);
}
