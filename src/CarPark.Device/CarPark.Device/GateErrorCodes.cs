using SkyInno.Lang;

namespace CarPark.Device;

public class GateErrorCodes
{
	public const string Already_In = "Already_In";

	public const string Area_Not_Allowed = "Area_Not_Allowed";

	public const string Card_Not_Exist = "Card_Not_Exist";

	public const string Card_Not_Valid = "Card_Not_Valid";

	public const string Charge = "Charge";

	public const string ContactManager = "ContactManager";

	public const string ContactMPass = "ContactMPass";

	public const string Gate_Not_Exist = "Gate_Not_Exist";

	public const string MP_BLS_Card = "MP_BLS_Card";

	public const string MP_CanNot_Trade = "MP_CanNot_Trade";

	public const string MP_DeuLMPC_Failed = "MP_DeuLMPC_Failed";

	public const string MP_Not_Enough_Cash = "MP_Not_Enough_Cash";

	public const string MP_Pay_Before_Exit = "MP_Pay_Before_Exit";

	public const string MP_Welcome = "MP_Welcome";

	public const string No_TimeCharge_Plan = "No_TimeCharge_Plan";

	public const string No_Vehicle_Detected = "No_Vehicle_Detected";

	public const string Not_Enough_Balance = "Not_Enough_Balance";

	public const string Park_Full = "Park_Full";

	public const string ParkAreaExtend_Not_Exist = "ParkAreaExtend_Not_Exist";

	public const string ParkType_Not_Match = "ParkType_Not_Match";

	public const string Pause = "Pause";

	public const string PLS_WAIT = "PleaseWait";

	public const string PrvCarNotLeft = "PrvCarNotLeft";

	public const string PutCard = "PutCard";

	public const string Remain = "Remain";

	public const string RentalType_Not_Exist = "RentalType_Not_Exist";

	public const string ThankYou = "ThankU";

	public const string TryAgain = "TryAgain";

	public const string Vehicle_Not_In = "Vehicle_Not_In";

	public const string WelCome = "WelCom";

	public GateErrorCodes()
	{
		Class2.hEE203xzkPmdM();
	}

	public static string GetCodeDesc(string code)
	{
		return LangManager.GetLangString("GateErrorCodes." + code);
	}

	public static string GetCodeDesc(string code, SysLanguage lang)
	{
		return LangManager.GetLangString("GateErrorCodes." + code, lang);
	}
}
