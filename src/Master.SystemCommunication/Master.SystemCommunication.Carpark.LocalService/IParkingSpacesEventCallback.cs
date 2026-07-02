using System.ServiceModel;
using CarPark.DB;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 車位回調
/// </summary>
[ServiceContract]
public interface IParkingSpacesEventCallback
{
	/// <summary>
	/// 車位改變通知
	/// </summary>
	/// <param name="parkAreaExtend"></param>
	[OperationContract]
	void ParkingSpacesChangeNotice(ParkAreaExtend parkAreaExtend);
}
