using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace N910POSDll;

[Serializable]
public class CheckLineResult : BaseCommand
{
	public string VERSION { get; set; }

	public string CMD { get; set; }

	public string STATUS { get; set; }

	public string TXNDATE { get; set; }

	public string TXNTIME { get; set; }

	public string RCODE { get; set; }

	public string MESSAGE { get; set; }

	public override T FromString<T>(string receivedPackage)
	{
		CheckLineResult checkLineResult = new CheckLineResult();
		List<CommandUnit> source = BaseCommand.ParseCommandUint(receivedPackage);
		PropertyInfo[] properties = checkLineResult.GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			string propName = propertyInfo.Name;
			CommandUnit commandUnit = source.FirstOrDefault((CommandUnit m) => m.Command == propName);
			if (commandUnit != null)
			{
				propertyInfo.SetValue(checkLineResult, Convert.ChangeType(commandUnit.Value, propertyInfo.PropertyType), null);
			}
		}
		return checkLineResult as T;
	}
}
