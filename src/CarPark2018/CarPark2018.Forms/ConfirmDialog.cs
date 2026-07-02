using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarPark2018.Properties;

namespace CarPark2018.Forms;

public class ConfirmDialog : Form
{
	private Image LoadImage(string path)
	{
		return Image.FromFile(path);
	}

	private Label CreateLabel(string text, int yPos)
	{
		return new Label
		{
			Text = text,
			Font = new Font("微软雅黑", 14f, FontStyle.Bold),
			AutoSize = false,
			Size = new Size(550, 40),
			TextAlign = ContentAlignment.MiddleCenter,
			Location = new Point(25, yPos)
		};
	}

	private Button CreateButton(string text, int x, int y, DialogResult result)
	{
		return new Button
		{
			Text = text,
			Font = new Font("微软雅黑", 20f),
			Size = new Size(120, 50),
			Location = new Point(x, y),
			BackColor = ((text == "確認") ? Color.FromArgb(0, 120, 215) : SystemColors.Control),
			ForeColor = ((text == "確認") ? Color.White : SystemColors.ControlText),
			DialogResult = result
		};
	}

	public ConfirmDialog(string currency, string paymentMethod, int payTypeIndex, bool isMOPChecked, string deposit)
	{
		InitializeComponents(currency, paymentMethod, payTypeIndex, isMOPChecked, deposit);
	}

	private void InitializeComponents(string currency, string paymentMethod, int payTypeIndex, bool isMOPChecked, string deposit)
	{
		base.FormBorderStyle = FormBorderStyle.FixedDialog;
		base.StartPosition = FormStartPosition.CenterParent;
		Font = new Font("微软雅黑", 12f);
		RichTextBox richTextBox = new RichTextBox
		{
			ReadOnly = true,
			BorderStyle = BorderStyle.None,
			BackColor = SystemColors.Control,
			Font = new Font("微软雅黑", 25f),
			Size = new Size(550, 60),
			Location = new Point(25, 30)
		};
		Color color = (isMOPChecked ? Color.Red : Color.Blue);
		richTextBox.AppendTextWithColor("正在使用 '", Color.Black);
		richTextBox.AppendTextWithColor(currency, color);
		richTextBox.AppendTextWithColor(" ", Color.Black);
		richTextBox.AppendTextWithColor(paymentMethod, Color.Red);
		richTextBox.AppendTextWithColor("' 支付", Color.Black);
		List<Control> list = new List<Control>();
		bool flag = false;
		switch (payTypeIndex)
		{
		case 8:
		{
			Label label4 = CreateLabel("請掃碼支付", richTextBox.Bottom + 15);
			list.Add(label4);
			PictureBox pictureBox = new PictureBox
			{
				SizeMode = PictureBoxSizeMode.Zoom,
				Size = new Size(550, 300),
				Location = new Point(20, label4.Bottom + 15)
			};
			try
			{
				pictureBox.Image = LoadImage("image/QR.png");
			}
			catch
			{
			}
			list.Add(pictureBox);
			flag = true;
			break;
		}
		case 5:
		{
			Label label2 = CreateLabel("賬戶名：高寶建築投資有限公司", richTextBox.Bottom + 15);
			list.Add(label2);
			string text = (isMOPChecked ? "中國銀行澳門幣賬戶\n賬號：1818-1110-1161-007" : "中國銀行港幣賬戶\n賬號：1818-0110-1931-061");
			Label label3 = CreateLabel(text, label2.Bottom + 10);
			label3.Height = 50;
			list.Add(label3);
			flag = true;
			break;
		}
		case 0:
			if (Settings.Default.nocash == "1" && string.IsNullOrEmpty(deposit))
			{
				Label label = CreateLabel("如非寫字樓同意，租金嚴禁收取現金", richTextBox.Bottom + 15);
				label.Font = new Font("微软雅黑", 20f, FontStyle.Bold | FontStyle.Underline);
				label.ForeColor = Color.Red;
				label.Height = 50;
				list.Add(label);
				flag = true;
			}
			break;
		}
		int num = ((!flag) ? (richTextBox.Bottom + 20) : (list.Last().Bottom + 30));
		Button item = CreateButton("確認", 150, num, DialogResult.OK);
		Button item2 = CreateButton("取消", 330, num, DialogResult.Cancel);
		List<Control> list2 = new List<Control> { richTextBox, item, item2 };
		list2.AddRange(list);
		int val = list2.Max((Control c) => c.Bottom) + 50;
		int val2 = (flag ? 350 : 220);
		base.Size = new Size(600, Math.Max(val, val2));
		base.Controls.AddRange(list2.ToArray());
	}
}
