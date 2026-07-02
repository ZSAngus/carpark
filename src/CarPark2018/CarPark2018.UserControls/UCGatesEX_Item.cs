using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark.Device;
using CarPark.Lib;
using System.Net;
using CarPark2018.Forms;
using CarPark2018.Properties;
using Master.SystemCommunication.Carpark.LocalService;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using log4net;

namespace CarPark2018.UserControls;

public class UCGatesEX_Item : UserControl
{
	public ParkGate mParkGate;

	private static ILog Logger;

	public DisabilityPressArgs D = new DisabilityPressArgs();

	public DisabilityPressArgs E = new DisabilityPressArgs();

	private string cn = "";

	private string en = "";

	private List<ParkArea> m_ParkArea = new List<ParkArea>();

	private List<ParkAreaExtend> m_ParkAreaExtend = new List<ParkAreaExtend>();

	private GetParkAreaArgs getArg = new GetParkAreaArgs();

	private string strLP = "";

	private IContainer components;

	public Label lab_TimeState;

	public Label lab_GateName;

	public TextBox txt_LicensePlate;

	private ToolTip toolTip1;

	public Label labTime;

	public Label lab_RentalState;

	public Label labMonth;

	public Label lab_MPState;

	public Label labMP;

	public Label lab_BOCState;

	public Label labQP;

	private Button btnDisability;

	private Button btn_Camera;

	private Button btnBarUp;

	private Button btnCancel;

	private Button btnElectric;

	private string textlp = "";

	public UCGatesEX_Item()
	{
		InitializeComponent();
	}

