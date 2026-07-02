using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CarPark.Core;
using CarPark.DB;
using SkyInno.Lang;

namespace CarPark2018;

public class DataBuffer2018
{
	private static List<ParkGate> m_ParkGates;

	private static List<ParkArea> m_ParkAreas;

	private static List<ParkAreaExtend> m_ParkAreaExtends;

	public static List<CardType> CardTypes { get; set; }

	public static List<StaffType> StaffTypes { get; set; }

	public static List<ParkGate> ParkGates
	{
		get
		{
			if (m_ParkGates != null)
			{
				int AreaID = Convert.ToInt32(Config.AreaCode);
				return m_ParkGates.Where((ParkGate m) => m.GateAreaID == AreaID).ToList();
			}
			return null;
		}
		set
		{
			m_ParkGates = value;
		}
	}

	public static List<ParkGate> AllParkGates
	{
		get
		{
			if (m_ParkGates != null)
			{
				return m_ParkGates;
			}
			return null;
		}
		set
		{
			m_ParkGates = value;
		}
	}

	public static List<ParkArea> ParkAreas
	{
		get
		{
			if (m_ParkAreas != null)
			{
				int AreaID = Convert.ToInt32(Config.AreaCode);
				return m_ParkAreas.Where((ParkArea m) => m.AreaID == AreaID).ToList();
			}
			return null;
		}
		set
		{
			m_ParkAreas = value;
		}
	}

	public static List<ParkArea> AllParkAreas
	{
		get
		{
			if (m_ParkAreas != null)
			{
				return m_ParkAreas;
			}
			return null;
		}
		set
		{
			m_ParkAreas = value;
		}
	}

	public static List<ParkAreaExtend> ParkAreaExtends
	{
		get
		{
			if (m_ParkAreaExtends != null)
			{
				int AreaID = Convert.ToInt32(Config.AreaCode);
				return m_ParkAreaExtends.Where((ParkAreaExtend m) => m.AreaID == AreaID).ToList();
			}
			return null;
		}
		set
		{
			m_ParkAreaExtends = value;
		}
	}

	public static ShiftRecord CurrentShiftRecord { get; set; }

	public static CompanyInfo CurrentCompanyInfo { get; set; }

	public static List<SysRole> SysRoles { get; set; }

	public static StaffInfo CurrentStaff { get; set; }

	public static void CheckRole(MethodBase function)
	{
		string text = $"{function.ReflectedType}.{function.Name}";
		foreach (SysRole sysRole in SysRoles)
		{
			if (sysRole.RoleClass == text)
			{
				return;
			}
		}
		throw new Exception(LangManager.GetLangString("ERR_NO_RIGHT"));
	}

	public static bool ShowRole(MethodBase function)
	{
		string text = $"{function.ReflectedType}.{function.Name}";
		foreach (SysRole sysRole in SysRoles)
		{
			if (sysRole.RoleClass == text)
			{
				return true;
			}
		}
		return false;
	}

	public static string GetCarParkSerial(EnumParkType parkType, int AreaID)
	{
		return (from m in m_ParkAreaExtends
			where m.ParkType == parkType && m.AreaID == AreaID
			select m.SerialNo).FirstOrDefault();
	}

	public static string GetCarParkSerialEx(EnumParkType parkType, int GateId)
	{
		int AreaID = (from m in m_ParkGates
			where m.GateID == GateId
			select m.GateAreaID).FirstOrDefault();
		return (from m in m_ParkAreaExtends
			where m.ParkType == parkType && m.AreaID == AreaID
			select m.SerialNo).FirstOrDefault();
	}
}
