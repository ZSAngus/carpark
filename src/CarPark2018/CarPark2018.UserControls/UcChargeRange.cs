using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CarPark.DB;
using CarPark.Lib;
using SkyInno.Lang;

namespace CarPark2018.UserControls;

public class UcChargeRange : UserControl
{
	private TimeChargeExt m_ChargeExt;

	private IContainer components = null;

	private CheckBox checkRange;

	private NumericUpDown timeStart;

	private Label lblCharge;

	private Label labelX7;

	private NumericUpDown timeEnd;

	private NumericUpDown valCharge;

	public TimeChargeExt ChargeExt
	{
		get
		{
			return m_ChargeExt;
		}
		set
		{
			m_ChargeExt = value;
			if (m_ChargeExt != null)
			{
				checkRange.Checked = m_ChargeExt.Enabled;
				valCharge.Value = m_ChargeExt.Charge;
				timeEnd.Value = m_ChargeExt.EndHR;
				timeStart.Value = m_ChargeExt.StartHR;
			}
			else
			{
				checkRange.Checked = false;
				timeEnd.Value = 0m;
				timeStart.Value = 0m;
				valCharge.Value = 0m;
			}
		}
	}

	public UcChargeRange()
	{
		InitializeComponent();
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		checkRange.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcChargeRange.checkRange");
		lblCharge.Text = LangManager.GetLangString("CarPark.UserControls.SysConfig.UcChargeRange.lblCharge");
	}

	private void checkRange_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			NumericUpDown numericUpDown = valCharge;
			NumericUpDown numericUpDown2 = timeEnd;
			bool flag = (timeStart.Enabled = checkRange.Checked);
			bool enabled = (numericUpDown2.Enabled = flag);
			numericUpDown.Enabled = enabled;
			if (m_ChargeExt != null)
			{
				m_ChargeExt.Enabled = checkRange.Checked;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	private void timeStart_Leave(object sender, EventArgs e)
	{
		try
		{
			if (m_ChargeExt != null)
			{
				if (timeEnd.Value < timeStart.Value)
				{
					Global.ShowMessage(LangManager.GetLangString("ERR_TIMERANGE"));
					return;
				}
				m_ChargeExt.Charge = valCharge.Value;
				m_ChargeExt.Enabled = checkRange.Checked;
				m_ChargeExt.EndHR = Convert.ToInt32(timeEnd.Value);
				m_ChargeExt.StartHR = Convert.ToInt32(timeStart.Value);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
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
		this.checkRange = new System.Windows.Forms.CheckBox();
		this.timeStart = new System.Windows.Forms.NumericUpDown();
		this.lblCharge = new System.Windows.Forms.Label();
		this.labelX7 = new System.Windows.Forms.Label();
		this.timeEnd = new System.Windows.Forms.NumericUpDown();
		this.valCharge = new System.Windows.Forms.NumericUpDown();
		((System.ComponentModel.ISupportInitialize)this.timeStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.timeEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.valCharge).BeginInit();
		base.SuspendLayout();
		this.checkRange.AutoSize = true;
		this.checkRange.Location = new System.Drawing.Point(17, 5);
		this.checkRange.Name = "checkRange";
		this.checkRange.Size = new System.Drawing.Size(111, 31);
		this.checkRange.TabIndex = 0;
		this.checkRange.Text = "時間範圍";
		this.checkRange.UseVisualStyleBackColor = true;
		this.checkRange.CheckedChanged += new System.EventHandler(checkRange_CheckedChanged);
		this.timeStart.Location = new System.Drawing.Point(134, 4);
		this.timeStart.Name = "timeStart";
		this.timeStart.Size = new System.Drawing.Size(52, 34);
		this.timeStart.TabIndex = 1;
		this.timeStart.Leave += new System.EventHandler(timeStart_Leave);
		this.lblCharge.AutoSize = true;
		this.lblCharge.Location = new System.Drawing.Point(277, 6);
		this.lblCharge.Name = "lblCharge";
		this.lblCharge.Size = new System.Drawing.Size(52, 27);
		this.lblCharge.TabIndex = 2;
		this.lblCharge.Text = "收費";
		this.labelX7.AutoSize = true;
		this.labelX7.Location = new System.Drawing.Point(192, 6);
		this.labelX7.Name = "labelX7";
		this.labelX7.Size = new System.Drawing.Size(21, 27);
		this.labelX7.TabIndex = 2;
		this.labelX7.Text = "-";
		this.timeEnd.Location = new System.Drawing.Point(219, 4);
		this.timeEnd.Name = "timeEnd";
		this.timeEnd.Size = new System.Drawing.Size(52, 34);
		this.timeEnd.TabIndex = 1;
		this.timeEnd.Leave += new System.EventHandler(timeStart_Leave);
		this.valCharge.Location = new System.Drawing.Point(335, 4);
		this.valCharge.Name = "valCharge";
		this.valCharge.Size = new System.Drawing.Size(91, 34);
		this.valCharge.TabIndex = 1;
		this.valCharge.Leave += new System.EventHandler(timeStart_Leave);
		base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.labelX7);
		base.Controls.Add(this.lblCharge);
		base.Controls.Add(this.valCharge);
		base.Controls.Add(this.timeEnd);
		base.Controls.Add(this.timeStart);
		base.Controls.Add(this.checkRange);
		this.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
		base.Name = "UcChargeRange";
		base.Size = new System.Drawing.Size(443, 43);
		((System.ComponentModel.ISupportInitialize)this.timeStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.timeEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.valCharge).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
