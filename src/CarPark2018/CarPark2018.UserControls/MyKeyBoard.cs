using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarPark2018.UserControls;

public class MyKeyBoard : UserControl
{
	private vkButton[] vk = new vkButton[41];

	private string[] k41 = new string[38]
	{
		"1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
		"q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
		"a", "s", "d", "f", "g", "h", "j", "k", "l", "z",
		"x", "c", "v", "b", "n", "m", "{BACKSPACE}", "{ENTER}"
	};

	private IContainer components = null;

	public MyKeyBoard()
	{
		InitializeComponent();
	}

	protected override void OnLoad(EventArgs e)
	{
		BackColor = ColorTranslator.FromHtml("#1E2C37");
		Font font = new Font("微软雅黑", 27.75f, FontStyle.Regular, GraphicsUnit.Point, 134);
		int num = 10;
		int num2 = 10;
		for (int i = 0; i < k41.Length; i++)
		{
			vk[i] = new vkButton(k41[i], k41[i], new Size(106, 86), font);
			vk[i].Click += btnCommon_Click;
			vk[i].Location = new Point(num, num2);
			switch (i)
			{
			case 36:
				vk[i].DefaultImage = ImageManager.GetImage("Login", "key_backspace_1_1680");
				vk[i].ClickImage = ImageManager.GetImage("Login", "key_backspace_2_1680");
				vk[i].Text = "";
				break;
			case 37:
				vk[i].DefaultImage = ImageManager.GetImage("Login", "key_ok_1_1860");
				vk[i].ClickImage = ImageManager.GetImage("Login", "key_ok_2_1860");
				vk[i].Text = "";
				break;
			default:
				vk[i].DefaultImage = ImageManager.GetImage("Login", "key_s_1_1680");
				vk[i].ClickImage = ImageManager.GetImage("Login", "key_s_2_1680");
				break;
			}
			vk[i].Size = new Size(vk[i].DefaultImage.Width, vk[i].DefaultImage.Height);
			switch (i)
			{
			case 19:
				num2 += 94;
				num = 10 + vk[i].Width / 2;
				break;
			default:
				if (i != 35)
				{
					num = ((i != 38 && i != 39 && i != 40) ? (num + (vk[i].Width + 11)) : (num + (vk[i].Width + 2)));
					break;
				}
				goto case 9;
			case 9:
			case 28:
				num2 += 94;
				num = 10;
				break;
			}
		}
		ControlCollection controls = base.Controls;
		Control[] controls2 = vk;
		controls.AddRange(controls2);
	}

	private void btnCommon_Click(object o, EventArgs e)
	{
		string keyValue = ((vkButton)o).KeyValue;
		Console.WriteLine(keyValue);
		SendKeys.Send(keyValue);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.Name = "MyKeyBoard";
		base.Size = new System.Drawing.Size(945, 389);
		base.ResumeLayout(false);
	}
}
