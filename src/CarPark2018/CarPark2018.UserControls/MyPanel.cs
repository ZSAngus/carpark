using System.Windows.Forms;

namespace CarPark2018.UserControls;

internal class MyPanel : Panel
{
	public MyPanel()
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
	}
}
