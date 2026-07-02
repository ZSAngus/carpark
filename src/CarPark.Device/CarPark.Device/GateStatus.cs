using CarPark.Core;
using SkyInno.Lang;

namespace CarPark.Device;

public class GateStatus
{
	public const string ERR_CARD_JAM = "02";

	public const string ERR_MONTHLY_ANTI_PASS = "14";

	public const string ERR_MONTHLY_BLACK = "15";

	public const string ERR_MONTHLY_ERROR = "08";

	public const string ERR_MONTHLY_EXPIRTY_DATE = "11";

	public const string ERR_MONTHLY_IMAGE_ERROR = "16";

	public const string ERR_MONTHLY_TIME_STAT = "10";

	public const string ERR_MONTHLY_TIME_ZONE = "09";

	public const string ERR_NO_CONNECTION = "99";

	public const string ERR_NO_LOOP = "13";

	public const string ERR_NO_TICKET = "98";

	public const string ERR_NOT_PAID = "17";

	public const string ERR_READ_ERROR = "01";

	public const string ERR_WRITE_ERROR = "07";

	public const string ERR_WRITE_HOURLY_TICKET = "12";

	public const string ERR_WRONG_AREA = "06";

	public const string ERR_WRONG_DIRECTION = "03";

	public const string ERR_WRONG_SERIAL = "05";

	public const string STAT_NORMAL = "00";

	public const string STAT_SYSTEM_STARTUP = "04";

	public const string STAT_TICKET_PASS = "20";

	public string ErrCode { get; set; }

	public string ErrDesc => LangManager.GetLangString("ACEStatus." + ErrCode);

	public EnumErrorLevel ErrLvl
	{
		get
		{
			EnumErrorLevel result = EnumErrorLevel.Error;
			if (ErrCode == "00" || ErrCode == "04" || ErrCode == "20")
			{
				result = EnumErrorLevel.Normal;
			}
			return result;
		}
	}

	public int GateID { get; set; }

	public GateStatus()
	{
		Class2.hEE203xzkPmdM();
	}
}
