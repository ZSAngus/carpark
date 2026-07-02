using System;

namespace N910POSDll;

[Serializable]
public class SaleCommand : ECRCommand
{
	public SaleCommand(decimal amt)
	{
		m_Commands.Add(new CommandUnit("VERSION", "V00002"));
		m_Commands.Add(new CommandUnit("CMD", "SALE"));
		m_TransactionSEQ = CommandConsts.TransactionSEQ;
		m_Commands.Add(new CommandUnit("REQUESTID", m_TransactionSEQ));
		m_Commands.Add(new CommandUnit("FUNC", "CARD"));
		m_Commands.Add(new CommandUnit("AMOUNT", amt.ToString("F2")));
		m_Commands.Add(new CommandUnit("DETAIL", "Y"));
	}
}
