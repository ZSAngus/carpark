using System;

namespace N910POSDll;

[Serializable]
public class SaleBrifInfo
{
	public string CardTransacionType { get; set; }

	public decimal OragBalance { get; set; }

	public DateTime SaleTime { get; set; }

	public string TerminalID { get; set; }

	public decimal TotalAMT { get; set; }
}
