using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace N910POSDll;

[Serializable]
public class SaleResult : BaseCommand
{
	public string VERSION { get; set; }

	public string CMD { get; set; }

	public string STATUS { get; set; }

	public string TXNDATE { get; set; }

	public string TXNTIME { get; set; }

	public string RCODE { get; set; }

	public string MESSAGE { get; set; }

	public string AMOUNT { get; set; }

	public string AUTHCODE { get; set; }

	public string PAN { get; set; }

	public string EXPIRYDATE { get; set; }

	public string ENTERMODE { get; set; }

	public string TRACEID { get; set; }

	public string TERMINALID { get; set; }

	public string MERCHANTID { get; set; }

	public string CARDTYPE { get; set; }

	public string CARDHOLDERNAME { get; set; }

	public string BATCHNO { get; set; }

	public string REFERENCENO { get; set; }

	public string ISSIGNATURE { get; set; }

	public override T FromString<T>(string receivedPackage)
	{
		SaleResult saleResult = new SaleResult();
		List<CommandUnit> source = BaseCommand.ParseCommandUint(receivedPackage);
		PropertyInfo[] properties = saleResult.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			string propName = propertyInfo.Name;
			CommandUnit commandUnit = source.FirstOrDefault((CommandUnit m) => m.Command == propName);
			if (commandUnit != null)
			{
				propertyInfo.SetValue(saleResult, Convert.ChangeType(commandUnit.Value, propertyInfo.PropertyType), null);
			}
		}
		return saleResult as T;
	}
}
