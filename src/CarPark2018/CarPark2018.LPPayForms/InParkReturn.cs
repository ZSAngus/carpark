using Master.SystemCommunication.Lib;

namespace CarPark2018.LPPayForms;

public class InParkReturn : ProgramBase
{
	public bool IsOK { get; set; }

	public string ErrCode { get; set; }

	public InParkReturn()
	{
	}

	public InParkReturn(string onlyID)
		: base(onlyID)
	{
	}
}
