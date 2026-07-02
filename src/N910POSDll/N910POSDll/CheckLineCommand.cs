using System;

namespace N910POSDll;

[Serializable]
public class CheckLineCommand : ECRCommand
{
	public CheckLineCommand()
	{
		m_Commands.Add(new CommandUnit("VERSION", "V00002"));
		m_Commands.Add(new CommandUnit("CMD", "CHECKLINE"));
		m_TransactionSEQ = CommandConsts.TransactionSEQ;
		m_Commands.Add(new CommandUnit("REQUESTID", m_TransactionSEQ));
	}
}
