namespace CarPark;

public class PrintEtc
{
	public static string CardChargePrint;

	public static string DepositChargePrint;

	public static string DepositVoidChargePrint;

	public static string MPReloadString;

	public static string MPSaleString;

	public static string PBOCSaleString;

	public static string ShiftInfo;

	public static string TimeChargePrint;

	public static string VoidCardInfo;

	public static string TimeChargeTimeOutPrint;

	public static string TimeChargePrintForYongLi;

	public static string ShiftInfoForYongLi;

	public static string TimeChargeTimeOutPrintForYongLi;

	static PrintEtc()
	{
		ShiftInfo = "Carpark Shift Receipt\n\n更次編號.            {0}\n站臺                 {1}\n開始時間             {2}\n結束時間             {3}\n\n\n-------Transaction-----\n閃付數量     :   {4}\n閃付金額     :   {5}\n澳門通數量   :   {6}\n澳門通金額   :   {7}\n澳門通增值   :   {8}\n增值金額     :   {9}\n時租數量     :   {10}\n時租金額     :   {11}\n失票數量     :   {12}\n失票金額     :   {13}\n坏票數量     :   {14}\n坏票金額     :   {15}\n超時數量     :   {16}\n超時金額     :   {17}\n月租數量     :   {18}\n月租金額     :   {19}\n月租按金     :   {20}\n按金退回     :   {21}\n交易總數     :   {22}\n現金金額     :   {23}\n交易金額     :   {24}\n\n---------免費及全免----\n免費數量     :   {25}\n免費金額     :   {26}\n\n\n---------手工起杆-----\n手工起杆     :   {27}\n\n\nUser ID      :   {27}\n\n---------END-------------\n";
		VoidCardInfo = "Date: {0} \t Time: {1}\n------------------------------------------------ \n\u001bEVoid \n\u001bFCard Number:  \t  \t        {2}\nVoid Amount:  \t  \t        ${3}------------------------------------------------ \nTotal\u0006\t\u001bi\u0001\u0001      ${3}\n\u001bi\0\0------------------------------------------------ \n";
		TimeChargePrint = "Carpark Timecharge Receipt\n{8}\n{9}\n\n時租票號   :           {0}\n入場時間   :           {1}\n繳費時間   :           {2}\n泊車時間   :           {3}\n優惠名稱   :           {10}\n免費時間   :           {4}\n車型       :           {5}\n\n收費金額   :          ${6}\n\n收費員     :           {7}\n\n\n-------End Transaction-----\n\n\n\n\n";
		TimeChargeTimeOutPrint = "Carpark Timecharge Receipt\n{8}\n{9}\n\n時租票號     :           {0}\n入場時間     :           {1}\n上次繳費時間 :           {11}\n本次繳費時間 :           {2}\n泊車時間     :           {3}\n優惠名稱     :           {10}\n免費時間     :           {4}\n車型         :           {5}\n\n收費金額     :          ${6}\n\n收費員       :           {7}\n\n\n-------End Transaction-----\n\n\n\n\n";
		MPReloadString = "歡迎使用澳門通支付平臺\nWelcom to MacauPass Payment Platform\n\n---------------------------\n\n充值 Reload\n收據編號 Invo.No   :{0}\n商戶編號 Merch.ID  :{1}\n終端編號 TID       :{2}\n卡號 CardNo:{3}\n交易時間           :{4}\n原本金額 Orig.Val. :{5}\n充值金額 Trans.Val :{6}\n交易匯率 Excha.Rate:{7}\n實際交易金額 Amount:{8}\n餘額 New Bal.      :{9}\n\n\n-------End Transaction-----\n\n";
		MPSaleString = "歡迎使用澳門通支付平臺\nWelcom to MacauPass Payment Platform\n\n---------------------------\n\n消費 Sale\n收據編號 Invo.No   :{0}\n商戶編號 Merch.ID  :{1}\n終端編號 TID       :{2}\n卡號 CardNo:{3}\n交易時間           :{4}\n原本金額 Orig.Val. :{5}\n交易金額 Trans.Val :{6}\n交易匯率 Excha.Rate:{7}\n實際交易金額 Amount:{8}\n餘額 New Bal.      :{9}\n\n\n-------End Transaction-----\n\n";
		PBOCSaleString = "歡迎使用銀聯閃付支付平臺\nWelcom to QuickPass Payment Platform\n\n---------------------------\n\n卡號 CardNo       :    {0}\n入場時間/EnterTime:    {1}\n離場時間/ExitTime :    {2}\n泊車時間/ParkTime :    {3}\n車型    /ParkType :    {4}\n\n收費金額/Charge   :   ${5}\n\n餘額    /Remain   :    {6}\n-------End Transaction-----\n\n";
		CardChargePrint = "Carpark Card Rental Receipt\n{7}\n{8}\n\n智能卡號    :           {0}\n繳費日期    :           {1}\n開始日期    :           {2}\n到期日期    :           {3}\n車型        :           {4}\n\n月租金額    :          ${5}\n\n收費員      :           {6}\n\n\n-------End Transaction-----\n\n\n\n\n";
		DepositChargePrint = "Carpark Card Deposit Receipt\n\n{4}\n{5}\n智能卡號    :           {0}\n繳費日期    :           {1}\n按金        :          ${2}\n\n\n收費員      :           {3}\n\n\n-------End Transaction-----\n\n\n\n\n";
		DepositVoidChargePrint = "Carpark Card Deposit Void Receipt\n{4}\n{5}\n\n智能卡號    :           {0}\n繳費日期    :           {1}\n按金        :          ${2}\n\n\n收費員      :           {3}\n\n\n-------End Transaction-----\n\n\n\n\n";
		TimeChargePrintForYongLi = "Carpark Timecharge Receipt\r\n{10}\r\n{11}\r\n\r\n時租票號 TICKET no.  : {0}\r\n入場時間 Entry time  : {1}\r\n繳費時間 Payment time: {2}\r\n泊車時間 Parked time : {3}\r\n優惠名稱 SHOP        : {4}\r\n免費時間 FREE TIME   : {5}\r\n車型 PRIVATE VEH.    : {6}\r\n幣種 CURRENCY        : {7}\r\n\r\n收費金額   PAID      : ${8}\r\n\r\n收費員 OPERATOR      : {9}\r\n\r\n\r\n-------End Transaction-----";
		TimeChargeTimeOutPrintForYongLi = "Carpark Timecharge Receipt\r\n{10}\r\n{11}\r\n\r\n時租票號   :           {0}\r\n入場時間   :           {1}\r\n繳費時間   :           {2}\r\n泊車時間   :           {3}\r\n優惠名稱   :           {4}\r\n免費時間   :           {5}\r\n車型       :           {6}\r\n幣種       :           {7}\r\n\r\n收費金額   :          ${8}\r\n\r\n收費員     :           {9}\r\n\r\n\r\n-------End Transaction-----";
		ShiftInfoForYongLi = "Carpark Shift Receipt\r\n\r\n更次編號 SHIFT ID   : {0}\r\n站臺     STATION    : {1}\r\n開始時間 START TIME : {2}\r\n結束時間 END TIME   : {3}\r\n\r\n\r\n-------Transaction-----\r\n時租數量 No.of transaction    :   {4}\r\n時租金額 HOURLY TRANSACTION   :   {5}\r\n\r\n失票數量 No.of Lost Tkt       :   {6}\r\n失票金額 PENALTY              :   {7}\r\n\r\n坏票數量 No.of DAMAGED Tkt    :   {8}\r\n坏票金額 AMOUNT OF DAMAGED Tkt:   {9}\r\n\r\n超時數量 No.of OVER-TIME      :   {10}\r\n超時金額 OVER-TIME PAYMENT    :   {11}\r\n\r\n葡幣數量 MOP TRANSACTIONS     :   {12}\r\n葡幣金額 MOP                  :   {13}\r\n\r\n港幣數量 HKD TRANSACTIONS     :   {14}\r\n港幣金額 HKD                  :   {15}\r\n\r\n港幣數量 HKD TRANSACTIONS     :   {21}\r\n港幣金額 HKD                  :   {22}\r\n\r\n交易總數 TTL TRANSACTIONS     :   {16}\r\n交易總額 TOTAL                :   {17}\r\n\r\n---------免費及全免----\r\n優惠數量 No.of Free hrs       :   {18}\r\n\r\n---------手動起杆-----\r\n手動起杆 MANUAL UP BAR        :   {19}\r\n\r\n\r\nUser ID                       :   {20}\r\n\r\n---------END-------------";
	}
}
