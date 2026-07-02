using System;
using DAT.Entity;

namespace CarPark.Device;

public interface IQuickPassTranscation
{
	GET_READERSTATUSdata QueryStatus();

	PBOCReadCardData QueryCard();

	CANCEL_TRANSACTIONdata CancelTransaction();

	PBOCPURCHASECardData Purchase_card(decimal amt, DateTime tradingTime, string CardNumber);

	PBOCPURCHASECardData Retry_purchase(decimal value, decimal value_type, string cardno, DateTime tradingTime);

	SET_PARAMETERdata SetParameter(int value_type);

	GET_CARDHISTORYdata GetCardhistory(int type);

	GET_READERHISTORYdata[] Get_readerhistory(DateTime dateStart, DateTime dateEnd);
}
