using System.Collections.Generic;
using System.ServiceModel;
using CarPark.DB;
using Master.Lib.Communication;
using Master.SystemCommunication.BaseFunc;
using Master.SystemCommunication.Extend;
using Master.SystemCommunication.LPRSInterface;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.Carpark.LocalService;

/// <summary>
/// 收費處接口
/// </summary>
[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICashierServiceCallBack))]
public interface ICashierService : ILPRSContrast, IFee, IService, ILongConnection, IDisability, IGateStatusEvent, ISystemEvent, IParkingSpaces, ICommunicationExtend, ILicensePlatePayment
{
	/// <summary>
	/// 系統通知
	/// </summary>
	/// <param name="noticeArgs"></param>
	[OperationContract]
	void SystemNotice(NoticeArgs noticeArgs);

	/// <summary>
	/// 查月租卡信息
	/// </summary>
	/// <param name="getCardInfoArgs"></param>
	/// <returns></returns>
	[OperationContract]
	GetCardInfoReturn GetCardInfo(GetCardInfoArgs getCardInfoArgs, out List<Card> cardList);

	/// <summary>
	/// 獲取在場車輛信息
	/// </summary>
	/// <param name="getView_TransactionAndLPArgs"></param>
	/// <param name="_view_transactionandlp"></param>
	/// <returns></returns>
	[OperationContract]
	GetView_TransactionAndLPReturn GetView_TransactionAndLP(GetView_TransactionAndLPArgs getView_TransactionAndLPArgs, out List<view_transactionandlp> _view_transactionandlp);
}
