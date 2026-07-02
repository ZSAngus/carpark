using System.Windows.Forms;

namespace CarPark.Lib;

public class Global
{
	public Global()
	{
		Class2.sKBPqdpzNwCBA();
	}

	public static DialogResult ShowDialog(string message)
	{
		Dialog dialog = new Dialog();
		dialog.MSGText = message;
		Dialog dialog2 = dialog;
		using Dialog dialog3 = dialog2;
		return dialog3.ShowDialog();
	}

	public static DialogResult ShowDialog(string message, bool OkFocus)
	{
		Dialog dialog = new Dialog();
		dialog.MSGText = message;
		Dialog dialog2 = dialog;
		dialog2.SetFocus(OkFocus);
		using Dialog dialog3 = dialog2;
		return dialog3.ShowDialog();
	}

	public static void ShowMessage(string message)
	{
		MessageForm messageForm = new MessageForm();
		messageForm.MSGText = message;
		MessageForm messageForm2 = messageForm;
		using MessageForm messageForm3 = messageForm2;
		messageForm3.ShowDialog();
	}
}
