using System;

namespace N910POSDll;

[Serializable]
public class LogonCommand : ECRCommand
{
	public LogonCommand()
	{
		m_Commands.Add(new CommandUnit("VERSION", "V00002"));
		m_Commands.Add(new CommandUnit("CMD", "LOGON"));
		m_TransactionSEQ = CommandConsts.TransactionSEQ;
		m_Commands.Add(new CommandUnit("REQUESTID", m_TransactionSEQ));
	}
}
