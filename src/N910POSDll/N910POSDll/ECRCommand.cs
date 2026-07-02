using System;

namespace N910POSDll;

[Serializable]
public class ECRCommand : BaseCommand
{
	public override string REQUESTID
	{
		get
		{
			if (string.IsNullOrEmpty(m_TransactionSEQ))
			{
				throw new ArgumentNullException("Transaction SEQ not inited");
			}
			return m_TransactionSEQ;
		}
		set
		{
			throw new NotSupportedException("TransactionSEQ can not be set in this command");
		}
	}
}
