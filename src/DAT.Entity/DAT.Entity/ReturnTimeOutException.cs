using System;

namespace DAT.Entity;

public class ReturnTimeOutException : Exception
{
	public ReturnTimeOutException()
	{
	}

	public ReturnTimeOutException(string message)
		: base(message)
	{
	}
}
