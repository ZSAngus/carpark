using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarPark.Core;
using CarPark.DB;
using CarPark2018.Properties;
using Master.SystemCommunication.Lib;
using SkyInno.Lang;
using SkyInno.UI.BindingText;
using log4net;

namespace CarPark2018.UserControls;

public class UCPasstraceEX2 : UserControl
{
	public class AutoCloseMessageBox : IDisposable
	{
		private Timer _timer;

		private Form _form;

		private DialogResult _result;

		public AutoCloseMessageBox(string text, string caption, int timeoutMs, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			_form = new Form
			{
				Width = 500,
				Height = 250,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				StartPosition = FormStartPosition.CenterScreen,
				Text = caption,
				TopMost = true,
				Font = new Font("微软雅黑", 12f)
			};
			Label value = new Label
			{
				Text = text.Replace("\n", Environment.NewLine),
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleCenter,
				Font = new Font("微软雅黑", 16f, FontStyle.Bold),
				ForeColor = Color.DarkBlue,
				Padding = new Padding(20),
				AutoSize = true
			};
			Button button = new Button
			{
				Text = "是",
				DialogResult = DialogResult.Yes,
				Font = new Font("微软雅黑", 16f),
				Size = new Size(120, 50),
				Margin = new Padding(10)
			};
			Button button2 = new Button
			{
				Text = "否",
				DialogResult = DialogResult.No,
				Font = new Font("微软雅黑", 16f),
				Size = new Size(120, 50),
				Margin = new Padding(10)
			};
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
			{
				Dock = DockStyle.Bottom,
				FlowDirection = FlowDirection.RightToLeft,
				Padding = new Padding(20),
				AutoSize = true
			};
			flowLayoutPanel.Controls.AddRange(new Control[2] { button2, button });
			_form.Controls.Add(value);
			_form.Controls.Add(flowLayoutPanel);
			_timer = new Timer
			{
				Interval = timeoutMs
			};
			_timer.Tick += delegate
			{
				_result = DialogResult.No;
				_form.Close();
			};
		}

		public DialogResult ShowDialog()
		{
			_timer.Start();
			_result = _form.ShowDialog();
			return _result;
		}

		public void Dispose()
		{
			if (_timer != null)
			{
				_timer.Dispose();
			}
			if (_form != null)
			{
				_form.Dispose();
			}
		}
	}

	public class SingleBtnMessageBox : IDisposable
	{
		private Form _form;

		private DialogResult _result;

		public SingleBtnMessageBox(string text, string caption, MessageBoxIcon icon = MessageBoxIcon.Exclamation)
		{
			_form = new Form
			{
				Width = 500,
				Height = 250,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				StartPosition = FormStartPosition.CenterScreen,
				Text = caption,
				TopMost = true,
				Font = new Font("微软雅黑", 12f)
			};
			Label value = new Label
			{
				Text = text.Replace("\n", Environment.NewLine),
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleCenter,
				Font = new Font("微软雅黑", 16f, FontStyle.Bold),
				ForeColor = Color.DarkBlue,
				Padding = new Padding(20),
				AutoSize = true
			};
			Button value2 = new Button
			{
				Text = "確認",
				DialogResult = DialogResult.OK,
				Font = new Font("微软雅黑", 16f),
				Size = new Size(120, 50),
				Margin = new Padding(10)
			};
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
			{
				Dock = DockStyle.Bottom,
				FlowDirection = FlowDirection.RightToLeft,
				Padding = new Padding(20),
				AutoSize = true
			};
			flowLayoutPanel.Controls.Add(value2);
			_form.Controls.Add(value);
			_form.Controls.Add(flowLayoutPanel);
		}

		public DialogResult ShowDialog()
		{
			_result = _form.ShowDialog();
			return _result;
		}

		public void Dispose()
		{
			if (_form != null)
			{
				_form.Dispose();
			}
		}
	}

	private static ILog Logger;

	private BindingSource bs;

	private IContainer components;

	private int currentTrace;

	private DataGridView dataMain;

	private Panel panMain;

	private PictureBox pictureBox;

	private string previousItem;

	public string StoredAnalysisResult { get; set; } = string.Empty;

