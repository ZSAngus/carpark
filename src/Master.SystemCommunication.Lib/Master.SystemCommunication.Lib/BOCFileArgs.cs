using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 銀聯上數內容
/// </summary>
public class BOCFileArgs : ProgramBase
{
	[DataMember]
	public int BOCFileID { get; set; }

	[DataMember]
	public DateTime StartTime { get; set; }

	[DataMember]
	public DateTime EndTime { get; set; }

	[DataMember]
	public DateTime ZipTime { get; set; }

	[DataMember]
	public DateTime UpLoadTime { get; set; }

	[DataMember]
	public bool IsUpload { get; set; }

	[DataMember]
	public string FileName { get; set; }

	[DataMember]
	public byte[] FileContent { get; set; }

	public BOCFileArgs()
	{
	}

	public BOCFileArgs(string onlyID)
		: base(onlyID)
	{
	}

	public override string ToString()
	{
		return $" BOCFileID{BOCFileID} StartTime{StartTime} EndTime{EndTime} ZipTime{ZipTime} UpLoadTime{UpLoadTime} FileName{FileName} IsUpload{IsUpload} ";
	}
}
