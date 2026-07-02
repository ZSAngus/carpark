using Master.SystemCommunication.Lib;

namespace CarPark2018.LPPayForms;

public class CorrectParkTypeReturn : ProgramBase
{
	public bool IsOK { get; set; }

	public string ErrCode { get; set; }

	public CorrectParkTypeReturn()
	{
	}

	public CorrectParkTypeReturn(string onlyID)
		: base(onlyID)
	{
	}
}
