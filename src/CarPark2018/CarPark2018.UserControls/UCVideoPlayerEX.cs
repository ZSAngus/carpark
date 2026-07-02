using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using AForge.Controls;
using AForge.Video.DirectShow;
using CarPark2018.Properties;
using log4net;

namespace CarPark2018.UserControls;

public class UCVideoPlayerEX : UserControl
{
	public delegate void DoSavePicFieidPath(string time, string picName, string imageName);

	private FilterInfoCollection videoDevices;

	private string[] tscbxCameras = new string[10];

	private string[] cmbFBL = new string[20];

	private ILog Logger;

	private IContainer components = null;

	private VideoSourcePlayer videoSourcePlayer;

	public UCVideoPlayerEX()
	{
		InitializeComponent();
		Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	}

	public bool LoadVideoDevices()
	{
		try
		{
			if (videoDevices == null)
			{
				videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			}
			if (videoDevices.Count == 0)
			{
				return false;
			}
			int num = 0;
			foreach (FilterInfo videoDevice in videoDevices)
			{
				tscbxCameras[num] = videoDevice.Name;
				num++;
			}
			return true;
		}
		catch
		{
			throw;
		}
	}

	public bool LoadVideoFBL()
	{
		if (tscbxCameras.Count() <= 0)
		{
			return false;
		}
		VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice(videoDevices[0].MonikerString);
		if (videoCaptureDevice.VideoCapabilities.Count() == 0)
		{
			return false;
		}
		cmbFBL = new string[20];
		int num = 0;
		VideoCapabilities[] videoCapabilities = videoCaptureDevice.VideoCapabilities;
		foreach (VideoCapabilities videoCapabilities2 in videoCapabilities)
		{
			cmbFBL[num] = videoCapabilities2.FrameSize.Width + "*" + videoCapabilities2.FrameSize.Height;
			num++;
		}
		return true;
	}

	public void CloseVideo()
	{
		if (videoSourcePlayer != null && videoSourcePlayer.IsRunning)
		{
			videoSourcePlayer.SignalToStop();
			videoSourcePlayer.WaitForStop();
		}
	}

	public bool OpenVideo()
	{
		CloseVideo();
		if (tscbxCameras.Count() <= 0)
		{
			MessageBox.Show("未发现摄像头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		VideoCaptureDevice videoCaptureDevice = new VideoCaptureDevice(videoDevices[0].MonikerString);
		videoCaptureDevice.VideoResolution = videoCaptureDevice.VideoCapabilities[0];
		videoSourcePlayer.VideoSource = videoCaptureDevice;
		videoSourcePlayer.Start();
		return true;
	}

	public string TakePhoto()
	{
		try
		{
			if (videoSourcePlayer.IsRunning)
			{
				BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(videoSourcePlayer.GetCurrentVideoFrame().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
				PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
				pngBitmapEncoder.Frames.Add(BitmapFrame.Create(source));
				string text = DateTime.Now.ToString("yyyy-MM-dd");
				string text2 = Config.FreePicLocalPath + "P" + Config.AreaCode + "\\" + text + "\\" + Settings.Default.OnlyID;
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				string text3 = "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
				if (File.Exists(text2 + text3))
				{
					File.Delete(text2 + text3);
				}
				using (Stream stream = File.Create(text2 + text3))
				{
					pngBitmapEncoder.Save(stream);
				}
				DoSavePicFieidPath doSavePicFieidPath = SavePicFieidPath;
				doSavePicFieidPath.BeginInvoke(text, text2, text3, null, null);
				return "\\P" + Config.AreaCode + "\\" + text + "\\" + Settings.Default.OnlyID + text3;
			}
			Logger.Error("当前尚未连接摄像头");
			return null;
		}
		catch (Exception ex)
		{
			Logger.Error("摄像头异常：" + ex.Message);
			return null;
		}
	}

	private void SavePicFieidPath(string time, string picName, string imageName)
	{
		try
		{
			string text = Config.FreePicFieldPath + "P" + Config.AreaCode + "\\" + time + "\\" + Settings.Default.OnlyID;
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			text += imageName;
			File.Copy(picName + imageName, text);
		}
		catch (Exception message)
		{
			Logger.Error(message);
		}
	}

	private void UCVideoPlayer_Load(object sender, EventArgs e)
	{
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
		this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
		base.SuspendLayout();
		this.videoSourcePlayer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.videoSourcePlayer.Location = new System.Drawing.Point(0, 0);
		this.videoSourcePlayer.Name = "videoSourcePlayer";
		this.videoSourcePlayer.Size = new System.Drawing.Size(277, 165);
		this.videoSourcePlayer.TabIndex = 0;
		this.videoSourcePlayer.Text = "videoSourcePlayer1";
		this.videoSourcePlayer.VideoSource = null;
		base.AutoScaleDimensions = new System.Drawing.SizeF(16f, 35f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.videoSourcePlayer);
		this.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
		base.Name = "UCVideoPlayerEX";
		base.Size = new System.Drawing.Size(277, 165);
		base.Load += new System.EventHandler(UCVideoPlayer_Load);
		base.ResumeLayout(false);
	}
}
