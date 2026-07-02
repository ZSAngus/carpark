using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.UserControls;

public class UCPasstraceEX : UserControl
{
	private static ILog Logger;

	private BindingSource bs;

	private IContainer components = null;

	private Label labTitle;

	private ListBox listBox1;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.Style &= -33554433;
			return createParams;
		}
	}

	static UCPasstraceEX()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public UCPasstraceEX()
	{
		InitializeComponent();
		BackgroundImage = ImageManager.GetImage("Main", "PasstraceBox");
		bs = new BindingSource();
		listBox1.BackColor = Color.FromArgb(150, 178, 208);
	}

	private void UCPasstraceEX2_Load(object sender, EventArgs e)
	{
		listBox1.DrawMode = DrawMode.OwnerDrawFixed;
		listBox1.ItemHeight = 50;
		listBox1.DrawItem += listBox1_DrawItem;
		bs.Clear();
		listBox1.DataSource = bs;
		LangManager.LanguageChangedEvent += LangManager_LanguageChangedEvent;
		LangManager_LanguageChangedEvent(SysLanguage.CHT);
	}

	public void Add(NoticeArgs context)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				bs.Add(new MyItem(context.Content, context.noticeType));
				bs.MoveLast();
				if (bs.Count > 100)
				{
					bs.RemoveAt(0);
				}
			});
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
	{
		if (e.Index >= 0)
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
			SetStyle(ControlStyles.DoubleBuffer, value: true);
			MyItem myItem = (MyItem)listBox1.Items[e.Index];
			string s = myItem.text;
			Rectangle rectangle = new Rectangle(0, 0, listBox1.Width, 100);
			Brush brush = new SolidBrush(Color.White);
			e.Graphics.FillRectangle(brush, e.Bounds);
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(49, 74, 105)), e.Bounds);
			Rectangle rectangle2 = new Rectangle(5, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
			if (myItem.PassStatus == NoticeType.Error)
			{
				e.Graphics.DrawString(s, new Font("微软雅黑", 9f), Brushes.Red, rectangle2);
			}
			else
			{
				e.Graphics.DrawString(s, new Font("微软雅黑", 9f), Brushes.Black, rectangle2);
			}
		}
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labTitle.Text = LangManager.GetLangString("CarPark.UserControls.UCPasstraceEX2.labTitle");
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
		this.labTitle = new System.Windows.Forms.Label();
		this.listBox1 = new System.Windows.Forms.ListBox();
		base.SuspendLayout();
		this.labTitle.BackColor = System.Drawing.Color.Transparent;
		this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
		this.labTitle.Font = new System.Drawing.Font("黑体", 15f);
		this.labTitle.ForeColor = System.Drawing.Color.White;
		this.labTitle.Location = new System.Drawing.Point(5, 10);
		this.labTitle.Name = "labTitle";
		this.labTitle.Size = new System.Drawing.Size(350, 33);
		this.labTitle.TabIndex = 4;
		this.labTitle.Text = "事件";
		this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listBox1.ForeColor = System.Drawing.Color.White;
		this.listBox1.FormattingEnabled = true;
		this.listBox1.ItemHeight = 12;
		this.listBox1.Location = new System.Drawing.Point(5, 43);
		this.listBox1.Name = "listBox1";
		this.listBox1.Size = new System.Drawing.Size(350, 274);
		this.listBox1.TabIndex = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.Controls.Add(this.listBox1);
		base.Controls.Add(this.labTitle);
		base.Name = "UCPasstraceEX2";
		base.Padding = new System.Windows.Forms.Padding(5, 10, 5, 10);
		base.Size = new System.Drawing.Size(360, 327);
		base.Load += new System.EventHandler(UCPasstraceEX2_Load);
		base.ResumeLayout(false);
	}
}
