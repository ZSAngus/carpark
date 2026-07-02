using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CarPark2018.LPPayForms;
using log4net;

namespace CarPark2018.Forms;

public class FormLPPayMainOwner : Form
{
	private ILog Logger;

	private static FormLPPayMainOwner frmUser;

	private IContainer components = null;

	private PictureBox pic1;

	private PictureBox pic2;

	private PictureBox pic3;

	private PictureBox pic6;

	private PictureBox pic5;

	private PictureBox pic4;

	private Label lp1;

	private Label lp2;

	private Label lp3;

	private Label lp5;

	private Label lp6;

	private Label lp4;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	public static FormLPPayMainOwner Self()
	{
		if (frmUser == null)
		{
			frmUser = new FormLPPayMainOwner();
			int num = Screen.GetWorkingArea(frmUser).Width;
			frmUser.Location = new Point(num, 0);
			frmUser.Show();
		}
		return frmUser;
	}

	public FormLPPayMainOwner()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		InitializeComponent();
	}

	public void SetInfo(List<LPShowItem> args)
	{
		try
		{
			int num = 0;
			foreach (LPShowItem arg in args)
			{
				switch (num)
				{
				case 0:
					SetControl(arg, lp1, pic1);
					break;
				case 1:
					SetControl(arg, lp2, pic2);
					break;
				case 2:
					SetControl(arg, lp3, pic3);
					break;
				case 3:
					SetControl(arg, lp4, pic4);
					break;
				case 4:
					SetControl(arg, lp5, pic5);
					break;
				case 5:
					SetControl(arg, lp6, pic6);
					break;
				}
				num++;
			}
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void SetControl(LPShowItem item, Label lab, PictureBox pic)
	{
		try
		{
			lab.Text = item.Licenseplate;
			pic.Image = item.Image;
		}
		catch (Exception message)
		{
			Logger.Error(message);
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
		this.pic1 = new System.Windows.Forms.PictureBox();
		this.pic2 = new System.Windows.Forms.PictureBox();
		this.pic3 = new System.Windows.Forms.PictureBox();
		this.pic6 = new System.Windows.Forms.PictureBox();
		this.pic5 = new System.Windows.Forms.PictureBox();
		this.pic4 = new System.Windows.Forms.PictureBox();
		this.lp1 = new System.Windows.Forms.Label();
		this.lp2 = new System.Windows.Forms.Label();
		this.lp3 = new System.Windows.Forms.Label();
		this.lp5 = new System.Windows.Forms.Label();
		this.lp6 = new System.Windows.Forms.Label();
		this.lp4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pic1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pic2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pic3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pic6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pic5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pic4).BeginInit();
		base.SuspendLayout();
		this.pic1.BackColor = System.Drawing.Color.Black;
		this.pic1.Location = new System.Drawing.Point(31, 43);
		this.pic1.Name = "pic1";
		this.pic1.Size = new System.Drawing.Size(410, 270);
		this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pic1.TabIndex = 0;
		this.pic1.TabStop = false;
		this.pic2.BackColor = System.Drawing.Color.Black;
		this.pic2.Location = new System.Drawing.Point(478, 43);
		this.pic2.Name = "pic2";
		this.pic2.Size = new System.Drawing.Size(410, 270);
		this.pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pic2.TabIndex = 1;
		this.pic2.TabStop = false;
		this.pic3.BackColor = System.Drawing.Color.Black;
		this.pic3.Location = new System.Drawing.Point(925, 43);
		this.pic3.Name = "pic3";
		this.pic3.Size = new System.Drawing.Size(410, 270);
		this.pic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pic3.TabIndex = 2;
		this.pic3.TabStop = false;
		this.pic6.BackColor = System.Drawing.Color.Black;
		this.pic6.Location = new System.Drawing.Point(925, 411);
		this.pic6.Name = "pic6";
		this.pic6.Size = new System.Drawing.Size(410, 270);
		this.pic6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pic6.TabIndex = 5;
		this.pic6.TabStop = false;
		this.pic5.BackColor = System.Drawing.Color.Black;
		this.pic5.Location = new System.Drawing.Point(478, 411);
		this.pic5.Name = "pic5";
		this.pic5.Size = new System.Drawing.Size(410, 270);
		this.pic5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pic5.TabIndex = 4;
		this.pic5.TabStop = false;
		this.pic4.BackColor = System.Drawing.Color.Black;
		this.pic4.Location = new System.Drawing.Point(31, 411);
		this.pic4.Name = "pic4";
		this.pic4.Size = new System.Drawing.Size(410, 270);
		this.pic4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pic4.TabIndex = 3;
		this.pic4.TabStop = false;
		this.lp1.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.lp1.ForeColor = System.Drawing.Color.White;
		this.lp1.Location = new System.Drawing.Point(31, 316);
		this.lp1.Name = "lp1";
		this.lp1.Size = new System.Drawing.Size(410, 40);
		this.lp1.TabIndex = 6;
		this.lp1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lp2.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.lp2.ForeColor = System.Drawing.Color.White;
		this.lp2.Location = new System.Drawing.Point(478, 316);
		this.lp2.Name = "lp2";
		this.lp2.Size = new System.Drawing.Size(410, 40);
		this.lp2.TabIndex = 7;
		this.lp2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lp3.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.lp3.ForeColor = System.Drawing.Color.White;
		this.lp3.Location = new System.Drawing.Point(925, 316);
		this.lp3.Name = "lp3";
		this.lp3.Size = new System.Drawing.Size(410, 40);
		this.lp3.TabIndex = 8;
		this.lp3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lp5.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.lp5.ForeColor = System.Drawing.Color.White;
		this.lp5.Location = new System.Drawing.Point(478, 684);
		this.lp5.Name = "lp5";
		this.lp5.Size = new System.Drawing.Size(410, 40);
		this.lp5.TabIndex = 9;
		this.lp5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lp6.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.lp6.ForeColor = System.Drawing.Color.White;
		this.lp6.Location = new System.Drawing.Point(925, 684);
		this.lp6.Name = "lp6";
		this.lp6.Size = new System.Drawing.Size(410, 40);
		this.lp6.TabIndex = 10;
		this.lp6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lp4.Font = new System.Drawing.Font("微软雅黑", 15f);
		this.lp4.ForeColor = System.Drawing.Color.White;
		this.lp4.Location = new System.Drawing.Point(31, 684);
		this.lp4.Name = "lp4";
		this.lp4.Size = new System.Drawing.Size(410, 40);
		this.lp4.TabIndex = 11;
		this.lp4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.SystemColors.Control;
		this.label1.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.label1.Location = new System.Drawing.Point(31, 43);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(31, 35);
		this.label1.TabIndex = 12;
		this.label1.Text = "1";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.SystemColors.Control;
		this.label2.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.label2.Location = new System.Drawing.Point(478, 43);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(31, 35);
		this.label2.TabIndex = 13;
		this.label2.Text = "2";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.SystemColors.Control;
		this.label3.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.label3.Location = new System.Drawing.Point(925, 43);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(31, 35);
		this.label3.TabIndex = 14;
		this.label3.Text = "3";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.SystemColors.Control;
		this.label4.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.label4.Location = new System.Drawing.Point(31, 411);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(31, 35);
		this.label4.TabIndex = 15;
		this.label4.Text = "4";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.SystemColors.Control;
		this.label5.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.label5.Location = new System.Drawing.Point(478, 411);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(31, 35);
		this.label5.TabIndex = 16;
		this.label5.Text = "5";
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.SystemColors.Control;
		this.label6.Font = new System.Drawing.Font("微软雅黑", 20f);
		this.label6.Location = new System.Drawing.Point(925, 411);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(31, 35);
		this.label6.TabIndex = 17;
		this.label6.Text = "6";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Highlight;
		base.ClientSize = new System.Drawing.Size(1366, 768);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.lp4);
		base.Controls.Add(this.lp6);
		base.Controls.Add(this.lp5);
		base.Controls.Add(this.lp3);
		base.Controls.Add(this.lp2);
		base.Controls.Add(this.lp1);
		base.Controls.Add(this.pic6);
		base.Controls.Add(this.pic5);
		base.Controls.Add(this.pic4);
		base.Controls.Add(this.pic3);
		base.Controls.Add(this.pic2);
		base.Controls.Add(this.pic1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "FormLPPayMainOwner";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "FormLPPayMainOwner";
		base.TopMost = true;
		((System.ComponentModel.ISupportInitialize)this.pic1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pic2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pic3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pic6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pic5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pic4).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
