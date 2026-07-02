namespace CarPark.DB.Context;

public class DBContext
{
	private static string _DBConn = string.Empty;

	private static string _DBConn2 = string.Empty;

	private static string m_UserName = "root";

	private static string m_Pwd = "123456";

	private static string m_Ip = "localhost";

	private static string m_DBName = "CarparkData";

	private static string m_fromatDBConn = "metadata=res://*/CarParkData.csdl|res://*/CarParkData.ssdl|res://*/CarParkData.msl;provider=MySql.Data.MySqlClient;provider connection string=\"server={0};User Id={1};password=master{2};Persist Security Info=True;database={3};Pooling=true;Min Pool Size=1;Max Pool Size=20;\"";

	private static string m_UserName2 = "root";

	private static string m_Pwd2 = "123456";

	private static string m_Ip2 = "localhost";

	private static string m_DBName2 = "CarparkData";

	public static bool ServerA = true;

	public static bool ServerB = true;

	public static string UserName
	{
		get
		{
			return m_UserName;
		}
		set
		{
			m_UserName = value;
		}
	}

	public static string Pwd
	{
		get
		{
			return m_Pwd;
		}
		set
		{
			m_Pwd = value;
		}
	}

	public static string Ip
	{
		get
		{
			return m_Ip;
		}
		set
		{
			m_Ip = value;
		}
	}

	public static string DBName
	{
		get
		{
			return m_DBName;
		}
		set
		{
			m_DBName = value;
		}
	}

	public static string DBConn
	{
		get
		{
			return _DBConn;
		}
		set
		{
			_DBConn = value;
		}
	}

	public static Entities NewContext
	{
		get
		{
			if (!string.IsNullOrEmpty(_DBConn))
			{
				return new Entities(_DBConn);
			}
			if (ServerA)
			{
				_DBConn2 = string.Format(m_fromatDBConn, m_Ip, m_UserName, m_Pwd, m_DBName);
			}
			else
			{
				_DBConn2 = string.Format(m_fromatDBConn, m_Ip2, m_UserName2, m_Pwd2, m_DBName2);
			}
			return new Entities(_DBConn2);
		}
	}

	public static void SetDBTwo(string userName2, string pwd2, string IP2, string DBName2)
	{
		m_UserName2 = userName2;
		m_Pwd2 = pwd2;
		m_Ip2 = IP2;
		m_DBName2 = DBName2;
	}
}
