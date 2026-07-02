using System;
using System.Collections.Generic;
using System.Reflection;

namespace N910POSDll;

[Serializable]
public class BaseCommand : BaseSerializable
{
	protected List<CommandUnit> m_Commands;

	protected string m_TransactionSEQ;

	public virtual string ToCommand
	{
		get
		{
			string commandBody = string.Empty;
			Array.ForEach(m_Commands.ToArray(), delegate(CommandUnit command)
			{
				commandBody += $"\"{command}\",";
			});
			if (commandBody.Length > 0)
			{
				commandBody = "," + commandBody;
			}
			string text = CommandConsts.STX + commandBody + CommandConsts.ETX;
			return text.Length.ToString().PadLeft(4, '0') + text;
		}
	}

	public virtual string REQUESTID
	{
		get
		{
			return m_TransactionSEQ;
		}
		set
		{
			m_TransactionSEQ = value;
		}
	}

	public BaseCommand()
	{
		m_Commands = new List<CommandUnit>();
		m_TransactionSEQ = string.Empty;
	}

	public virtual T FromString<T>(string receivedPackage) where T : BaseCommand
	{
		throw new NotImplementedException();
	}

	protected static List<CommandUnit> ParseCommandUint(string receivedPackage)
	{
		string text = receivedPackage.Trim();
		text = text.Substring(4);
		if (!text.StartsWith(CommandConsts.STX))
		{
			return null;
		}
		int num = text.LastIndexOf(CommandConsts.STX) + CommandConsts.STX.Length;
		int length = text.Length;
		string text2 = text.Substring(num, length - num);
		List<CommandUnit> rtn = new List<CommandUnit>();
		int index = 0;
		Array.ForEach(text2.Split(','), delegate(string val)
		{
			if (!string.IsNullOrEmpty(val))
			{
				val = val.Substring(1, val.Length - 2);
				string[] array = val.Split('=');
				if (array.Length == 2)
				{
					CommandUnit item = new CommandUnit(array[0], array[1])
					{
						Index = index++
					};
					rtn.Add(item);
				}
			}
		});
		return rtn;
	}

	protected static Dictionary<string, string> ParseMessage(string receivedPackage)
	{
		string text = receivedPackage.Replace(CommandConsts.ETX, string.Empty).Replace(CommandConsts.STX, string.Empty);
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		int num = 0;
		string[] array = text.Split(',');
		foreach (string text2 in array)
		{
			if (text2.IndexOf('=') >= 0)
			{
				string[] array2 = text2.Substring(1).Substring(0, text2.Length - 2).Split('=');
				dictionary.Add(array2[0], array2[1]);
				num++;
			}
		}
		return dictionary;
	}

	protected static List<KeyValuePair<string, string>> ParsePair(string receivedPackage)
	{
		string text = receivedPackage.Replace(CommandConsts.ETX, string.Empty).Replace(CommandConsts.STX, string.Empty);
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		string[] array = text.Split(',');
		foreach (string text2 in array)
		{
			if (text2.IndexOf('=') >= 0)
			{
				string[] array2 = text2.Substring(1).Substring(0, text2.Length - 2).Split('=');
				list.Add(new KeyValuePair<string, string>(array2[0], array2[1]));
			}
		}
		return list;
	}

	public override string ToString()
	{
		string text = string.Empty;
		PropertyInfo[] properties = GetType().GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			text += $"{propertyInfo.Name}:{propertyInfo.GetValue(this, null)}{Environment.NewLine}";
		}
		return text;
	}
}
