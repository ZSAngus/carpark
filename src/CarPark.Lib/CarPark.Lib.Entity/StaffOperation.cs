using System;
using System.Collections.Generic;
using System.Reflection;
using SkyInno.Lang;
using SkyInno.UI.BindingText;

namespace CarPark.Lib.Entity;

public class StaffOperation : IAdditionalDataSource
{
	public const int OPERAT_ADD_CARD = 12;

	public const int OPERAT_ADD_CUSTOMER = 5;

	public const int OPERAT_ADD_RENTAL = 9;

	public const int OPERAT_ADD_STAFF = 15;

	public const int OPERAT_ADD_TENAT = 22;

	public const int OPERAT_ADD_TIMECHARGE = 18;

	public const int OPERAT_DEIT_PARKAREACOUNT = 7;

	public const int OPERAT_DELETE_CARD = 11;

	public const int OPERAT_DELETE_CUSTOMER = 4;

	public const int OPERAT_DELETE_PARKRECORD = 2;

	public const int OPERAT_DELETE_RENTAL = 8;

	public const int OPERAT_DELETE_STAFF = 14;

	public const int OPERAT_DELETE_TENAT = 20;

	public const int OPERAT_DELETE_TIMECHARGE = 17;

	public const int OPERAT_EDIT_CARD = 13;

	public const int OPERAT_EDIT_COMPANYINFO = 3;

	public const int OPERAT_EDIT_CUSTOMER = 6;

	public const int OPERAT_EDIT_RENTAL = 10;

	public const int OPERAT_EDIT_STAFF = 16;

	public const int OPERAT_EDIT_TENAT = 21;

	public const int OPERAT_EDIT_TIMECHARGE = 19;

	public const int OPERAT_MANUAL_OPEN_GATE = 1;

	public const int OPERAT_ADD_TEMPCARD = 30;

	public const int OPERAT_DISCOUNT_TICKET = 31;

	public const int OPERAT_ADD_COMPENSATION_FARE = 32;

	public const int OPERAT_OPEN_DRAWER = 33;

	public const int OPERAT_PARKAREA_FULL = 34;

	public const int OPERAT_HANDLE_EXCEPTIONS = 35;

	public const int OPERAT_ADD_PARK = 36;

	public const int OPERAT_EDIT_PARK = 37;

	public const int OPERAT_DELETE_PARK = 38;

	public const int OPERAT_ADD_TempStop = 39;

	public const int OPERAT_DELETE_TempStop = 40;

	public const int OPERAT_EDIT_TempStop = 41;

	public const int OPERAT_ADD_RuleControl = 42;

	public const int OPERAT_DELETE_RuleControl = 43;

	public const int OPERAT_EDIT_RuleControl = 44;

	public const int OPERAT_ADD_TransactionData = 45;

	public const int OPERAT_ADD_SalaryBlock = 46;

	public const int OPERAT_EDIT_SalaryBlock = 47;

	public const int OPERAT_DELETE_SalaryBlock = 48;

	public const int OPERAT_ADD_FreeRegister = 49;

	public const int OPERAT_EDIT_FreeRegister = 50;

	public const int OPERAT_DELETE_FreeRegister = 51;

	public const int OPERAT_EDIT_TransactionData = 52;

	public const int OPERAT_TIMECHARGE_Refund = 53;

	[BindingControlEditStyle(EnumEditStyle.ConstComboBox, typeof(StaffOperation))]
	public int OperationCode { get; set; }

	public DateTime OperationTime { get; set; }

	public string Remark { get; set; }

	public int ShiftID { get; set; }

	public string StaffCode { get; set; }

	public StaffOperation()
	{
		Class2.sKBPqdpzNwCBA();
	}

	public object GetAdditionanDataSource()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		Type type = GetType();
		FieldInfo[] fields = type.GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			list.Add(new KeyValuePair<int, string>((int)fieldInfo.GetValue(this), LangManager.GetLangString($"{type.FullName}.{fieldInfo.Name}")));
		}
		return list;
	}
}
