using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarPark2018.UserControls;

public class vkButton : Label
{
	public string KeyValue = "";

	private IContainer components = null;

	public Image DefaultImage { get; set; }

	public Image ClickImage { get; set; }

	public vkButton(string text)
	{
		New(text, text, new Size(30, 30), new Font("tahoma", 12f, FontStyle.Bold));
	}

	public vkButton(string text, string keyValue, Size size, Font font)
	{
		New(text, keyValue, size, font);
	}

	public void New(string text, string keyvalue, Size size, Font font)
	{
		BackColor = ColorTranslator.FromHtml("#1E2C37");
		Font = new Font("微软雅黑", 27.75f, FontStyle.Regular, GraphicsUnit.Point, 134);
		ForeColor = SystemColors.Window;
		Text = text;
		KeyValue = keyvalue;
		Font = font;
		base.Size = size;
		Cursor = Cursors.Hand;
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		base.MouseUp += btnNormal;
		base.MouseDown += btnPressed;
	}

	private void btnNormal(object o, EventArgs e)
	{
		Graphics graphics = CreateGraphics();
		btnPaint(graphics, ForeColor, BackColor, DefaultImage);
		graphics.Dispose();
	}

	private void btnHover(object o, EventArgs e)
	{
	}

	private void btnPressed(object o, EventArgs e)
	{
		Graphics graphics = CreateGraphics();
		btnPaint(graphics, ForeColor, BackColor, ClickImage);
		graphics.Dispose();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		btnPaint(e.Graphics, ForeColor, BackColor, DefaultImage);
	}

	private void btnPaint(Graphics graphic, Color foreColor, Color backgroundColor, Image image)
	{
		if (image != null)
		{
			graphic.Clear(backgroundColor);
			graphic.DrawImageUnscaled(image, 0, 0, image.Width, image.Height);
			StringFormat stringFormat = new StringFormat();
			Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			graphic.DrawString(Text.ToUpper(), Font, new SolidBrush(foreColor), rectangle, stringFormat);
		}
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
		this.components = new System.ComponentModel.Container();
	}
}
