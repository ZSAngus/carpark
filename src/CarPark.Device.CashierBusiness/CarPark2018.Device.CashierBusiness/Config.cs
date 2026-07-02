using System;
using System.IO;
using System.Text;

namespace CarPark2018.Device.CashierBusiness;

public class Config
{
	private static string fileName;

	private static CashierBusinessConfig m_SystemConfig;

	public static CashierBusinessConfig SystemConfig
	{
		get
		{
			if (m_SystemConfig == null)
			{
				m_SystemConfig = new CashierBusinessConfig();
			}
			return m_SystemConfig;
		}
	}

	static Config()
	{
		m_SystemConfig = null;
		fileName = $"{AppDomain.CurrentDomain.BaseDirectory}\\CarPark2018.Device.CashierBusiness.Config.xml";
	}

	public static void Save()
	{
		File.WriteAllText(fileName, m_SystemConfig.ToString(), Encoding.UTF8);
	}
}
