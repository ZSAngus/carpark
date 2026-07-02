using System;

namespace DAT.Entity;

[Serializable]
public class PBOCReadCardData
{
	public string CardPhyType { get; set; }

	public string CardAppType { get; set; }

	public string CardNumber { get; set; }

	public string BillArea { get; set; }

	public string BillAreaB { get; set; }

	public decimal OffLineRemain_MOP { get; set; }

	public decimal OffLineRemain_RMB { get; set; }

	public int IsBlack { get; set; }

	public string ReplyCode { get; set; }

	public string ErrorCode { get; set; }

	public EnumREQUEST_CARD_State State { get; set; }

	public bool Valid { get; set; }

	public string EncryptedCardNumber
	{
		get
		{
			string result = CardNumber;
			try
			{
				result = $"{CardNumber.Substring(0, 6)}******{CardNumber.Substring(CardNumber.Length - 4, 4)}";
			}
			catch (Exception)
			{
			}
			return result;
		}
	}

	public string EncryptedCardNumber6
	{
		get
		{
			string result = CardNumber;
			try
			{
				result = $"{CardNumber.Substring(0, 6)}****{CardNumber.Substring(CardNumber.Length - 6, 6)}";
			}
			catch (Exception)
			{
			}
			return result;
		}
	}
}
