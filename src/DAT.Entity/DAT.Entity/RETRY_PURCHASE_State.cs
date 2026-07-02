namespace DAT.Entity;

public enum RETRY_PURCHASE_State
{
	重試消費完成,
	重試消費失敗,
	重試消費失敗_协议错误_重试3次失败,
	讀寫器狀態錯誤,
	重試消費_指令超時
}
