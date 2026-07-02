using System;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using SkyInno.Lang;

namespace CarPark.Lib;

public class LangHelper
{
	public LangHelper()
	{
		Class2.sKBPqdpzNwCBA();
	}

	public static void ApplyLang(object target)
	{
		Type type = target.GetType();
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			object value = fieldInfo.GetValue(target);
			if (value is Control)
			{
				Control control = (Control)value;
				string text = type.FullName + "." + control.Name;
				string langString = LangManager.GetLangString(text);
				if (text != langString)
				{
					control.Text = langString;
				}
			}
			if (value is ToolStripItem)
			{
				ToolStripItem toolStripItem = value as ToolStripItem;
				string text = type.FullName + "." + toolStripItem.Name;
				string langString = LangManager.GetLangString(text);
				if (text != langString)
				{
					toolStripItem.Text = langString;
				}
			}
			if (value is BaseItem)
			{
				BaseItem baseItem = (BaseItem)value;
				string text = type.FullName + "." + baseItem.Name;
				string langString = LangManager.GetLangString(text);
				if (text != langString)
				{
					baseItem.Text = langString;
				}
			}
			if (value is TabItem)
			{
				TabItem tabItem = (TabItem)value;
				string text = type.FullName + "." + tabItem.Name;
				string langString = LangManager.GetLangString(text);
				if (text != langString)
				{
					tabItem.Text = langString;
				}
			}
			if (value is DevComponents.AdvTree.ColumnHeader)
			{
				DevComponents.AdvTree.ColumnHeader columnHeader = (DevComponents.AdvTree.ColumnHeader)value;
				string text = type.FullName + "." + columnHeader.Name;
				string langString = LangManager.GetLangString(text);
				if (text != langString)
				{
					columnHeader.Text = langString;
				}
			}
		}
	}
}
