namespace DAT.Entity;

public enum GET_READERHISTORY_State
{
	查詢成功,
	查詢失敗_無交易記錄,
	查詢失敗,
	查詢讀卡器記錄失敗_协议错误_重试3次失败,
	讀寫器狀態錯誤,
	查詢失敗_指令超時
}
