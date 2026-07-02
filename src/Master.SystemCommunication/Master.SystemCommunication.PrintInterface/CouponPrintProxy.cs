using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Master.Lib.Communication;
using Master.SystemCommunication.Lib;

namespace Master.SystemCommunication.PrintInterface;

/// <summary>
/// 優惠券打印服務協議
/// Roger Zhang
/// 20180413
/// </summary>
public class CouponPrintProxy : DuplexClientBase<ICouponPrintService>, ICouponPrintService, IService
{
	public CouponPrintProxy(InstanceContext callbackInstance)
		: base(callbackInstance)
	{
	}

	public CouponPrintProxy(InstanceContext callbackInstance, string endpointConfigurationName)
		: base(callbackInstance, endpointConfigurationName)
	{
	}

	public CouponPrintProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		: base(callbackInstance, binding, remoteAddress)
	{
	}

	public CouponPrintProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public CouponPrintProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		: base(callbackInstance, endpointConfigurationName, remoteAddress)
	{
	}

	public void PrintCoupon(PrintCouponArgs printCouponArgs)
	{
		base.Channel.PrintCoupon(printCouponArgs);
	}

	public void PrintCouponList(List<PrintCouponArgs> printCouponArgs)
	{
		base.Channel.PrintCouponList(printCouponArgs);
	}

	public void Connect()
	{
		base.Channel.Connect();
	}

	public void Disconnect()
	{
		base.Channel.Disconnect();
	}

	public void Test()
	{
		base.Channel.Test();
	}
}
