using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class BeepArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public int Second { get; set; }

	public BeepArgs()
	{
	}

	public BeepArgs(string onlyID)
		: base(onlyID)
	{
	}
}
