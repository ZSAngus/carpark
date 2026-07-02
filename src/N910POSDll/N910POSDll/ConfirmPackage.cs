using System;
using System.Collections.Generic;

namespace N910POSDll;

[Serializable]
public class ConfirmPackage : BaseCommand
{
	public override string ToCommand => $"{CommandConsts.STX},{$"\"TXNNO={m_TransactionSEQ}\""},{CommandConsts.ETX}";

	public override string REQUESTID
	{
		get
		{
			if (string.IsNullOrEmpty(m_TransactionSEQ))
			{
				throw new ArgumentNullException("transaction seq not inted");
			}
			return base.REQUESTID;
		}
		set
		{
			base.REQUESTID = value;
		}
	}

	public override T FromString<T>(string receivedPackage)
	{
		ConfirmPackage confirmPackage = new ConfirmPackage();
		Dictionary<string, string> dictionary = BaseCommand.ParseMessage(receivedPackage);
		if (!dictionary.ContainsKey("REQUESTID"))
		{
			throw new ArgumentException("Not a confirm package");
		}
		confirmPackage.REQUESTID = dictionary["REQUESTID"];
		return confirmPackage as T;
	}
}
