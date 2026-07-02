using System;

namespace N910POSDll;

[Serializable]
public class CommandUnit
{
	private string m_Command;

	private string m_Value;

	public string Command => m_Command;

	public int Index { get; set; }

	public string Value => m_Value;

	public CommandUnit(string command, string value)
	{
		m_Command = command;
		m_Value = value;
	}

	public override string ToString()
	{
		return $"{m_Command}={m_Value}";
	}
}
