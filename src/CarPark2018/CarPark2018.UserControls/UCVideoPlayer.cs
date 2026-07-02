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
using log4net;

namespace CarPark2018.UserControls;

public class UCVideoPlayer : UserControl
{
	private FilterInfoCollection videoDevices;

	private string[] tscbxCameras = new string[10];

	private string[] cmbFBL = new string[20];

	private ILog Logger;

	private IContainer components = null;

	private Panel panMain;

	private VideoSourcePlayer videoSourcePlayer;

	private System.Windows.Forms.PictureBox pictureBox3;

	private System.Windows.Forms.PictureBox pictureBox2;

	private System.Windows.Forms.PictureBox pictureBox1;

	public UCVideoPlayer()
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

	public bool TakePhoto()
	{
		try
		{
			if (videoSourcePlayer.IsRunning)
			{
				BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(videoSourcePlayer.GetCurrentVideoFrame().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
				PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
				pngBitmapEncoder.Frames.Add(BitmapFrame.Create(source));
				string text = Config.FreePicLocalPath + DateTime.Now.ToString("yyyyMMdd");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				text = text + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				using (Stream stream = File.Create(text))
				{
					pngBitmapEncoder.Save(stream);
				}
				ShowPhoto();
				return true;
			}
			MessageBox.Show("当前尚未连接摄像头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		catch (Exception ex)
		{
			MessageBox.Show("摄像头异常：" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
	}

	public void ShowPhoto()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Config.FreePicLocalPath + DateTime.Now.ToString("yyyyMMdd"));
		FileInfo[] files = directoryInfo.GetFiles();
		int num = 0;
		int num2 = 0;
		for (int num3 = files.Count(); num3 > 0; num3--)
		{
			if (files[num3 - 1].Extension == ".jpg")
			{
				num++;
				if (num > 3)
				{
					break;
				}
				try
				{
					string filename = Config.FreePicLocalPath + DateTime.Now.ToString("yyyyMMdd") + "\\" + files[num3 - 1].Name;
					switch (num)
					{
					case 1:
						pictureBox1.Image = Image.FromFile(filename);
						break;
					case 2:
						pictureBox2.Image = Image.FromFile(filename);
						break;
					case 3:
						pictureBox3.Image = Image.FromFile(filename);
						break;
					}
				}
				catch (Exception ex)
				{
					Logger.Error(ex);
					Console.WriteLine(ex.Message);
				}
				num2 += 167;
			}
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
		this.panMain = new System.Windows.Forms.Panel();
		this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.panMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		base.SuspendLayout();
		this.panMain.Controls.Add(this.pictureBox3);
		this.panMain.Controls.Add(this.pictureBox2);
		this.panMain.Controls.Add(this.pictureBox1);
		this.panMain.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panMain.Location = new System.Drawing.Point(0, 265);
		this.panMain.Name = "panMain";
		this.panMain.Size = new System.Drawing.Size(500, 100);
		this.panMain.TabIndex = 1;
		this.videoSourcePlayer.Location = new System.Drawing.Point(0, 3);
		this.videoSourcePlayer.Name = "videoSourcePlayer";
		this.videoSourcePlayer.Size = new System.Drawing.Size(500, 256);
		this.videoSourcePlayer.TabIndex = 0;
		this.videoSourcePlayer.Text = "videoSourcePlayer1";
		this.videoSourcePlayer.VideoSource = null;
		this.pictureBox1.Location = new System.Drawing.Point(4, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(160, 95);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.pictureBox2.Location = new System.Drawing.Point(170, 3);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(160, 95);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 0;
		this.pictureBox2.TabStop = false;
		this.pictureBox3.Location = new System.Drawing.Point(336, 2);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Size = new System.Drawing.Size(160, 95);
		this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox3.TabIndex = 0;
		this.pictureBox3.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(16f, 35f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.panMain);
		base.Controls.Add(this.videoSourcePlayer);
		this.Font = new System.Drawing.Font("微软雅黑", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
		base.Name = "UCVideoPlayer";
		base.Size = new System.Drawing.Size(500, 365);
		base.Load += new System.EventHandler(UCVideoPlayer_Load);
		this.panMain.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		base.ResumeLayout(false);
	}
}
