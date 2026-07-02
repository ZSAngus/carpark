namespace CarPark2018;

public class ReceiptObj
{
	public int mpDecalCount;

	public decimal mpDecalAmt;

	public int cashTotalTimeChargeCnt;

	public decimal cashTotalTimeChargeAmt;

	public int cashLostCnt;

	public decimal cashLostAmt;

	public int cashDamageCnt;

	public decimal cashDamageAmt;

	public int cashTimeoutCnt;

	public decimal cashTimeoutAmt;

	public int cashPaymentConversionCnt;

	public decimal cashPaymentConversionAmt;

	public int macauPassTotalTimeChargeCnt;

	public decimal macauPassTotalTimeChargeAmt;

	public int macauPassLostCnt;

	public decimal macauPassLostAmt;

	public int macauPassDamageCnt;

	public decimal macauPassDamageAmt;

	public int macauPassTimeoutCnt;

	public decimal macauPassTimeoutAmt;

	public int macauPassPaymentConversionCnt;

	public decimal macauPassPaymentConversionAmt;

	public int macauPassOutCnt;

	public decimal macauPassOutAmt;

	public int quickPassTotalTimeChargeCnt;

	public decimal quickPassTotalTimeChargeAmt;

	public int quickPassLostCnt;

	public decimal quickPassLostAmt;

	public int quickPassDamageCnt;

	public decimal quickPassDamageAmt;

	public int quickPassTimeoutCnt;

	public decimal quickPassTimeoutAmt;

	public int quickPassPaymentConversionCnt;

	public decimal quickPassPaymentConversionAmt;

	public int quickPassOutCnt;

	public decimal quickPassOutAmt;

	public int NoSenseCnt;

	public decimal NoSenseAmt;

	public int cashRentalCnt;

	public decimal cashRentalAmt;

	public int cashCnt => mpDecalCount + cashTotalTimeChargeCnt + cashLostCnt + cashDamageCnt + cashTimeoutCnt + cashPaymentConversionCnt + cashRentalCnt;

	public decimal cashAmt => mpDecalAmt + cashTotalTimeChargeAmt + cashLostAmt + cashDamageAmt + cashTimeoutAmt + cashPaymentConversionAmt;

	public int mpassCnt => macauPassTotalTimeChargeCnt + macauPassLostCnt + macauPassDamageCnt + macauPassTimeoutCnt + macauPassPaymentConversionCnt + macauPassOutCnt;

	public decimal mpassAmt => macauPassTotalTimeChargeAmt + macauPassLostAmt + macauPassDamageAmt + macauPassTimeoutAmt + macauPassPaymentConversionAmt + macauPassOutAmt;

	public int quickPassCnt => quickPassTotalTimeChargeCnt + quickPassLostCnt + quickPassDamageCnt + quickPassTimeoutCnt + quickPassPaymentConversionCnt + quickPassOutCnt;

	public decimal quickAmt => quickPassTotalTimeChargeAmt + quickPassLostAmt + quickPassDamageAmt + quickPassTimeoutAmt + quickPassPaymentConversionAmt + quickPassOutAmt;
}
