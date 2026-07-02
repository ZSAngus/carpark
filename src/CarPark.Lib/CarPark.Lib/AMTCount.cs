using System;
using System.Collections.Generic;
using System.Linq;
using CarPark.DB;
using CarPark.DB.Context;

namespace CarPark.Lib;

public class AMTCount
{
	private static Dictionary<int, PeriodofTimeInfo> ptinfo = new Dictionary<int, PeriodofTimeInfo>();

	public AMTCount()
	{
		Class2.sKBPqdpzNwCBA();
	}

	public static ChargeRecord CalcMonthCharge(RentalType type, int rentalMonth)
	{
		ChargeRecord chargeRecord = new ChargeRecord();
		chargeRecord.ParkTypeID = type.ParkTypeID;
		chargeRecord.TotalCharge = type.NormalCharge * (decimal)rentalMonth;
		return chargeRecord;
	}

	public static ChargeRecord CalcTimeCharge(TransactionData data, DateTime chargeTime, CustomFreeType freetype, bool useDefaultFree, bool isTimeOut = false, CustomFreeTenat customFreeTenat = null)
	{
		return CalcTimeChargeForUM(data, chargeTime, freetype, useDefaultFree, isTimeOut, customFreeTenat);
	}

	public static decimal calcCharge(TimeSpan firseMins, TimeSpan FreeMins, TimeSpan TotalMins, decimal firstMoney, decimal NorMoney, List<TimeChargeExt> ext, DateTime InTime, DateTime chargeTime, TimeCharge chargeType)
	{
		decimal num = 0m;
		TimeSpan timeSpan = ((firseMins > FreeMins) ? firseMins : FreeMins);
		TimeSpan timeSpan2 = firseMins;
		if (TotalMins < firseMins)
		{
			timeSpan2 = TotalMins;
		}
		decimal num2 = (decimal)Math.Ceiling((timeSpan2 - FreeMins).TotalHours);
		decimal num3 = (decimal)Math.Ceiling((TotalMins - timeSpan).TotalHours);
		if (num2 < 0m)
		{
			num2 = 0m;
		}
		if (num3 < 0m)
		{
			num3 = 0m;
		}
		decimal num4 = num2 * firstMoney;
		decimal num5 = num3 * NorMoney;
		if (ext != null && ext.Count > 0 && (TotalMins - timeSpan).TotalHours > 0.0)
		{
			UpdateTime(InTime, ext, chargeType);
			num5 = ChargeLog(InTime.AddMinutes(timeSpan.TotalMinutes), chargeTime, ext, chargeType);
		}
		return num4 + num5;
	}

	public static decimal calcChargeEX(TimeSpan firseMins, TimeSpan FreeMins, TimeSpan TotalMins, decimal firstMoney, decimal NorMoney, List<TimeChargeExt> ext, DateTime InTime, DateTime chargeTime, TimeCharge chargeType)
	{
		decimal num = 0m;
		TimeSpan timeSpan = ((firseMins > FreeMins) ? firseMins : FreeMins);
		TimeSpan timeSpan2 = firseMins;
		if (TotalMins < firseMins)
		{
			timeSpan2 = TotalMins;
		}
		decimal num2 = (decimal)Math.Ceiling((timeSpan2 - FreeMins).TotalHours);
		decimal num3 = (decimal)Math.Ceiling((TotalMins - timeSpan).TotalHours);
		if (num2 < 0m)
		{
			num2 = 0m;
		}
		if (num3 < 0m)
		{
			num3 = 0m;
		}
		decimal num4 = num2 * firstMoney;
		decimal num5 = num3 * NorMoney;
		if (ext != null && ext.Count > 0 && (TotalMins - timeSpan).TotalHours > 0.0)
		{
			UpdateTime(InTime, ext, chargeType);
			num5 = ChargeLogEX2(InTime.AddMinutes(timeSpan.TotalMinutes), chargeTime, ext, chargeType);
		}
		return num4 + num5;
	}

