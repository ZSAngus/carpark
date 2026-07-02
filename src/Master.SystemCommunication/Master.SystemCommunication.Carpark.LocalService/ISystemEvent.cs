using System.ServiceModel;

namespace Master.SystemCommunication.Carpark.LocalService;

[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ISystemEventCallback))]
public interface ISystemEvent
{
}
