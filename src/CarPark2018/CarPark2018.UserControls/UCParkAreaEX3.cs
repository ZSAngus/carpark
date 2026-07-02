using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CarPark2018.UserControls;

public class UCParkAreaEX3 : UserControl
{
	private IContainer components = null;

	public UCParkAreaEX3()
	{
		InitializeComponent();
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
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Transparent;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.Name = "UCParkAreaEX3";
		base.ResumeLayout(false);
	}
}
