namespace DAT.Entity;

public enum EnumPURCHASE_CARD_State
{
	消費完成 = 0,
	消費失敗 = 1,
	消費失敗_协议错误_重试3次失败 = 28,
	讀寫器狀態錯誤 = 29,
	指令超時 = 72
}
