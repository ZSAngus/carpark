using Master.SystemCommunication.Lib;

namespace CarPark2018.LPPayForms;

public class CheckFeeReturn : ProgramBase
{
	public bool IsPaid { get; set; }

	public bool IsTimeout { get; set; }

	public CheckFeeReturn()
	{
	}

	public CheckFeeReturn(string onlyID)
		: base(onlyID)
	{
	}
}