	public static decimal ChargeLog(DateTime start, DateTime end, List<TimeChargeExt> ext, TimeCharge charge)
	{
		Func<TimeChargeExt, bool> func = null;
		decimal num = 0m;
		while (start < end)
		{
			func = ((!(start < charge.StartDate)) ? ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= start.Hour && m.EndHR > start.Hour && m.AfterFlag)) : ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= start.Hour && m.EndHR > start.Hour && !m.AfterFlag)));
			TimeChargeExt timeChargeExt = ext.FirstOrDefault(func);
			if (timeChargeExt != null)
			{
				num += timeChargeExt.Charge;
				if (ptinfo.ContainsKey(timeChargeExt.StartHR))
				{
					ptinfo[timeChargeExt.StartHR]._Minute++;
					ptinfo[timeChargeExt.StartHR]._Money += timeChargeExt.Charge;
				}
			}
			else
			{
				num += charge.NormalCharge;
			}
			Console.WriteLine("num=" + num);
			start = start.AddHours(1.0);
		}
		return num;
	}

	public static decimal ChargeLogEX(DateTime start, DateTime end, List<TimeChargeExt> ext, TimeCharge charge)
	{
		Func<TimeChargeExt, bool> func = null;
		decimal num = 0m;
		while (start < end)
		{
			func = ((!(start < charge.StartDate)) ? ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= start.Hour && m.EndHR > start.Hour && m.AfterFlag)) : ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= start.Hour && m.EndHR > start.Hour && !m.AfterFlag)));
			TimeChargeExt timeChargeExt = ext.FirstOrDefault(func);
			if (timeChargeExt != null)
			{
				if (charge.ParkTypeID == 1 && timeChargeExt.AfterFlag)
				{
					if (start.Hour >= 8 && start.Hour < 20)
					{
						if ((end - start).TotalMinutes <= 30.0 && (end - start).TotalMinutes > 0.0)
						{
							num += timeChargeExt.Charge / 2m;
						}
						else
						{
							DateTime temp2 = start.AddHours(0.5);
							Func<TimeChargeExt, bool> predicate = (TimeChargeExt m) => m.StartHR <= temp2.Hour && m.EndHR > temp2.Hour && m.AfterFlag;
							TimeChargeExt timeChargeExt2 = ext.FirstOrDefault(predicate);
							if (timeChargeExt2 != null && timeChargeExt != timeChargeExt2)
							{
								num += timeChargeExt.Charge / 2m + timeChargeExt2.Charge;
							}
							else
							{
								num += timeChargeExt.Charge;
							}
						}
					}
					else
					{
						num += timeChargeExt.Charge;
					}
				}
				else
				{
					num += timeChargeExt.Charge;
				}
				if (ptinfo.ContainsKey(timeChargeExt.StartHR))
				{
					ptinfo[timeChargeExt.StartHR]._Minute++;
					ptinfo[timeChargeExt.StartHR]._Money += timeChargeExt.Charge;
				}
			}
			else
			{
				num += charge.NormalCharge;
			}
			Console.WriteLine("num=" + num);
			start = start.AddHours(1.0);
		}
		return num;
	}

	public static decimal ChargeLogEX2(DateTime start, DateTime end, List<TimeChargeExt> ext, TimeCharge charge)
	{
		Func<TimeChargeExt, bool> func = null;
		decimal num = 0m;
		if (charge.ParkTypeID == 1)
		{
			num = CalPrivate(start, end, ext, charge);
		}
		else
		{
			DateTime temp = start;
			while (temp < end)
			{
				func = ((!(temp < charge.StartDate)) ? ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= temp.Hour && m.EndHR > temp.Hour && m.AfterFlag)) : ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= temp.Hour && m.EndHR > temp.Hour && !m.AfterFlag)));
				TimeChargeExt timeChargeExt = ext.FirstOrDefault(func);
				if (timeChargeExt != null)
				{
					num += timeChargeExt.Charge;
					if (ptinfo.ContainsKey(timeChargeExt.StartHR))
					{
						ptinfo[timeChargeExt.StartHR]._Minute++;
						ptinfo[timeChargeExt.StartHR]._Money += timeChargeExt.Charge;
					}
				}
				else
				{
					num += charge.NormalCharge;
				}
				temp = temp.AddHours(1.0);
			}
			Console.WriteLine("num=" + num);
		}
		return num;
	}

	private static decimal CalPrivate(DateTime start, DateTime end, List<TimeChargeExt> ext, TimeCharge charge)
	{
		Func<TimeChargeExt, bool> func = null;
		decimal num = 0m;
		while (start < end)
		{
			func = ((!(start < charge.StartDate)) ? ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= start.Hour && m.EndHR > start.Hour && m.AfterFlag)) : ((Func<TimeChargeExt, bool>)((TimeChargeExt m) => m.StartHR <= start.Hour && m.EndHR > start.Hour && !m.AfterFlag)));
			TimeChargeExt timeChargeExt = ext.FirstOrDefault(func);
			if (timeChargeExt != null)
			{
				if (charge.ParkTypeID == 1 && timeChargeExt.AfterFlag)
				{
					if (start.Hour >= 8 && start.Hour < 20)
					{
						num += timeChargeExt.Charge / 2m;
					}
					else
					{
						num += timeChargeExt.Charge;
					}
				}
				else
				{
					num += timeChargeExt.Charge;
				}
				if (ptinfo.ContainsKey(timeChargeExt.StartHR))
				{
					ptinfo[timeChargeExt.StartHR]._Minute++;
					ptinfo[timeChargeExt.StartHR]._Money += timeChargeExt.Charge;
				}
			}
			else
			{
				num += charge.NormalCharge;
			}
			if (start.Hour >= 8 && start.Hour < 20)
			{
				start = start.AddHours(0.5);
			}
			else
			{
				start = start.AddHours(1.0);
			}
		}
		Console.WriteLine("num=" + num);
		return num;
	}

	public static void UpdateTime(DateTime start, List<TimeChargeExt> ext, TimeCharge charge)
	{
		ptinfo.Clear();
		List<TimeChargeExt> list = null;
		list = ((!(start < charge.StartDate)) ? ext.FindAll((TimeChargeExt m) => m.AfterFlag) : ext.FindAll((TimeChargeExt m) => !m.AfterFlag));
		if (list == null)
		{
			return;
		}
		foreach (TimeChargeExt item in list)
		{
			string periodofTimeName = $"{item.StartHR}:00>{item.EndHR}:00";
			if (!ptinfo.ContainsKey(item.StartHR))
			{
				ptinfo.Add(item.StartHR, new PeriodofTimeInfo
				{
					PeriodofTimeName = periodofTimeName
				});
			}
		}
	}

	public static ChargeRecord CalcTimeChargeForNorm(TransactionData data, DateTime chargeTime, CustomFreeType freetype, bool useDefaultFree, bool isTimeOut = false, CustomFreeTenat customFreeTenat = null)
	{
		TimeCharge chargeType = DataBuffer.TimeCharges.FirstOrDefault((TimeCharge m) => m.ParkTypeID == data.ParkTypeID);
		if (chargeType == null)
		{
			throw new NotSupportedException($"No TimeCharge for parktype:{data.ParkTypeID}");
		}
		TimeSpan timeSpan = chargeTime - data.InTime;
		TimeSpan timeSpan2 = timeSpan;
		List<TimeChargeExt> ext = null;
		bool flag = false;
		if (timeSpan2.TotalMinutes > (double)DataBuffer.CompanyInfo.BufferTime && DataBuffer.CompanyInfo.BufferTime > 0)
		{
			chargeTime = chargeTime.AddMinutes(-DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
			timeSpan2 = timeSpan;
			flag = true;
		}
		if (useDefaultFree && timeSpan.Minutes < DataBuffer.CompanyInfo.DefaultFreeMin)
		{
			timeSpan2 = new TimeSpan(timeSpan.Hours, 0, 0);
		}
		using (Entities entities = DBContext.NewContext)
		{
			_ = DateTime.Now >= chargeType.StartDate;
			IQueryable<TimeChargeExt> source = entities.TimeChargeExt.Where((TimeChargeExt m) => m.ParkTypeID == chargeType.ParkTypeID && m.Enabled);
			if (source.Count() > 0)
			{
				ext = source.ToList();
			}
		}
		decimal num = 0m;
		decimal num2 = 0m;
		int freeMin = 0;
		new TimeSpan(0, 0, 0);
		TimeSpan timeSpan3 = new TimeSpan(0, 0, 0);
		TimeSpan timeSpan4 = new TimeSpan(0, 0, 0);
		decimal firstMoney = 0m;
		if (chargeType.FirstMin > 0 && !isTimeOut)
		{
			timeSpan3 = new TimeSpan(0, chargeType.FirstMin, 0);
			firstMoney = chargeType.FirstNormalCharge;
		}
		if (freetype != null && freetype.IsFreeByTime)
		{
			timeSpan4 = new TimeSpan(0, freetype.FreeMinutes, 0);
			freeMin = freetype.FreeMinutes;
		}
		_ = timeSpan3 > timeSpan4;
		num = calcCharge(timeSpan3, new TimeSpan(0, 0, 0), timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
		num2 = calcCharge(timeSpan3, timeSpan4, timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
		if (flag)
		{
			chargeTime = chargeTime.AddMinutes(DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
		}
		timeSpan2 = timeSpan.Subtract(timeSpan4);
		if (freetype != null)
		{
			if (freetype.IsAllFree)
			{
				timeSpan2 = new TimeSpan(0, 0, 0);
				num2 = 0m;
				freeMin = (int)Math.Ceiling(timeSpan.TotalMinutes);
			}
			else if (freetype.IsEmployeesPrice)
			{
				freeMin = 0;
				num2 = (decimal)Math.Ceiling(timeSpan2.TotalHours) * freetype.EmployeesAmount;
			}
			else
			{
				num2 -= freetype.FreeAmount;
			}
		}
		if (timeSpan2.TotalMinutes < 0.0)
		{
			timeSpan2 = new TimeSpan(0, 0, 0);
		}
		if (num2 < 0m)
		{
			num2 = 0m;
		}
		int num3 = (int)Math.Ceiling(timeSpan2.TotalMinutes);
		if (flag)
		{
			num3 -= DataBuffer.CompanyInfo.BufferTime;
		}
		ChargeRecord chargeRecord = new ChargeRecord();
		chargeRecord.ParkTimeSpan = timeSpan;
		chargeRecord.ParkTypeID = data.ParkTypeID;
		chargeRecord.ChargeTime = chargeTime;
		chargeRecord.FreeCharge = num - num2;
		chargeRecord.FreeMin = freeMin;
		chargeRecord.StaffCode = ((DataBuffer.CurrentStaff == null) ? "Auto" : DataBuffer.CurrentStaff.StaffCode);
		chargeRecord.ShiftID = DataBuffer.CurrentShiftRecord.ShiftID;
		chargeRecord.TotalCharge = num2;
		chargeRecord.TransactionID = data.TransactionID;
		chargeRecord.ChargeMin = ((num3 > 0) ? num3 : 0);
		chargeRecord.CardCode = data.InCardCode;
		chargeRecord.FromStation = Environment.MachineName;
		chargeRecord.Remark = ((customFreeTenat == null) ? "" : customFreeTenat.TenatNameCn);
		chargeRecord.BufferTime = DataBuffer.CompanyInfo.BufferTime;
		ChargeRecord chargeRecord2 = chargeRecord;
		chargeRecord2.ParkMin = (int)Math.Ceiling((chargeTime - data.InTime).TotalMinutes);
		if (ptinfo.Count > 0)
		{
			ptinfo = ptinfo.OrderBy((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key).ToDictionary((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key, (KeyValuePair<int, PeriodofTimeInfo> p) => p.Value);
			chargeRecord2.PeriodofTimeInfoList = ptinfo.Values.ToList();
			chargeRecord2.PeriodofTime = chargeRecord2.GetPeriodofTime;
		}
		else
		{
			chargeRecord2.PeriodofTime = "";
		}
		ptinfo.Clear();
		return chargeRecord2;
	}

	public static ChargeRecord CalcTimeChargeForXiaHuanJie(TransactionData data, DateTime chargeTime, CustomFreeType freetype, bool useDefaultFree, bool isTimeOut = false, CustomFreeTenat customFreeTenat = null)
	{
		TimeCharge chargeType = DataBuffer.TimeCharges.FirstOrDefault((TimeCharge m) => m.ParkTypeID == data.ParkTypeID);
		if (chargeType == null)
		{
			throw new NotSupportedException($"No TimeCharge for parktype:{data.ParkTypeID}");
		}
		TimeSpan timeSpan = chargeTime - data.InTime;
		TimeSpan timeSpan2 = timeSpan;
		List<TimeChargeExt> ext = null;
		bool flag = false;
		if (useDefaultFree && timeSpan.Minutes < DataBuffer.CompanyInfo.DefaultFreeMin)
		{
			timeSpan2 = new TimeSpan(timeSpan.Hours, 0, 0);
		}
		if (timeSpan2.TotalMinutes > (double)DataBuffer.CompanyInfo.BufferTime && DataBuffer.CompanyInfo.BufferTime > 0)
		{
			chargeTime = chargeTime.AddMinutes(-DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
			timeSpan2 = timeSpan;
			flag = true;
		}
		using (Entities entities = DBContext.NewContext)
		{
			_ = DateTime.Now >= chargeType.StartDate;
			IQueryable<TimeChargeExt> source = entities.TimeChargeExt.Where((TimeChargeExt m) => m.ParkTypeID == chargeType.ParkTypeID && m.Enabled);
			if (source.Count() > 0)
			{
				ext = source.ToList();
			}
		}
		decimal num = 0m;
		decimal num2 = 0m;
		int freeMin = 0;
		new TimeSpan(0, 0, 0);
		TimeSpan timeSpan3 = new TimeSpan(0, 0, 0);
		TimeSpan timeSpan4 = new TimeSpan(0, 0, 0);
		decimal firstMoney = 0m;
		if (chargeType.FirstMin > 0 && !isTimeOut)
		{
			switch (chargeType.ParkTypeID)
			{
			case 1:
				if (data.InTime < chargeType.StartDate)
				{
					timeSpan3 = new TimeSpan(0, chargeType.FirstMin, 0);
					firstMoney = chargeType.FirstNormalChargeA;
				}
				else if (data.InTime.Hour >= 8 && data.InTime.Hour < 20)
				{
					timeSpan3 = new TimeSpan(0, chargeType.FirstMin, 0);
					firstMoney = chargeType.FirstNormalCharge;
				}
				break;
			case 2:
			case 3:
				if (data.InTime < chargeType.StartDate)
				{
					timeSpan3 = new TimeSpan(0, chargeType.FirstMin, 0);
					firstMoney = chargeType.FirstNormalChargeA;
				}
				else
				{
					timeSpan3 = new TimeSpan(0, chargeType.FirstMin, 0);
					firstMoney = chargeType.FirstNormalCharge;
				}
				break;
			}
		}
		if (freetype != null && freetype.IsFreeByTime)
		{
			timeSpan4 = new TimeSpan(0, freetype.FreeMinutes, 0);
			freeMin = freetype.FreeMinutes;
		}
		_ = timeSpan3 > timeSpan4;
		num = calcChargeEX(timeSpan3, new TimeSpan(0, 0, 0), timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
		num2 = calcChargeEX(timeSpan3, timeSpan4, timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
		if (flag)
		{
			chargeTime = chargeTime.AddMinutes(DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
		}
		timeSpan2 = timeSpan.Subtract(timeSpan4);
		if (freetype != null)
		{
			if (freetype.IsAllFree)
			{
				timeSpan2 = new TimeSpan(0, 0, 0);
				num2 = 0m;
				freeMin = (int)Math.Ceiling(timeSpan.TotalMinutes);
			}
			else if (freetype.IsEmployeesPrice)
			{
				freeMin = 0;
				num2 = (decimal)Math.Ceiling(timeSpan2.TotalHours) * freetype.EmployeesAmount;
			}
			else
			{
				num2 -= freetype.FreeAmount;
			}
		}
		if (chargeType.ParkTypeID == 3 && timeSpan2.TotalMinutes <= 30.0)
		{
			timeSpan2 = new TimeSpan(0, 0, 0);
			num2 = 0m;
			freeMin = (int)Math.Ceiling(timeSpan.TotalMinutes);
		}
		if (timeSpan2.TotalMinutes < 0.0)
		{
			timeSpan2 = new TimeSpan(0, 0, 0);
		}
		if (num2 < 0m)
		{
			num2 = 0m;
		}
		int num3 = (int)Math.Ceiling(timeSpan2.TotalMinutes);
		if (flag)
		{
			num3 -= DataBuffer.CompanyInfo.BufferTime;
		}
		ChargeRecord chargeRecord = new ChargeRecord();
		chargeRecord.ParkTimeSpan = timeSpan;
		chargeRecord.ParkTypeID = data.ParkTypeID;
		chargeRecord.ChargeTime = chargeTime;
		chargeRecord.FreeCharge = num - num2;
		chargeRecord.FreeMin = freeMin;
		chargeRecord.StaffCode = ((DataBuffer.CurrentStaff == null) ? "Auto" : DataBuffer.CurrentStaff.StaffCode);
		chargeRecord.ShiftID = DataBuffer.CurrentShiftRecord.ShiftID;
		chargeRecord.TotalCharge = num2;
		chargeRecord.TransactionID = data.TransactionID;
		chargeRecord.ChargeMin = ((num3 > 0) ? num3 : 0);
		chargeRecord.CardCode = data.InCardCode;
		chargeRecord.FromStation = Environment.MachineName;
		chargeRecord.Remark = ((customFreeTenat == null) ? "" : customFreeTenat.TenatNameCn);
		chargeRecord.BufferTime = DataBuffer.CompanyInfo.BufferTime;
		ChargeRecord chargeRecord2 = chargeRecord;
		chargeRecord2.ParkMin = (int)Math.Ceiling((chargeTime - data.InTime).TotalMinutes);
		if (ptinfo.Count > 0)
		{
			ptinfo = ptinfo.OrderBy((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key).ToDictionary((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key, (KeyValuePair<int, PeriodofTimeInfo> p) => p.Value);
			chargeRecord2.PeriodofTimeInfoList = ptinfo.Values.ToList();
			chargeRecord2.PeriodofTime = chargeRecord2.GetPeriodofTime;
		}
		else
		{
			chargeRecord2.PeriodofTime = "";
		}
		ptinfo.Clear();
		return chargeRecord2;
	}

	public static ChargeRecord CalcTimeChargeForHaiYang(TransactionData data, DateTime chargeTime, CustomFreeType freetype, bool useDefaultFree, bool isTimeOut = false, CustomFreeTenat customFreeTenat = null)
	{
		TimeCharge chargeType = DataBuffer.TimeCharges.FirstOrDefault((TimeCharge m) => m.ParkTypeID == data.ParkTypeID);
		if (chargeType == null)
		{
			throw new NotSupportedException($"No TimeCharge for parktype:{data.ParkTypeID}");
		}
		TimeSpan timeSpan = chargeTime - data.InTime;
		TimeSpan timeSpan2 = timeSpan;
		List<TimeChargeExt> ext = null;
		bool flag = false;
		if (timeSpan2.TotalMinutes > (double)DataBuffer.CompanyInfo.BufferTime && DataBuffer.CompanyInfo.BufferTime > 0)
		{
			chargeTime = chargeTime.AddMinutes(-DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
			timeSpan2 = timeSpan;
			flag = true;
		}
		if (useDefaultFree && timeSpan.Minutes < DataBuffer.CompanyInfo.DefaultFreeMin)
		{
			timeSpan2 = new TimeSpan(timeSpan.Hours, 0, 0);
		}
		using (Entities entities = DBContext.NewContext)
		{
			_ = DateTime.Now >= chargeType.StartDate;
			IQueryable<TimeChargeExt> source = entities.TimeChargeExt.Where((TimeChargeExt m) => m.ParkTypeID == chargeType.ParkTypeID && m.Enabled);
			if (source.Count() > 0)
			{
				ext = source.ToList();
			}
		}
		decimal num = 0m;
		decimal num2 = 0m;
		int num3 = 0;
		new TimeSpan(0, 0, 0);
		TimeSpan firseMins = new TimeSpan(0, 0, 0);
		TimeSpan timeSpan3 = new TimeSpan(0, 0, 0);
		decimal firstMoney = 0m;
		TimeSpan discountMins = new TimeSpan(0, 0, 0);
		decimal num4 = 0m;
		int num5 = 0;
		if (chargeType.FirstMin > 0 && !isTimeOut)
		{
			firseMins = new TimeSpan(0, chargeType.FirstMin, 0);
			firstMoney = chargeType.FirstNormalCharge;
		}
		num = calcCharge(firseMins, new TimeSpan(0, 0, 0), timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
		num2 = num;
		if (freetype != null)
		{
			if (freetype.IsFreeByTime)
			{
				timeSpan3 = new TimeSpan(0, freetype.FreeMinutes, 0);
				num3 = freetype.FreeMinutes;
				num2 = calcCharge(firseMins, timeSpan3, timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
			}
			else if (freetype.IsDiscountByAmount.HasValue && freetype.IsDiscountByAmount.Value)
			{
				num4 = ((!freetype.DiscountAmount.HasValue) ? 0m : freetype.DiscountAmount.Value);
				discountMins = new TimeSpan(0, freetype.DiscountMinutes.HasValue ? freetype.DiscountMinutes.Value : 0, 0);
				num2 = calcChargeForDiscountByAmount(firseMins, discountMins, timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType, num4);
				num5 = ((discountMins.TotalMinutes != 0.0) ? freetype.DiscountMinutes.Value : ((int)Math.Ceiling(timeSpan.TotalMinutes)));
			}
			if (freetype.FreeAmount > 0m)
			{
				num2 -= freetype.FreeAmount;
			}
		}
		if (flag)
		{
			chargeTime = chargeTime.AddMinutes(DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
		}
		timeSpan2 = timeSpan.Subtract(timeSpan3);
		if (freetype != null)
		{
			if (freetype.IsAllFree)
			{
				timeSpan2 = new TimeSpan(0, 0, 0);
				num2 = 0m;
				num3 = (int)Math.Ceiling(timeSpan.TotalMinutes);
			}
			else if (freetype.IsEmployeesPrice)
			{
				num3 = 0;
				num2 = (decimal)Math.Ceiling(timeSpan2.TotalHours) * freetype.EmployeesAmount;
			}
		}
		if (timeSpan2.TotalMinutes < 0.0)
		{
			timeSpan2 = new TimeSpan(0, 0, 0);
		}
		if (num2 < 0m)
		{
			num2 = 0m;
		}
		int num6 = (int)Math.Ceiling(timeSpan2.TotalMinutes);
		if (flag)
		{
			num6 -= DataBuffer.CompanyInfo.BufferTime;
		}
		ChargeRecord chargeRecord = new ChargeRecord();
		chargeRecord.ParkTimeSpan = timeSpan;
		chargeRecord.ParkTypeID = data.ParkTypeID;
		chargeRecord.ChargeTime = chargeTime;
		chargeRecord.FreeCharge = num - num2;
		chargeRecord.FreeMin = ((num3 >= num5) ? num3 : num5);
		chargeRecord.StaffCode = ((DataBuffer.CurrentStaff == null) ? "Auto" : DataBuffer.CurrentStaff.StaffCode);
		chargeRecord.ShiftID = DataBuffer.CurrentShiftRecord.ShiftID;
		chargeRecord.TotalCharge = num2;
		chargeRecord.TransactionID = data.TransactionID;
		chargeRecord.ChargeMin = ((num6 > 0) ? num6 : 0);
		chargeRecord.CardCode = data.InCardCode;
		chargeRecord.FromStation = Environment.MachineName;
		chargeRecord.Remark = ((customFreeTenat == null) ? "" : customFreeTenat.TenatNameCn);
		chargeRecord.BufferTime = DataBuffer.CompanyInfo.BufferTime;
		ChargeRecord chargeRecord2 = chargeRecord;
		chargeRecord2.ParkMin = (int)Math.Ceiling((chargeTime - data.InTime).TotalMinutes);
		if (ptinfo.Count > 0)
		{
			ptinfo = ptinfo.OrderBy((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key).ToDictionary((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key, (KeyValuePair<int, PeriodofTimeInfo> p) => p.Value);
			chargeRecord2.PeriodofTimeInfoList = ptinfo.Values.ToList();
			chargeRecord2.PeriodofTime = chargeRecord2.GetPeriodofTime;
		}
		else
		{
			chargeRecord2.PeriodofTime = "";
		}
		ptinfo.Clear();
		return chargeRecord2;
	}

	public static decimal calcChargeForDiscountByAmount(TimeSpan firseMins, TimeSpan discountMins, TimeSpan TotalMins, decimal firstMoney, decimal NorMoney, List<TimeChargeExt> ext, DateTime InTime, DateTime chargeTime, TimeCharge chargeType, decimal discountCharge)
	{
		decimal num = 0m;
		TimeSpan timeSpan = ((firseMins > discountMins) ? firseMins : discountMins);
		TimeSpan timeSpan2 = firseMins;
		if (TotalMins < firseMins)
		{
			timeSpan2 = TotalMins;
		}
		TimeSpan timeSpan3 = discountMins;
		if (TotalMins < discountMins)
		{
			timeSpan3 = TotalMins;
		}
		decimal num2 = 0m;
		decimal num3 = 0m;
		decimal num4 = 0m;
		if (firstMoney > 0m)
		{
			num2 = (decimal)Math.Ceiling((timeSpan2 - timeSpan3).TotalHours);
			num3 = (decimal)Math.Ceiling(timeSpan3.TotalHours);
			num4 = (decimal)Math.Ceiling((TotalMins - timeSpan).TotalHours);
		}
		else
		{
			num3 = (decimal)Math.Ceiling(timeSpan3.TotalHours);
			num4 = (decimal)Math.Ceiling((TotalMins - firseMins - timeSpan3).TotalHours);
		}
		if (num2 < 0m)
		{
			num2 = 0m;
		}
		if (num3 < 0m)
		{
			num3 = 0m;
		}
		if (num4 < 0m)
		{
			num4 = 0m;
		}
		decimal num5 = num2 * firstMoney + num3 * discountCharge;
		decimal num6 = num4 * NorMoney;
		if (ext != null && ext.Count > 0 && (TotalMins - timeSpan).TotalHours > 0.0)
		{
			UpdateTime(InTime, ext, chargeType);
			num6 = ChargeLog(InTime.AddMinutes(timeSpan.TotalMinutes), chargeTime, ext, chargeType);
		}
		num = num5 + num6;
		if (discountMins.TotalMinutes == 0.0)
		{
			num = (decimal)Math.Ceiling(TotalMins.TotalHours) * discountCharge;
		}
		return num;
	}

	public static ChargeRecord CalcTimeChargeForUM(TransactionData data, DateTime chargeTime, CustomFreeType freetype, bool useDefaultFree, bool isTimeOut = false, CustomFreeTenat customFreeTenat = null, int areaID = 0)
	{
		TimeCharge chargeType = DataBuffer.TimeCharges.FirstOrDefault(delegate(TimeCharge m)
		{
			if (m.ParkTypeID == data.ParkTypeID && m.AreaID == areaID)
			{
				int? chargeType2 = m.ChargeType;
				if (chargeType2.GetValueOrDefault() == 0)
				{
					return chargeType2.HasValue;
				}
				return false;
			}
			return false;
		});
		if (chargeType == null)
		{
			throw new NotSupportedException($"No TimeCharge for parktype:{data.ParkTypeID}");
		}
		TimeSpan timeSpan = chargeTime - data.InTime;
		TimeSpan timeSpan2 = timeSpan;
		List<TimeChargeExt> ext = null;
		bool flag = false;
		if (timeSpan2.TotalMinutes > (double)DataBuffer.CompanyInfo.BufferTime && DataBuffer.CompanyInfo.BufferTime > 0)
		{
			chargeTime = chargeTime.AddMinutes(-DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
			timeSpan2 = timeSpan;
			flag = true;
		}
		if (useDefaultFree && timeSpan.Minutes < DataBuffer.CompanyInfo.DefaultFreeMin)
		{
			timeSpan2 = new TimeSpan(timeSpan.Hours, 0, 0);
		}
		using (Entities entities = DBContext.NewContext)
		{
			_ = DateTime.Now >= chargeType.StartDate;
			IQueryable<TimeChargeExt> source = entities.TimeChargeExt.Where((TimeChargeExt m) => m.ParkTypeID == chargeType.ParkTypeID && m.Enabled && m.TimeChargeTypeID == (int?)chargeType.TimeChargeTypeID);
			if (source.Count() > 0)
			{
				ext = source.ToList();
			}
		}
		decimal num = 0m;
		decimal num2 = 0m;
		int num3 = 0;
		new TimeSpan(0, 0, 0);
		TimeSpan firseMins = new TimeSpan(0, 0, 0);
		TimeSpan timeSpan3 = new TimeSpan(0, 0, 0);
		decimal firstMoney = 0m;
		TimeSpan discountMins = new TimeSpan(0, 0, 0);
		decimal num4 = 0m;
		int num5 = 0;
		if (chargeType.FirstMin > 0 && !isTimeOut)
		{
			firseMins = new TimeSpan(0, chargeType.FirstMin, 0);
			firstMoney = chargeType.FirstNormalCharge;
		}
		num = calcCharge(firseMins, new TimeSpan(0, 0, 0), timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
		num2 = num;
		if (freetype != null)
		{
			if (freetype.IsFreeByTime)
			{
				timeSpan3 = new TimeSpan(0, freetype.FreeMinutes, 0);
				num3 = freetype.FreeMinutes;
				num2 = calcCharge(firseMins, timeSpan3, timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType);
			}
			else if (freetype.IsDiscountByAmount.HasValue && freetype.IsDiscountByAmount.Value)
			{
				num4 = ((!freetype.DiscountAmount.HasValue) ? 0m : freetype.DiscountAmount.Value);
				discountMins = new TimeSpan(0, freetype.DiscountMinutes.HasValue ? freetype.DiscountMinutes.Value : 0, 0);
				num2 = calcChargeForDiscountByAmount(firseMins, discountMins, timeSpan, firstMoney, chargeType.NormalCharge, ext, data.InTime, chargeTime, chargeType, num4);
				num5 = ((discountMins.TotalMinutes != 0.0) ? freetype.DiscountMinutes.Value : ((int)Math.Ceiling(timeSpan.TotalMinutes)));
			}
			else if (freetype.IsDiscountByPercentage.HasValue && freetype.IsDiscountByPercentage.Value)
			{
				num2 = Math.Round(num * freetype.DiscountPercentage.Value, 2);
			}
			if (freetype.FreeAmount > 0m)
			{
				num2 -= freetype.FreeAmount;
			}
		}
		if (flag)
		{
			chargeTime = chargeTime.AddMinutes(DataBuffer.CompanyInfo.BufferTime);
			timeSpan = chargeTime - data.InTime;
		}
		timeSpan2 = timeSpan.Subtract(timeSpan3);
		if (freetype != null)
		{
			if (freetype.IsAllFree)
			{
				timeSpan2 = new TimeSpan(0, 0, 0);
				num2 = 0m;
				num3 = (int)Math.Ceiling(timeSpan.TotalMinutes);
			}
			else if (freetype.IsEmployeesPrice)
			{
				num3 = 0;
				num2 = (decimal)Math.Ceiling(timeSpan2.TotalHours) * freetype.EmployeesAmount;
			}
		}
		if (timeSpan2.TotalMinutes < 0.0)
		{
			timeSpan2 = new TimeSpan(0, 0, 0);
		}
		if (num2 < 0m)
		{
			num2 = 0m;
		}
		int num6 = (int)Math.Ceiling(timeSpan2.TotalMinutes);
		if (flag)
		{
			num6 -= DataBuffer.CompanyInfo.BufferTime;
		}
		ChargeRecord chargeRecord = new ChargeRecord();
		chargeRecord.ParkTimeSpan = timeSpan;
		chargeRecord.ParkTypeID = data.ParkTypeID;
		chargeRecord.ChargeTime = chargeTime;
		chargeRecord.FreeCharge = num - num2;
		chargeRecord.FreeMin = ((num3 >= num5) ? num3 : num5);
		chargeRecord.StaffCode = ((DataBuffer.CurrentStaff == null) ? "Auto" : DataBuffer.CurrentStaff.StaffCode);
		chargeRecord.ShiftID = DataBuffer.CurrentShiftRecord.ShiftID;
		chargeRecord.TotalCharge = num2;
		chargeRecord.TransactionID = data.TransactionID;
		chargeRecord.ChargeMin = ((num6 > 0) ? num6 : 0);
		chargeRecord.CardCode = data.InCardCode;
		chargeRecord.FromStation = Environment.MachineName;
		chargeRecord.Remark = ((customFreeTenat == null) ? "" : customFreeTenat.TenatNameCn);
		chargeRecord.BufferTime = DataBuffer.CompanyInfo.BufferTime;
		ChargeRecord chargeRecord2 = chargeRecord;
		chargeRecord2.ParkMin = (int)Math.Ceiling((chargeTime - data.InTime).TotalMinutes);
		if (ptinfo.Count > 0)
		{
			ptinfo = ptinfo.OrderBy((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key).ToDictionary((KeyValuePair<int, PeriodofTimeInfo> p) => p.Key, (KeyValuePair<int, PeriodofTimeInfo> p) => p.Value);
			chargeRecord2.PeriodofTimeInfoList = ptinfo.Values.ToList();
			chargeRecord2.PeriodofTime = chargeRecord2.GetPeriodofTime;
		}
		else
		{
			chargeRecord2.PeriodofTime = "";
		}
		ptinfo.Clear();
		return chargeRecord2;
	}

	public static ChargeRecord CalcTimeCharge(TransactionData data, DateTime chargeTime, CustomFreeType freetype, bool useDefaultFree, bool isTimeOut = false, CustomFreeTenat customFreeTenat = null, int areaID = 0)
	{
		return CalcTimeChargeForUM(data, chargeTime, freetype, useDefaultFree, isTimeOut, customFreeTenat);
	}
}