	static UCPasstraceEX2()
	{
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public UCPasstraceEX2()
	{
		InitializeComponent();
		SetDGVStyle(dataMain);
		bs = new BindingSource();
		currentTrace = 0;
		LangManager.LanguageChangedEvent += LangManager_LanguageChangedEvent;
		LangManager_LanguageChangedEvent(SysLanguage.CHT);
	}

	private void InitBinding()
	{
		dataMain.Columns.Clear();
		bs.Clear();
		BindingHelper.BindDataGridView<PassTrace>(bs, dataMain, new DataGridBindingAttr[7]
		{
			new DataGridBindingAttr(PropertyHelper<PassTrace>.GetProperty((PassTrace m) => m.PassCardCode), 130),
			new DataGridBindingAttr(PropertyHelper<PassTrace>.GetProperty((PassTrace m) => m.PassTimeHR), 80),
			new DataGridBindingAttr(PropertyHelper<PassTrace>.GetProperty((PassTrace m) => m.PassGateID), 80),
			new DataGridBindingAttr(PropertyHelper<PassTrace>.GetProperty((PassTrace m) => m.ParkTypeID), 100),
			new DataGridBindingAttr(PropertyHelper<PassTrace>.GetProperty((PassTrace m) => m.PassBillType), 130),
			new DataGridBindingAttr(PropertyHelper<PassTrace>.GetProperty((PassTrace m) => m.PassStatus), 100),
			new DataGridBindingAttr(PropertyHelper<PassTrace>.GetProperty((PassTrace m) => m.PassRemark), 380)
		});
	}

	private void LangManager_LanguageChangedEvent(SysLanguage currentLang)
	{
		InitBinding();
	}

	private void RefreshDataGrid()
	{
		for (int i = 0; i < bs.Count; i++)
		{
			PassTrace passTrace = bs[i] as PassTrace;
			if (passTrace.ExtendEnumPassDirection == EnumPassDirection.IN)
			{
				dataMain.Rows[i].DefaultCellStyle.ForeColor = Color.LightGreen;
			}
			else
			{
				dataMain.Rows[i].DefaultCellStyle.ForeColor = Color.LightBlue;
			}
			if (!string.IsNullOrEmpty(StoredAnalysisResult) && passTrace.PassCardCode == StoredAnalysisResult)
			{
				dataMain.Rows[i].Cells[0].Tag = passTrace.PassCardCode;
				dataMain.Rows[i].Cells[0].Style.BackColor = Color.LightYellow;
			}
			else
			{
				dataMain.Rows[i].Cells[0].Tag = null;
				dataMain.Rows[i].Cells[0].Style.BackColor = Color.Empty;
			}
			if (passTrace.ExtendEnumPassStatus == EnumPassStatus.Error)
			{
				if (passTrace.PassRemarkCn == "未付費" || passTrace.PassRemarkCn == "在綫檢查不通過")
				{
					dataMain.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
					dataMain.Rows[i].DefaultCellStyle.ForeColor = Color.DarkRed;
				}
				else
				{
					dataMain.Rows[i].DefaultCellStyle.BackColor = Color.DarkRed;
				}
			}
			else
			{
				dataMain.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
			}
			if (passTrace.PassRemarkCn != null && passTrace.PassRemarkCn.Contains("請審核"))
			{
				dataMain.Rows[i].DefaultCellStyle.BackColor = Color.Red;
			}
		}
	}

	private void AddData(PassTrace dataPass)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				for (int i = 0; i < 15; i++)
				{
					PassTrace passTrace = new PassTrace
					{
						ExtendEnumPassDirection = EnumPassDirection.IN,
						ExtendEnumPassStatus = EnumPassStatus.Normal,
						ParkTypeID = 1,
						PassBillType = 1,
						PassCardCode = "620531******4293",
						PassGateID = 1,
						PassTime = DateTime.Now,
						PassRemarkCode = "123333",
						PassRemarkCn = "jjj",
						PassRemarkPt = "ooo",
						RentalTypeID = null
					};
					passTrace.PassGateID = i;
					bs.Add(passTrace);
					currentTrace = passTrace.PassTraceID;
				}
				bs.MoveLast();
				RefreshDataGrid();
			});
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void UCPasstraceEX2_Load(object sender, EventArgs e)
	{
	}

	private void dataMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	private void SetDGVStyle(DataGridView dgv)
	{
		DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
		dgv.AllowUserToAddRows = false;
		dgv.AllowUserToDeleteRows = false;
		dgv.AllowUserToResizeColumns = false;
		dgv.AllowUserToResizeRows = false;
		dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = SystemColors.Window;
		dataGridViewCellStyle.Font = new Font("Times New Roman", 15f);
		dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = SystemColors.ControlText;
		dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
		dgv.DefaultCellStyle = dataGridViewCellStyle;
		dgv.GridColor = Color.FromArgb(208, 215, 229);
		dgv.ReadOnly = true;
		dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
		dgv.RowTemplate.Height = 30;
		dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 15f);
		dgv.ScrollBars = ScrollBars.None;
	}

	public void Add(PassTrace context)
	{
		try
		{
			Invoke((MethodInvoker)delegate
			{
				Tuple<string, string> analysisResultByPassTraceID = LPDBHelper.GetAnalysisResultByPassTraceID(context.PassTraceID, context.PassCardCode, context.ParkTypeID);
				string item = analysisResultByPassTraceID.Item1;
				_ = analysisResultByPassTraceID.Item2;
				if (context.PassGateID == 2)
				{
					StoredAnalysisResult = ((context.PassStatus == 1) ? item : string.Empty);
				}
				if (context.PassGateID == 1)
				{
					if (Settings.Default.verify != "0" && context.PassStatus == 2)
					{
						if (previousItem != null && previousItem == item)
						{
							context.PassRemarkCn = "請審核";
						}
						else
						{
							previousItem = item;
						}
					}
					if (Settings.Default.verify == "1" && (context.PassBillType == 12 || context.PassBillType == 0))
					{
						context.PassRemarkCn = "請審核";
					}
				}
				if (context.PassStatus == 2 && context.PassBillType == 10)
				{
					if (context.PassCardCode.StartsWith("13"))
					{
						context.PassRemarkCn = "MPark";
					}
					else if (context.PassCardCode.StartsWith("14"))
					{
						context.PassRemarkCn = "大豐";
					}
				}
				if (context.PassRemarkCn != null)
				{
					context.PassRemarkCn = context.PassRemarkCn.Replace("GateErrorCodes.", "");
					context.PassRemarkCn = context.PassRemarkCn.Replace("澳門通無感提示:扣費失敗。詳情:支付失敗:澳門通查詢付費結果沒有支付,未支付(1002)", "澳門通無感:扣費失敗。客戶未支付");
					context.PassRemarkCn = context.PassRemarkCn.Replace("澳門通無感提示:扣費失敗。詳情:支付失敗:", "澳門通無感:扣費失敗。");
				}
				if (!string.IsNullOrEmpty(item))
				{
					if (context.PassBillType != 1 || context.PassStatus != 1)
					{
						context.PassCardCode = item;
					}
					if (Settings.Default.verify != "0" && (context.PassBillType == 12 || context.PassBillType == 0) && context.PassStatus == 2)
					{
						string text = item;
						bool flag = false;
						if (text.StartsWith("CM") && context.ParkTypeID == 2)
						{
							if (text.Length != 7 || !text.Substring(2).All(char.IsDigit))
							{
								flag = true;
							}
						}
						else if (text.StartsWith("EX") || text.StartsWith("ES"))
						{
							string text2 = text.Substring(2);
							if (text2.Length < 1 || text2.Length > 4 || !text2.All(char.IsDigit))
							{
								flag = true;
							}
						}
						else if (text.StartsWith("M") && text.Length == 5)
						{
							if (!text.Substring(1).All(char.IsDigit))
							{
								flag = true;
							}
						}
						else if (text.Length == 6)
						{
							string source = text.Substring(0, 2);
							string source2 = text.Substring(2);
							if (!source.All(char.IsLetter) || !source2.All(char.IsDigit))
							{
								flag = true;
							}
						}
						else
						{
							flag = true;
						}
						if (flag && context.PassGateID == 1)
						{
							context.PassRemarkCn = "請審核";
						}
					}
				}
				if (context.PassStatus == 2 && context.PassGateID == 1 && Settings.Default.OnlyID == "CashierA" && (context.PassBillType == 12 || context.PassBillType == 0 || context.PassBillType == 10))
				{
					Tuple<int?, int?, string> tuple = LPDBHelper.CheckFreeRegisterExists(context.PassCardCode, context.ParkTypeID, context.PassTime);
					int? item2 = tuple.Item1;
					int? item3 = tuple.Item2;
					string item4 = tuple.Item3;
					int? transactionID = context.TransactionID;
					if (item2.HasValue && item3.HasValue && transactionID.HasValue)
					{
						LPDBHelper.SetFreeRecord(item2.Value, item3.Value, transactionID.Value);
						context.PassRemarkCn = item4;
					}
					if (bs.Count > 0 && bs[bs.Count - 1] is PassTrace { PassRemarkCn: "該卡已進場" })
					{
						Task.Factory.StartNew(delegate
						{
							bool isMonthIn = LPDBHelper.CheckMonthIn(item, context.ParkTypeID);
							BeginInvoke((Action)delegate
							{
								if (isMonthIn)
								{
									using (SingleBtnMessageBox singleBtnMessageBox = new SingleBtnMessageBox("車牌：" + item + "為車證車輛， \n車證和時鐘重複入場，\n請核查原因，避免錯誤收費！", "車輛核查"))
									{
										singleBtnMessageBox.ShowDialog();
									}
								}
							});
						});
					}
				}
				int[] source3 = new int[4] { 0, 3, 10, 12 };
				if (context.PassStatus == 2 && context.PassGateID == 2 && Settings.Default.OnlyID == "CashierA" && source3.Contains(context.PassBillType) && context.TransactionID.HasValue)
				{
					string totalChargeByTransactionID = LPDBHelper.GetTotalChargeByTransactionID(context.TransactionID.Value);
					PassTrace passTrace2 = context;
					passTrace2.PassRemarkCn = passTrace2.PassRemarkCn + "收費:" + totalChargeByTransactionID;
				}
				bool flag2 = context.PassStatus == 1;
				bool flag3 = context.PassRemarkCn == "沒有進場紀錄" || ((context.PassBillType == 11 || context.PassBillType == 12) && context.PassRemarkCn == "該卡已進場");
				if (Settings.Default.OnlyID == "CashierA" && flag2 && flag3 && LPDBHelper.IsRecentPassMatched(context.PassCardCode, context.ParkTypeID, context.PassTime, context.PassGateID))
				{
					context.PassRemarkCn = "查詢已存在完成記錄，自動起桿";
					Task.Factory.StartNew(delegate
					{
						try
						{
							TryManualUpBar(context.PassGateID);
						}
						catch (Exception message2)
						{
							Logger.Error(message2);
						}
					});
				}
				if (bs.Count >= 50)
				{
					bs.RemoveAt(0);
				}
				bs.Add(context);
				currentTrace = context.PassTraceID;
				bs.MoveLast();
				RefreshDataGrid();
				dataMain.ClearSelection();
			});
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
		this.dataMain = new System.Windows.Forms.DataGridView();
		this.panMain = new System.Windows.Forms.Panel();
		this.pictureBox = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.dataMain).BeginInit();
		this.panMain.SuspendLayout();
		base.SuspendLayout();
		this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox.Visible = false;
		this.pictureBox.BackColor = System.Drawing.Color.Transparent;
		base.Controls.Add(this.pictureBox);
		this.dataMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataMain.Location = new System.Drawing.Point(0, 0);
		this.dataMain.Name = "dataMain";
		this.dataMain.RowTemplate.Height = 10;
		this.dataMain.Size = new System.Drawing.Size(150, 150);
		this.dataMain.TabIndex = 7;
		this.dataMain.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dataMain_DataError);
		this.dataMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataMain_CellClick);
		this.dataMain.MouseMove += new System.Windows.Forms.MouseEventHandler(dataMain_MouseMove);
		this.dataMain.MouseLeave += new System.EventHandler(dataMain_MouseLeave);
		this.panMain.Controls.Add(this.dataMain);
		this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panMain.Font = new System.Drawing.Font("Times New Roman", 24f);
		this.panMain.Location = new System.Drawing.Point(0, 0);
		this.panMain.Name = "panMain";
		this.panMain.Size = new System.Drawing.Size(150, 150);
		this.panMain.TabIndex = 1;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.Controls.Add(this.panMain);
		this.DoubleBuffered = true;
		base.Name = "UCPasstraceEX2";
		base.Load += new System.EventHandler(UCPasstraceEX2_Load);
		((System.ComponentModel.ISupportInitialize)this.dataMain).EndInit();
		this.panMain.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void dataMain_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
			{
				return;
			}
			DataGridViewCell dataGridViewCell = dataMain.Rows[e.RowIndex].Cells[e.ColumnIndex];
			PassTrace passTrace = bs[e.RowIndex] as PassTrace;
			if (dataMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
			{
				dataMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
				using AutoCloseMessageBox autoCloseMessageBox = new AutoCloseMessageBox("是否再次發起" + StoredAnalysisResult + "車輛出口驗證，\n自動為出口閘機輸入車牌？", "出口確認", 20000, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (autoCloseMessageBox.ShowDialog() == DialogResult.Yes)
				{
					ManualOpenGate();
				}
			}
			if (!(dataGridViewCell.Value?.ToString() == "請審核") || (passTrace.PassBillType != 12 && passTrace.PassBillType != 0) || passTrace.PassGateID != 1)
			{
				return;
			}
			Invoke((MethodInvoker)delegate
			{
				foreach (object item in bs.List)
				{
					if (item is PassTrace { PassRemarkCn: "請審核" } passTrace2 && (passTrace2.PassBillType == 12 || passTrace2.PassBillType == 0) && passTrace2.PassGateID == 1)
					{
						passTrace2.PassRemarkCn = string.Empty;
					}
				}
				bs.ResetBindings(metadataChanged: false);
				RefreshDataGrid();
				int? num = null;
				foreach (StaffType staffType in DataBuffer2018.StaffTypes)
				{
					if (DataBuffer2018.CurrentStaff.StaffTypeId == staffType.StaffTypeID)
					{
						num = staffType.SystemCode;
						break;
					}
				}
				if (num.HasValue)
				{
					Process.Start(Settings.Default.ReportPath + "park/verify.html?StaffCode=" + DataBuffer2018.CurrentStaff.StaffCode + "&StaffPwd=" + DataBuffer2018.CurrentStaff.StaffPwd + "&StaffId=" + DataBuffer2018.CurrentStaff.StaffId + "&StaffName=" + DataBuffer2018.CurrentStaff.StaffName + "&StaffTypeId=" + DataBuffer2018.CurrentStaff.StaffTypeId);
				}
			});
		}
		catch (Exception exception)
		{
			Logger.Error("操作失败", exception);
		}
	}

	private void dataMain_MouseMove(object sender, MouseEventArgs e)
	{
		DataGridView.HitTestInfo hitTestInfo = dataMain.HitTest(e.X, e.Y);
		if (hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex == 0)
		{
			if (bs[hitTestInfo.RowIndex] is PassTrace passTrace)
			{
				string item = LPDBHelper.GetAnalysisResultByPassTraceID(passTrace.PassTraceID, passTrace.PassCardCode, passTrace.ParkTypeID).Item2;
				string filename = Config.LicensePlatePath + item;
				if (!string.IsNullOrEmpty(item))
				{
					pictureBox.Image = Image.FromFile(filename);
					pictureBox.Location = new Point(130, 0);
					pictureBox.Size = new Size(490, 275);
					pictureBox.Visible = true;
				}
			}
		}
		else
		{
			pictureBox.Visible = false;
		}
	}

	private void dataMain_MouseLeave(object sender, EventArgs e)
	{
		pictureBox.Visible = false;
	}

	private void ManualOpenGate()
	{
		try
		{
			ManualUpBarArgs obj = new ManualUpBarArgs(Settings.Default.OnlyID)
			{
				GateID = 2,
				OperationPC = Settings.Default.OnlyID,
				ShiffCode = DataBuffer2018.CurrentStaff.StaffCode
			};
			string storedAnalysisResult = StoredAnalysisResult;
			obj.Extend1 = storedAnalysisResult;
			ManualUpBarArgs manualUpBarArgs = obj;
			Common._Carpark2018ServiceContext.CommunicationChannel.ManualUpBar(manualUpBarArgs);
		}
		catch (TimeoutException)
		{
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void TryManualUpBar(int gateid)
	{
		try
		{
			ManualUpBarArgs manualUpBarArgs = new ManualUpBarArgs(Settings.Default.OnlyID)
			{
				GateID = gateid,
				OperationPC = Settings.Default.OnlyID,
				ShiffCode = DataBuffer2018.CurrentStaff.StaffCode,
				Extend1 = ""
			};
			Common._Carpark2018ServiceContext.CommunicationChannel.ManualUpBar(manualUpBarArgs);
		}
		catch (Exception)
		{
		}
	}
}