	static UCGatesEX_Item()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public UCGatesEX_Item(ParkGate mGate)
	{
		InitializeComponent();
		base.Name = "Gate" + mGate.GateID;
		lab_GateName.Text = mGate.GateName;
		cn = mGate.GateNameCn;
		en = mGate.GateNamePt;
		lab_TimeState.Text = "未連接";
		lab_TimeState.ForeColor = Color.Red;
		lab_TimeState.Tag = "-2";
		lab_RentalState.Text = "未連接";
		lab_RentalState.ForeColor = Color.Red;
		lab_RentalState.Tag = "-2";
		lab_MPState.Text = "未連接";
		lab_MPState.ForeColor = Color.Red;
		lab_MPState.Tag = "-2";
		lab_BOCState.Text = "未連接";
		lab_BOCState.ForeColor = Color.Red;
		lab_BOCState.Tag = "-2";
		btnDisability.Image = ImageManager.GetImage("UCGatesEX", "disability");
		btnCancel.Image = ImageManager.GetImage("UCGatesEX", "cancel");
		btnBarUp.Image = ImageManager.GetImage("UCGatesEX", "up");
		btnElectric.Image = ImageManager.GetImage("UCGatesEX", "Electric");
		mParkGate = mGate;
		BackgroundImage = ImageManager.GetImage("UCGatesEX", "GateBackground");
		btn_Camera.Visible = false;
		txt_LicensePlate.Visible = false;
		btnBarUp.Location = new Point(btnBarUp.Location.X, btnBarUp.Location.Y - 20);
		btnDisability.Location = new Point(btnDisability.Location.X, btnDisability.Location.Y - 20);
		btnCancel.Location = new Point(btnCancel.Location.X, btnCancel.Location.Y - 20);
		btnElectric.Location = new Point(btnElectric.Location.X, btnElectric.Location.Y - 20);
		LangManager.LanguageChangedEvent += LangManager_LanguageChangedEvent;
		LangManager_LanguageChangedEvent(LangManager.CurLanguage);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			DataBuffer2018.CheckRole(MethodBase.GetCurrentMethod());
			using (FormManualUpBarDialog formManualUpBarDialog = new FormManualUpBarDialog())
			{
				FormManualUpBarDialog.m_FormManualUpBarDialog_Event += FormManualUpBarDialog_m_FormManualUpBarDialog_Event;
				if (formManualUpBarDialog.ShowDialog(LangManager.GetLangString("ShowMessage.UpBar"), OkFocus: false) == DialogResult.Cancel)
				{
					return;
				}
			}
			ManualUpBarArgs manualUpBarArgs = new ManualUpBarArgs(Settings.Default.OnlyID);
			manualUpBarArgs.GateID = mParkGate.GateID;
			manualUpBarArgs.OperationPC = Settings.Default.OnlyID;
			manualUpBarArgs.ShiffCode = DataBuffer2018.CurrentStaff.StaffCode;
			manualUpBarArgs.Extend1 = strLP.ToUpper().Trim();
			FormManualUpBarDialog.m_FormManualUpBarDialog_Event -= FormManualUpBarDialog_m_FormManualUpBarDialog_Event;
			strLP = "";
			try
			{
				textlp = manualUpBarArgs.Extend1;
				if (string.IsNullOrEmpty(manualUpBarArgs.Extend1))
				{
					Form optionsForm = new Form
					{
						Text = "選擇操作",
						Size = new Size(500, 520),
						StartPosition = FormStartPosition.CenterParent,
						FormBorderStyle = FormBorderStyle.FixedDialog,
						MaximizeBox = false,
						Font = new Font("微软雅黑", 11.5f),
						BackColor = Color.FromArgb(240, 248, 255)
					};
					Label value = new Label
					{
						Text = "未輸入車牌，涉嫌違規操作！\n請輸入車牌或選擇違規原因",
						Size = new Size(440, 70),
						Location = new Point(30, 20),
						TextAlign = ContentAlignment.MiddleCenter,
						ForeColor = Color.DarkRed,
						Font = new Font("微软雅黑", 18f, FontStyle.Bold)
					};
					optionsForm.Controls.Add(value);
					Panel panel = new Panel
					{
						Location = new Point(40, 100),
						Size = new Size(420, 50),
						Visible = false
					};
					TextBox txtInput = new TextBox
					{
						MaxLength = 8,
						CharacterCasing = CharacterCasing.Upper,
						Size = new Size(300, 45),
						Font = new Font("微软雅黑", 20f, FontStyle.Bold),
						BorderStyle = BorderStyle.FixedSingle,
						TextAlign = HorizontalAlignment.Center,
						Multiline = true,
						Visible = false
					};
					txtInput.KeyPress += delegate(object s, KeyPressEventArgs ev)
					{
						if (!char.IsLetterOrDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
						{
							ev.Handled = true;
						}
					};
					Button button = new Button
					{
						Text = "確認",
						Size = new Size(120, 45),
						Location = new Point(300, 0),
						Font = new Font("微软雅黑", 16f, FontStyle.Bold),
						FlatStyle = FlatStyle.Standard,
						BackColor = Color.LightSteelBlue,
						Visible = false
					};
					button.Click += delegate
					{
						if (string.IsNullOrWhiteSpace(txtInput.Text))
						{
							MessageBox.Show("請先輸入車牌號！\n或選擇原因", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
						else
						{
							optionsForm.DialogResult = DialogResult.OK;
							textlp = txtInput.Text.Trim();
							optionsForm.Close();
						}
					};
					panel.Controls.Add(txtInput);
					panel.Controls.Add(button);
					txtInput.Location = new Point(0, 0);
					optionsForm.Controls.Add(panel);
					Label value2 = new Label
					{
						Text = "此處輸入車牌不執行出入流程驗證，僅作為日志記錄。",
						Size = new Size(440, 30),
						Location = new Point(30, 145),
						TextAlign = ContentAlignment.MiddleCenter,
						ForeColor = Color.DimGray,
						Font = new Font("微软雅黑", 11.5f, FontStyle.Regular),
						Visible = false
					};
					optionsForm.Controls.Add(value2);
					string[] obj = new string[4] { "維修、保養、測試", "非車輛通行（人、貨物等）", "政府車輛（警車）", "其它" };
					FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
					{
						Location = new Point(40, 185),
						Size = new Size(420, 200),
						FlowDirection = FlowDirection.TopDown,
						WrapContents = false
					};
					string[] array = obj;
					foreach (string reason in array)
					{
						Button button2 = new Button
						{
							Text = reason,
							Size = new Size(415, 45),
							Font = new Font("微软雅黑", 16f),
							FlatStyle = FlatStyle.Standard,
							BackColor = Color.White,
							TextAlign = ContentAlignment.MiddleCenter
						};
						if (reason == "其它")
						{
							button2.Click += ShowOtherReasonDialog_Click;
						}
						else
						{
							button2.Click += delegate
							{
								optionsForm.DialogResult = DialogResult.OK;
								textlp = reason;
								optionsForm.Close();
							};
						}
						flowLayoutPanel.Controls.Add(button2);
					}
					optionsForm.Controls.Add(flowLayoutPanel);
					Button button3 = new Button
					{
						Text = "取消",
						Size = new Size(120, 45),
						Location = new Point(190, 410),
						Font = new Font("微软雅黑", 15f),
						BackColor = Color.LightGray,
						FlatStyle = FlatStyle.Standard
					};
					button3.Click += delegate
					{
						optionsForm.DialogResult = DialogResult.Cancel;
						optionsForm.Close();
					};
					optionsForm.Controls.Add(button3);
					optionsForm.ShowDialog();
					if (optionsForm.DialogResult == DialogResult.Cancel)
					{
						return;
					}
					try
					{
						LPDBHelper.LogOperation(1, textlp, (DataBuffer2018.CurrentStaff != null) ? DataBuffer2018.CurrentStaff.StaffCode : "Auto", Settings.Default.OnlyID);
					}
					catch (Exception)
					{
					}
				}
				Common._Carpark2018ServiceContext.CommunicationChannel.ManualUpBar(manualUpBarArgs);
				string param = "閘門：" + cn + "；\n車牌：" + textlp + "；\n操作員：" + manualUpBarArgs.ShiffCode;
				if (Settings.Default.zhaji == "1")
				{
					ExecuteHttpRequest("銀座手動起桿", param);
				}
			}
			catch (TimeoutException)
			{
				Global.ShowMessage("操作超時，請重新操作");
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3);
				Global.ShowMessage(ex3.Message);
			}
		}
		catch (TimeoutException)
		{
			Global.ShowMessage("操作超時，請重新操作");
		}
		catch (Exception ex5)
		{
			Global.ShowMessage(ex5.Message);
		}
	}

	private void btn_Camera_Click(object sender, EventArgs e)
	{
		AgainCameraArgs againCameraArgs = new AgainCameraArgs();
		againCameraArgs.GateID = mParkGate.GateID;
		againCameraArgs.LicensePlate = txt_LicensePlate.Text;
		try
		{
			Common._Carpark2018ServiceContext.CommunicationChannel.AgainCamera(againCameraArgs);
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			Global.ShowMessage(ex.Message);
		}
	}

	public void deviceState(GateStatus gs, DeviceStatus dsi, EnumDeviceType type)
	{
		Invoke((MethodInvoker)delegate
		{
			Logger.Debug("GateID:" + dsi.GateID + "\tDeviceStatus:" + type.ToString() + "\tACEStatus." + dsi.DeviceCode);
			if (type == EnumDeviceType.Ticket)
			{
				lab_TimeState.Text = LangManager.GetLangString("ACEStatus." + dsi.DeviceCode);
				lab_TimeState.Tag = dsi.DeviceCode;
				if (gs.ErrLvl == EnumErrorLevel.Normal)
				{
					lab_TimeState.ForeColor = Color.White;
				}
				else
				{
					lab_TimeState.ForeColor = Color.Red;
				}
			}
			else if (type == EnumDeviceType.Monthly)
			{
				lab_RentalState.Text = LangManager.GetLangString("ACEStatus." + dsi.DeviceCode);
				lab_RentalState.Tag = dsi.DeviceCode;
				if (dsi.DeviceCode == "1")
				{
					lab_RentalState.ForeColor = Color.White;
				}
				else
				{
					lab_RentalState.ForeColor = Color.Red;
				}
			}
			else if (type != EnumDeviceType.MacauPass)
			{
				if (type == EnumDeviceType.QuickPass)
				{
					lab_BOCState.Text = LangManager.GetLangString("ACEStatus." + dsi.DeviceCode);
					lab_BOCState.Tag = dsi.DeviceCode;
					if (dsi.DeviceCode == "1")
					{
						lab_BOCState.ForeColor = Color.White;
					}
					else
					{
						lab_BOCState.ForeColor = Color.Red;
					}
				}
			}
			else
			{
				lab_MPState.Text = LangManager.GetLangString("ACEStatus." + dsi.DeviceCode);
				lab_MPState.Tag = dsi.DeviceCode;
				if (dsi.DeviceCode == "1")
				{
					lab_MPState.ForeColor = Color.White;
				}
				else
				{
					lab_MPState.ForeColor = Color.Red;
				}
			}
		}, null);
	}

	public void DisabilityState(DisabilityPressArgs info)
	{
		Invoke((MethodInvoker)delegate
		{
			if (info.PressParkType == EnumParkType.Other)
			{
				D = info;
				btnDisability.Visible = true;
			}
			else if (info.PressParkType == EnumParkType.Charging)
			{
				E = info;
				btnElectric.Visible = true;
			}
		}, null);
	}

	private void btnDisability_Click(object sender, EventArgs e)
	{
		try
		{
			if (GetParkAreaCount("Disability") > 0)
			{
				using (FormDisabilityDialog formDisabilityDialog = new FormDisabilityDialog(lab_GateName.Text, D))
				{
					formDisabilityDialog.m_ParkGate = mParkGate;
					formDisabilityDialog.ShowDialog();
					btnDisability.Visible = false;
					btnElectric.Visible = false;
					return;
				}
			}
			btnDisability.Visible = false;
			Global.ShowMessage("傷殘車位已滿");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
	}

	private void btnElectric_Click(object sender, EventArgs e)
	{
		try
		{
			if (GetParkAreaCount("Electric") > 0)
			{
				using (FormDisabilityDialog formDisabilityDialog = new FormDisabilityDialog(lab_GateName.Text, E))
				{
					formDisabilityDialog.m_ParkGate = mParkGate;
					formDisabilityDialog.ShowDialog();
					btnDisability.Visible = false;
					btnElectric.Visible = false;
					return;
				}
			}
			btnElectric.Visible = false;
			Global.ShowMessage("電動車位已滿");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		labTime.Text = LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.labTime");
		labMonth.Text = LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.labMonth");
		labMP.Text = LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.labMP");
		labQP.Text = LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.labQP");
		toolTip1.SetToolTip(btnBarUp, LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.btnBarUp"));
		toolTip1.SetToolTip(btnDisability, LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.btnDisability"));
		toolTip1.SetToolTip(btnCancel, LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.btnCancel"));
		toolTip1.SetToolTip(btn_Camera, LangManager.GetLangString("CarPark.UserControls.UCGatesEX_Item.btn_Camera"));
		lab_TimeState.Text = LangManager.GetLangString("ACEStatus." + (string)lab_TimeState.Tag);
		lab_RentalState.Text = LangManager.GetLangString("ACEStatus." + (string)lab_RentalState.Tag);
		lab_MPState.Text = LangManager.GetLangString("ACEStatus." + (string)lab_MPState.Tag);
		lab_BOCState.Text = LangManager.GetLangString("ACEStatus." + (string)lab_BOCState.Tag);
		if (currentLang == SysLanguage.CHT)
		{
			lab_GateName.Text = cn;
		}
		else
		{
			lab_GateName.Text = en;
		}
	}

	private int GetParkAreaCount(string str)
	{
		try
		{
			int result = 0;
			int AreaID = Convert.ToInt32(Config.AreaCode);
			ChargeContext chargeContext = new ChargeContext();
			GetParkAreaReturn parkAreaExtend = chargeContext.CommunicationChannel.GetParkAreaExtend(getArg, out m_ParkAreaExtend, out m_ParkArea);
			chargeContext.CommunicationChannel.Disconnect();
			if (!parkAreaExtend.ISOK)
			{
				throw new Exception(parkAreaExtend.ErrCode);
			}
			if (str == "Disability")
			{
				result = m_ParkAreaExtend.Where((ParkAreaExtend m) => m.ParkTypeID == 4 && m.AreaID == AreaID).FirstOrDefault().TimeChargRemain;
			}
			else if (str == "Electric")
			{
				result = m_ParkAreaExtend.Where((ParkAreaExtend m) => m.ParkTypeID == 5 && m.AreaID == AreaID).FirstOrDefault().TimeChargRemain;
			}
			return result;
		}
		catch (Exception ex)
		{
			Logger.Error(ex);
			throw ex;
		}
	}

	private void FormManualUpBarDialog_m_FormManualUpBarDialog_Event(string str)
	{
		strLP = str;
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
		this.lab_TimeState = new System.Windows.Forms.Label();
		this.lab_GateName = new System.Windows.Forms.Label();
		this.txt_LicensePlate = new System.Windows.Forms.TextBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.btnDisability = new System.Windows.Forms.Button();
		this.btn_Camera = new System.Windows.Forms.Button();
		this.btnBarUp = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.labTime = new System.Windows.Forms.Label();
		this.lab_RentalState = new System.Windows.Forms.Label();
		this.labMonth = new System.Windows.Forms.Label();
		this.lab_MPState = new System.Windows.Forms.Label();
		this.labMP = new System.Windows.Forms.Label();
		this.lab_BOCState = new System.Windows.Forms.Label();
		this.labQP = new System.Windows.Forms.Label();
		this.btnElectric = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.lab_TimeState.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.lab_TimeState.ForeColor = System.Drawing.Color.White;
		this.lab_TimeState.Location = new System.Drawing.Point(71, 31);
		this.lab_TimeState.Name = "lab_TimeState";
		this.lab_TimeState.Size = new System.Drawing.Size(60, 16);
		this.lab_TimeState.TabIndex = 6;
		this.lab_TimeState.Text = "系統啟動";
		this.lab_GateName.BackColor = System.Drawing.Color.Transparent;
		this.lab_GateName.Font = new System.Drawing.Font("新細明體", 10f, System.Drawing.FontStyle.Bold);
		this.lab_GateName.ForeColor = System.Drawing.Color.Navy;
		this.lab_GateName.Location = new System.Drawing.Point(18, 6);
		this.lab_GateName.Name = "lab_GateName";
		this.lab_GateName.Size = new System.Drawing.Size(113, 22);
		this.lab_GateName.TabIndex = 7;
		this.lab_GateName.Text = "label1";
		this.lab_GateName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txt_LicensePlate.Font = new System.Drawing.Font("黑体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.txt_LicensePlate.Location = new System.Drawing.Point(25, 97);
		this.txt_LicensePlate.Name = "txt_LicensePlate";
		this.txt_LicensePlate.Size = new System.Drawing.Size(60, 26);
		this.txt_LicensePlate.TabIndex = 3;
		this.txt_LicensePlate.Text = "MO6654";
		this.btnDisability.BackColor = System.Drawing.Color.Transparent;
		this.btnDisability.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnDisability.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnDisability.FlatAppearance.BorderSize = 0;
		this.btnDisability.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnDisability.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnDisability.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDisability.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.btnDisability.ForeColor = System.Drawing.Color.White;
		this.btnDisability.Location = new System.Drawing.Point(58, 126);
		this.btnDisability.Name = "btnDisability";
		this.btnDisability.Size = new System.Drawing.Size(30, 20);
		this.btnDisability.TabIndex = 9;
		this.toolTip1.SetToolTip(this.btnDisability, "傷殘車通過");
		this.btnDisability.UseVisualStyleBackColor = false;
		this.btnDisability.Visible = false;
		this.btnDisability.Click += new System.EventHandler(btnDisability_Click);
		this.btn_Camera.BackColor = System.Drawing.Color.Transparent;
		this.btn_Camera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btn_Camera.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_Camera.FlatAppearance.BorderSize = 0;
		this.btn_Camera.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btn_Camera.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btn_Camera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btn_Camera.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.btn_Camera.ForeColor = System.Drawing.Color.White;
		this.btn_Camera.Location = new System.Drawing.Point(91, 101);
		this.btn_Camera.Name = "btn_Camera";
		this.btn_Camera.Size = new System.Drawing.Size(24, 19);
		this.btn_Camera.TabIndex = 10;
		this.toolTip1.SetToolTip(this.btn_Camera, "拍照");
		this.btn_Camera.UseVisualStyleBackColor = false;
		this.btn_Camera.Click += new System.EventHandler(btn_Camera_Click);
		this.btnBarUp.BackColor = System.Drawing.Color.Transparent;
		this.btnBarUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnBarUp.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnBarUp.FlatAppearance.BorderSize = 0;
		this.btnBarUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnBarUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnBarUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnBarUp.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.btnBarUp.ForeColor = System.Drawing.Color.White;
		this.btnBarUp.Location = new System.Drawing.Point(25, 126);
		this.btnBarUp.Name = "btnBarUp";
		this.btnBarUp.Size = new System.Drawing.Size(30, 20);
		this.btnBarUp.TabIndex = 9;
		this.toolTip1.SetToolTip(this.btnBarUp, "起杆");
		this.btnBarUp.UseVisualStyleBackColor = false;
		this.btnBarUp.Click += new System.EventHandler(button1_Click);
		this.btnCancel.BackColor = System.Drawing.Color.Transparent;
		this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnCancel.FlatAppearance.BorderSize = 0;
		this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.btnCancel.ForeColor = System.Drawing.Color.White;
		this.btnCancel.Location = new System.Drawing.Point(121, 103);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(30, 20);
		this.btnCancel.TabIndex = 9;
		this.toolTip1.SetToolTip(this.btnCancel, "傷殘車不通過");
		this.btnCancel.UseVisualStyleBackColor = false;
		this.btnCancel.Visible = false;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.labTime.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.labTime.ForeColor = System.Drawing.Color.White;
		this.labTime.Location = new System.Drawing.Point(18, 31);
		this.labTime.Name = "labTime";
		this.labTime.Size = new System.Drawing.Size(59, 16);
		this.labTime.TabIndex = 8;
		this.labTime.Text = "時租：";
		this.lab_RentalState.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.lab_RentalState.ForeColor = System.Drawing.Color.White;
		this.lab_RentalState.Location = new System.Drawing.Point(71, 47);
		this.lab_RentalState.Name = "lab_RentalState";
		this.lab_RentalState.Size = new System.Drawing.Size(60, 16);
		this.lab_RentalState.TabIndex = 6;
		this.lab_RentalState.Text = "label1";
		this.labMonth.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.labMonth.ForeColor = System.Drawing.Color.White;
		this.labMonth.Location = new System.Drawing.Point(18, 47);
		this.labMonth.Name = "labMonth";
		this.labMonth.Size = new System.Drawing.Size(66, 16);
		this.labMonth.TabIndex = 8;
		this.labMonth.Text = "月租：";
		this.lab_MPState.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.lab_MPState.ForeColor = System.Drawing.Color.White;
		this.lab_MPState.Location = new System.Drawing.Point(71, 63);
		this.lab_MPState.Name = "lab_MPState";
		this.lab_MPState.Size = new System.Drawing.Size(60, 16);
		this.lab_MPState.TabIndex = 6;
		this.lab_MPState.Text = "label1";
		this.labMP.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.labMP.ForeColor = System.Drawing.Color.White;
		this.labMP.Location = new System.Drawing.Point(18, 63);
		this.labMP.Name = "labMP";
		this.labMP.Size = new System.Drawing.Size(66, 16);
		this.labMP.TabIndex = 8;
		this.labMP.Text = "澳門通：";
		this.lab_BOCState.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.lab_BOCState.ForeColor = System.Drawing.Color.White;
		this.lab_BOCState.Location = new System.Drawing.Point(71, 79);
		this.lab_BOCState.Name = "lab_BOCState";
		this.lab_BOCState.Size = new System.Drawing.Size(60, 16);
		this.lab_BOCState.TabIndex = 6;
		this.lab_BOCState.Text = "label1";
		this.labQP.Font = new System.Drawing.Font("新細明體", 9f, System.Drawing.FontStyle.Bold);
		this.labQP.ForeColor = System.Drawing.Color.White;
		this.labQP.Location = new System.Drawing.Point(18, 79);
		this.labQP.Name = "labQP";
		this.labQP.Size = new System.Drawing.Size(66, 16);
		this.labQP.TabIndex = 8;
		this.labQP.Text = "銀聯：";
		this.btnElectric.BackColor = System.Drawing.Color.Transparent;
		this.btnElectric.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.btnElectric.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnElectric.FlatAppearance.BorderSize = 0;
		this.btnElectric.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
		this.btnElectric.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
		this.btnElectric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnElectric.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.btnElectric.ForeColor = System.Drawing.Color.White;
		this.btnElectric.Location = new System.Drawing.Point(94, 126);
		this.btnElectric.Name = "btnElectric";
		this.btnElectric.Size = new System.Drawing.Size(30, 20);
		this.btnElectric.TabIndex = 9;
		this.toolTip1.SetToolTip(this.btnElectric, "電動車通過");
		this.btnElectric.UseVisualStyleBackColor = false;
		this.btnElectric.Visible = false;
		this.btnElectric.Click += new System.EventHandler(btnElectric_Click);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.Controls.Add(this.btn_Camera);
		base.Controls.Add(this.btnBarUp);
		base.Controls.Add(this.btnElectric);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnDisability);
		base.Controls.Add(this.lab_BOCState);
		base.Controls.Add(this.lab_MPState);
		base.Controls.Add(this.lab_RentalState);
		base.Controls.Add(this.lab_TimeState);
		base.Controls.Add(this.labQP);
		base.Controls.Add(this.labMP);
		base.Controls.Add(this.labMonth);
		base.Controls.Add(this.labTime);
		base.Controls.Add(this.lab_GateName);
		base.Controls.Add(this.txt_LicensePlate);
		this.DoubleBuffered = true;
		base.Name = "UCGatesEX_Item";
		base.Size = new System.Drawing.Size(146, 136);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

		private void ExecuteHttpRequest(string param1, string param2)
		{
			System.Net.WebClient client = new System.Net.WebClient();
			string requestUri = Settings.Default.ReportPath + "park/php/angushw.php?param1=" + param1 + "&param2=" + param2;
			try
			{
				client.DownloadString(requestUri);
			}
			catch (Exception message)
			{
				Logger.Error(message);
			}
		}

	private void ShowOtherReasonDialog_Click(object sender, EventArgs e)
	{
		Form otherForm = new Form
		{
			Text = "輸入其它原因",
			Size = new Size(420, 200),
			StartPosition = FormStartPosition.CenterParent,
			FormBorderStyle = FormBorderStyle.FixedDialog,
			MaximizeBox = false,
			Font = new Font("微软雅黑", 11.5f),
			BackColor = Color.White
		};
		Label value = new Label
		{
			Text = "請輸入其它起桿原因",
			Location = new Point(20, 20),
			Size = new Size(380, 35),
			Font = new Font("微软雅黑", 15f, FontStyle.Bold),
			ForeColor = Color.Red,
			TextAlign = ContentAlignment.MiddleCenter
		};
		otherForm.Controls.Add(value);
		TextBox txtOther = new TextBox
		{
			Location = new Point(20, 60),
			Size = new Size(360, 35),
			Font = new Font("微软雅黑", 13f),
			BorderStyle = BorderStyle.FixedSingle
		};
		otherForm.Controls.Add(txtOther);
		Button button = new Button
		{
			Text = "確認",
			Size = new Size(100, 35),
			Location = new Point(70, 115),
			Font = new Font("微软雅黑", 11.5f),
			BackColor = Color.LightSteelBlue
		};
		button.Click += delegate
		{
			if (string.IsNullOrWhiteSpace(txtOther.Text))
			{
				MessageBox.Show("請填寫其它原因後再確認。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				textlp = txtOther.Text.Trim();
				otherForm.DialogResult = DialogResult.OK;
				otherForm.Close();
				Form form = ((Button)sender).FindForm();
				form.DialogResult = DialogResult.OK;
				form.Close();
			}
		};
		otherForm.Controls.Add(button);
		Button button2 = new Button
		{
			Text = "取消",
			Size = new Size(100, 35),
			Location = new Point(230, 115),
			Font = new Font("微软雅黑", 11f),
			BackColor = Color.LightGray
		};
		button2.Click += delegate
		{
			otherForm.DialogResult = DialogResult.Cancel;
			otherForm.Close();
		};
		otherForm.Controls.Add(button2);
		otherForm.ShowDialog();
	}
}
