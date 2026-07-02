using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace CarPark2018.UserControls;

public class UCClock : UserControl
{
	private DateTime _dateTime;

	private SevenSegmentClockStyle _clockStyle = SevenSegmentClockStyle.DateAndTime;

	private Color _clockColor = Color.Black;

	private bool _isDrawShadow = true;

	private Timer _timer = null;

	private bool _isTimerEnable = false;

	private Graphics g = null;

	private Bitmap m_Bitmap = null;

	private int _clockStringWidth;

	private int _clockStringHeight;

	public bool IsDrawShadow
	{
		get
		{
			return _isDrawShadow;
		}
		set
		{
			_isDrawShadow = value;
			Invalidate();
		}
	}

	[Browsable(false)]
	public Timer Timer
	{
		get
		{
			return _timer;
		}
		set
		{
			_timer = value;
			if (_timer != null)
			{
				_timer.Tick += TimerOnTick;
			}
		}
	}

	public bool IsTimerEnable
	{
		get
		{
			return _isTimerEnable;
		}
		set
		{
			if (value)
			{
				if (_timer == null)
				{
					_timer = new Timer();
					_timer.Tick += TimerOnTick;
					_timer.Interval = 1000;
					_timer.Enabled = true;
				}
			}
			else if (_timer != null)
			{
				_timer.Enabled = false;
			}
			_isTimerEnable = value;
		}
	}

	public DateTime DateTime
	{
		get
		{
			return _dateTime;
		}
		set
		{
			_dateTime = value;
		}
	}

	public Color ClockColor
	{
		get
		{
			return _clockColor;
		}
		set
		{
			_clockColor = value;
			Invalidate();
		}
	}

	public SevenSegmentClockStyle SevenSegmentClockStyle
	{
		get
		{
			return _clockStyle;
		}
		set
		{
			_clockStyle = value;
			Invalidate();
		}
	}

	public int ClockStringWidth => _clockStringWidth;

	public int ClockStringHeight => _clockStringHeight;

	public void Start()
	{
		IsTimerEnable = true;
		Refresh();
	}

	public void Stop()
	{
		IsTimerEnable = false;
	}

	public UCClock()
	{
		Text = "Seven-Segment Clock";
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
		UpdateStyles();
		Init();
		_dateTime = DateTime.Now;
	}

	private void Init()
	{
		m_Bitmap = new Bitmap(base.Width, base.Height);
		g = Graphics.FromImage(m_Bitmap);
		g.CompositingQuality = CompositingQuality.HighQuality;
		g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
		g.SmoothingMode = SmoothingMode.HighQuality;
	}

	private void TimerOnTick(object obj, EventArgs ea)
	{
		DateTime now = DateTime.Now;
		now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
		if (now != _dateTime)
		{
			_dateTime = now;
			Invalidate();
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		m_Bitmap = DrawClock();
		Graphics graphics = e.Graphics;
		graphics.CompositingQuality = CompositingQuality.HighQuality;
		graphics.DrawImageUnscaled(m_Bitmap, 0, 0);
	}

	public Bitmap DrawClock()
	{
		return DrawClock(base.ClientRectangle);
	}

	private void SevenSegmentClock_Resize(object sender, EventArgs e)
	{
		Init();
		Refresh();
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		this.BackColor = System.Drawing.SystemColors.Control;
		base.Name = "UCClock";
		base.Size = new System.Drawing.Size(448, 64);
		base.Resize += new System.EventHandler(SevenSegmentClock_Resize);
		base.ResumeLayout(false);
	}

	public Bitmap DrawClock(Rectangle destRect)
	{
		m_Bitmap = new Bitmap(destRect.Width, destRect.Height);
		Graphics graphics = Graphics.FromImage(m_Bitmap);
		graphics.CompositingQuality = CompositingQuality.HighQuality;
		graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
		graphics.SmoothingMode = SmoothingMode.HighQuality;
		SevenSegmentDisplay sevenSegmentDisplay = new SevenSegmentDisplay(graphics);
		sevenSegmentDisplay.IsDrawShadow = _isDrawShadow;
		GraphicsState gstate = graphics.Save();
		graphics.TranslateTransform(destRect.X, destRect.Y);
		string empty = string.Empty;
		empty = ((_clockStyle == SevenSegmentClockStyle.TimeOnly) ? _dateTime.ToString("T", DateTimeFormatInfo.InvariantInfo) : ((_clockStyle != SevenSegmentClockStyle.DateOnly) ? (_dateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) + " " + _dateTime.ToString("T", DateTimeFormatInfo.InvariantInfo)) : _dateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo)));
		SizeF sizeF = sevenSegmentDisplay.MeasureString(empty, Font);
		float num = Math.Min((float)destRect.Width / sizeF.Width, (float)destRect.Height / sizeF.Height);
		Font font = new Font(Font.FontFamily, num * Font.SizeInPoints);
		sizeF = sevenSegmentDisplay.MeasureString(empty, font);
		_clockStringWidth = (int)sizeF.Width;
		_clockStringHeight = (int)sizeF.Height;
		sevenSegmentDisplay.DrawString(empty, font, new SolidBrush(_clockColor), ((float)destRect.Width - sizeF.Width) / 2f, ((float)destRect.Height - sizeF.Height) / 2f);
		graphics.Restore(gstate);
		return m_Bitmap;
	}
}
