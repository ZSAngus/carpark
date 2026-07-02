using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.RFIDInterface;

public interface IRFIDControlCallback : ICallback, ILongConnectionCallBack
{
	[OperationContract]
	void ScanRFIDCallBack(ScanRFIDArgs scanRFIDArgs);
}
