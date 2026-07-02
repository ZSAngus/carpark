using Master.SystemCommunication.Lib;

namespace CarPark2018.UserControls;

public class MyItem
{
	public string text { get; set; }

	public NoticeType PassStatus { get; set; }

	public MyItem(string str, NoticeType status)
	{
		text = str;
		PassStatus = status;
	}
}
