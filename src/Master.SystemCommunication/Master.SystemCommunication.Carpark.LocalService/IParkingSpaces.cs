using System.ServiceModel;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IParkingSpacesEventCallback))]
public interface IParkingSpaces
{
}
