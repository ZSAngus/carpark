using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CarPark.DB.Context;
using log4net;

namespace CarPark.DB;

public class DataBuffer
{
	private static Entities _DBContext = null;

	private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	private static List<SysRole> m_CurrentRoles = new List<SysRole>();

	private static ShiftRecord m_CurrentShiftRecord;

	private static StaffInfo m_CurrentStaff = null;

	private static List<CustomFreeTenat> m_CustomFreeTenats = new List<CustomFreeTenat>();

	private static List<CustomFreeType> m_CustomFreeTypes = new List<CustomFreeType>();

	private static List<ParkAreaExtend> m_ParkAreaExtends = new List<ParkAreaExtend>();

	private static List<ParkArea> m_ParkAreas = new List<ParkArea>();

	private static List<ParkGate> m_ParkGates = new List<ParkGate>();

	private static List<RentalType> m_RentalTypes = new List<RentalType>();

	private static List<StaffType> m_StaffTypes = new List<StaffType>();

	private static List<TimeCharge> m_TimeCharges = new List<TimeCharge>();

	private static List<CardType> m_CardTypes = new List<CardType>();

	private static List<Park> m_Parks = new List<Park>();

	private static string m_FromStation = Environment.MachineName;

	public static bool IsMoreCashier = false;

	public static string PayStationName = string.Empty;

	private static int m_SystemCode = 1;

	private static string m_APPOnlyID = string.Empty;

	private static List<ChargeItem> m_ChargeItems = new List<ChargeItem>();

	public static string APPOnlyID
	{
		get
		{
			return m_APPOnlyID;
		}
		set
		{
			m_APPOnlyID = value;
		}
	}

	public static int SystemCode
	{
		get
		{
			return m_SystemCode;
		}
		set
		{
			m_SystemCode = value;
		}
	}

	[CompilerGenerated]
	public static CompanyInfo CompanyInfo { get; set; }

	public static List<SysRole> CurrentRoles => m_CurrentRoles;

	public static ShiftRecord CurrentShiftRecord
	{
		get
		{
			if (!string.IsNullOrEmpty(PayStationName))
			{
				UpdateCurrentShiftRecord(null, PayStationName);
			}
			else if (IsMoreCashier)
			{
				UpdateCurrentShiftRecord(null, Environment.MachineName);
			}
			else
			{
				UpdateCurrentShiftRecord(null);
			}
			return m_CurrentShiftRecord;
		}
		set
		{
			m_CurrentShiftRecord = value;
		}
	}

	public static StaffInfo CurrentStaff
	{
		get
		{
			return m_CurrentStaff;
		}
		set
		{
			m_CurrentStaff = value;
			UpdateRoles();
		}
	}

	public static List<CustomFreeTenat> CustomFreeTenats => m_CustomFreeTenats;

	public static List<CustomFreeType> CustomFreeTypes => m_CustomFreeTypes;

	public static Entities DBContext
	{
		get
		{
			if (_DBContext == null)
			{
				_DBContext = CarPark.DB.Context.DBContext.NewContext;
			}
			return _DBContext;
		}
	}

	public static List<ParkAreaExtend> ParkAreaExtends => m_ParkAreaExtends;

	public static List<ParkArea> ParkAreas => m_ParkAreas;

	public static List<ParkGate> ParkGates => m_ParkGates;

	public static List<RentalType> RentalTypes => m_RentalTypes;

	public static List<StaffType> StaffTypes => m_StaffTypes;

	public static List<TimeCharge> TimeCharges => m_TimeCharges;

	public static List<CardType> CardTypes => m_CardTypes;

	public static List<Park> parks => m_Parks;

	public static List<ChargeItem> ChargeItems => m_ChargeItems;

	public static List<KeyValuePair<int, string>> GetParkAreaBindings()
	{
		List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
		foreach (ParkArea parkArea in m_ParkAreas)
		{
			list.Add(new KeyValuePair<int, string>(parkArea.AreaID, parkArea.AreaName));
		}
		return list;
	}

	public static void UpdateAll()
	{
		UpdateCompanyInfo();
		UpdateStaffTypes();
		UpdateParkAreas();
		UpdateParkAreaExtends();
		UpdateRentalTypes();
		UpdateTimeCharges();
		UpdateCustomFreeTypes();
		UpdateParkGates();
		UpdateCustomFreeTenats();
		UpdateCurrentShiftRecord(null);
		UpdateCardTypes();
		UpdateParks();
	}

