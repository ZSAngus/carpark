using System.Collections.Generic;
using System.ServiceModel;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.PrintInterface;

/// <summary>
/// 優惠券打印服務
/// Roger Zhang
/// 20180413
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallback))]
public interface ICouponPrintService : IService
{
	/// <summary>
	/// 打印一張優惠券
	/// </summary>
	/// <param name="printCouponArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void PrintCoupon(PrintCouponArgs printCouponArgs);

	/// <summary>
	/// 打印多張優惠券
	/// </summary>
	/// <param name="printCouponArgs"></param>
	/// <returns></returns>
	[OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
	void PrintCouponList(List<PrintCouponArgs> printCouponArgs);
}