	public static void UpdateCompanyInfo()
	{
		try
		{
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			CompanyInfo = entities.CompanyInfo.OrderBy((CompanyInfo m) => m.CompanyID).First((CompanyInfo m) => m.IsDelete == false);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateCurrentShiftRecord(ShiftRecord record)
	{
		try
		{
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			if (record == null)
			{
				for (m_CurrentShiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => !m.IsComplete); m_CurrentShiftRecord == null; m_CurrentShiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => !m.IsComplete))
				{
					ShiftRecord shiftRecord = new ShiftRecord();
					shiftRecord.IsComplete = false;
					shiftRecord.StartTime = DateTime.Now;
					ShiftRecord shiftRecord2 = shiftRecord;
					ShiftRecord shiftRecord3 = shiftRecord2;
					if (CurrentStaff != null)
					{
						shiftRecord3.StartStaffCode = CurrentStaff.StaffCode;
					}
					entities.ShiftRecord.AddObject(shiftRecord3);
					entities.SaveChanges();
				}
			}
			else
			{
				ShiftRecord dest = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => m.ShiftID == record.ShiftID);
				EntityHelper.CopyEntity(record, dest);
				entities.SaveChanges();
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateCurrentShiftRecord(ShiftRecord record, string fromStation)
	{
		try
		{
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			if (record == null)
			{
				for (m_CurrentShiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => !m.IsComplete && m.FromStation == fromStation); m_CurrentShiftRecord == null; m_CurrentShiftRecord = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => !m.IsComplete && m.FromStation == fromStation))
				{
					ShiftRecord shiftRecord = new ShiftRecord();
					shiftRecord.IsComplete = false;
					shiftRecord.StartTime = DateTime.Now;
					shiftRecord.FromStation = fromStation;
					ShiftRecord shiftRecord2 = shiftRecord;
					ShiftRecord shiftRecord3 = shiftRecord2;
					if (CurrentStaff != null)
					{
						shiftRecord3.StartStaffCode = CurrentStaff.StaffCode;
					}
					entities.ShiftRecord.AddObject(shiftRecord3);
					entities.SaveChanges();
				}
			}
			else
			{
				ShiftRecord dest = entities.ShiftRecord.FirstOrDefault((ShiftRecord m) => m.ShiftID == record.ShiftID && m.FromStation == fromStation);
				EntityHelper.CopyEntity(record, dest);
				entities.SaveChanges();
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateCurrentShiftRecordByPublic(ShiftRecord record, string fromStation)
	{
		if (!string.IsNullOrEmpty(PayStationName))
		{
			UpdateCurrentShiftRecord(record, PayStationName);
		}
		else if (IsMoreCashier)
		{
			UpdateCurrentShiftRecord(record, fromStation);
		}
		else
		{
			UpdateCurrentShiftRecord(record);
		}
	}

	public static void UpdateCustomFreeTenats()
	{
		try
		{
			m_CustomFreeTenats.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (CustomFreeTenat item in entities.CustomFreeTenat.Where((CustomFreeTenat m) => m.IsDelete == false))
			{
				m_CustomFreeTenats.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateCustomFreeTypes()
	{
		try
		{
			m_CustomFreeTypes.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (CustomFreeType item in entities.CustomFreeType.Where((CustomFreeType m) => m.IsDelete == false))
			{
				m_CustomFreeTypes.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateParkAreaExtends()
	{
		try
		{
			m_ParkAreaExtends.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (ParkAreaExtend item in entities.ParkAreaExtend.Where((ParkAreaExtend m) => m.IsDelete == false))
			{
				m_ParkAreaExtends.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateParkAreas()
	{
		try
		{
			m_ParkAreas.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (ParkArea item in (IEnumerable<ParkArea>)entities.ParkArea)
			{
				m_ParkAreas.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateParkGates()
	{
		try
		{
			m_ParkGates.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (ParkGate item in entities.ParkGate.Where((ParkGate m) => m.IsDelete == false))
			{
				m_ParkGates.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateRentalTypes()
	{
		try
		{
			m_RentalTypes.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (RentalType item in entities.RentalType.Where((RentalType m) => m.IsDelete == false))
			{
				m_RentalTypes.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateRoles()
	{
		if (m_CurrentStaff == null)
		{
			m_CurrentRoles.Clear();
			return;
		}
		using Entities entities = CarPark.DB.Context.DBContext.NewContext;
		IQueryable<int> myRoles = from Roles in entities.SysStaffRole
			where Roles.StaffTypeID == m_CurrentStaff.StaffTypeId
			select Roles.RoleID;
		m_CurrentRoles = entities.SysRole.Where((SysRole sysroles) => myRoles.Contains(sysroles.RoleID) && sysroles.InUse).ToList();
	}

	public static void UpdateStaffTypes()
	{
		try
		{
			m_StaffTypes.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (StaffType item in entities.StaffType.Where((StaffType m) => m.IsDelete == false))
			{
				m_StaffTypes.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateTimeCharges()
	{
		try
		{
			m_TimeCharges.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (TimeCharge item in (IEnumerable<TimeCharge>)entities.TimeCharge)
			{
				m_TimeCharges.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateCardTypes()
	{
		try
		{
			m_CardTypes.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (CardType item in entities.CardType.Where((CardType m) => m.IsDelete == false))
			{
				m_CardTypes.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	public static void UpdateParks()
	{
		try
		{
			m_Parks.Clear();
			using Entities entities = CarPark.DB.Context.DBContext.NewContext;
			foreach (Park item in entities.Park.Where((Park m) => m.IsDelete == false))
			{
				m_Parks.Add(item);
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}
}
