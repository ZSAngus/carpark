using System.Drawing;

namespace CarPark2018.UserControls;

internal class SevenSegmentDisplay
{
	private Graphics grfx;

	private Brush _brush = Brushes.Black;

	private bool _isDrawShadow = true;

	private Color _shadowColor = Color.FromArgb(60, Color.White);

	private Brush _shadowBrush = null;

	private static byte[,] bySegment = new byte[10, 7]
	{
		{ 1, 1, 1, 0, 1, 1, 1 },
		{ 0, 0, 1, 0, 0, 1, 0 },
		{ 1, 0, 1, 1, 1, 0, 1 },
		{ 1, 0, 1, 1, 0, 1, 1 },
		{ 0, 1, 1, 1, 0, 1, 0 },
		{ 1, 1, 0, 1, 0, 1, 1 },
		{ 1, 1, 0, 1, 1, 1, 1 },
		{ 1, 0, 1, 0, 0, 1, 0 },
		{ 1, 1, 1, 1, 1, 1, 1 },
		{ 1, 1, 1, 1, 0, 1, 1 }
	};

	private readonly Point[][] apt = new Point[7][];

	public bool IsDrawShadow
	{
		get
		{
			return _isDrawShadow;
		}
		set
		{
			_isDrawShadow = value;
		}
	}

	public SevenSegmentDisplay(Graphics grfx)
	{
		this.grfx = grfx;
		apt[0] = new Point[4]
		{
			new Point(3, 2),
			new Point(39, 2),
			new Point(31, 10),
			new Point(11, 10)
		};
		apt[1] = new Point[4]
		{
			new Point(2, 3),
			new Point(10, 11),
			new Point(10, 31),
			new Point(2, 35)
		};
		apt[2] = new Point[4]
		{
			new Point(40, 3),
			new Point(40, 35),
			new Point(32, 31),
			new Point(32, 11)
		};
		apt[3] = new Point[6]
		{
			new Point(3, 36),
			new Point(11, 32),
			new Point(31, 32),
			new Point(39, 36),
			new Point(31, 40),
			new Point(11, 40)
		};
		apt[4] = new Point[4]
		{
			new Point(2, 37),
			new Point(10, 41),
			new Point(10, 61),
			new Point(2, 69)
		};
		apt[5] = new Point[4]
		{
			new Point(40, 37),
			new Point(40, 69),
			new Point(32, 61),
			new Point(32, 41)
		};
		apt[6] = new Point[4]
		{
			new Point(11, 62),
			new Point(31, 62),
			new Point(39, 70),
			new Point(3, 70)
		};
	}

	public SizeF MeasureString(string str, Font font)
	{
		SizeF result = new SizeF(0f, grfx.DpiX * font.SizeInPoints / 72f);
		for (int i = 0; i < str.Length; i++)
		{
			if (char.IsDigit(str[i]))
			{
				result.Width += 42f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
			}
			else if (str[i] == '-')
			{
				result.Width += 42f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
			}
			else if (str[i] == ':')
			{
				result.Width += 20f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
			}
			else if (str[i] == ' ')
			{
				result.Width += 36f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
			}
		}
		return result;
	}

	public void DrawString(string str, Font font, Brush brush, float x, float y)
	{
		_brush = brush;
		_shadowBrush = new SolidBrush(Color.Transparent);
		for (int i = 0; i < str.Length; i++)
		{
			if (char.IsDigit(str[i]))
			{
				x = Number(str[i] - 48, font, brush, x, y);
			}
			else if (str[i] == '-')
			{
				x = MinusSign(font, brush, x, y);
			}
			else if (str[i] == ':')
			{
				x = Colon(font, brush, x, y);
			}
			else if (str[i] == ' ')
			{
				x = DrawSpace(font, brush, x, y);
			}
		}
	}

	private float Number(int num, Font font, Brush brush, float x, float y)
	{
		for (int i = 0; i < apt.Length; i++)
		{
			if (_isDrawShadow)
			{
				Fill(apt[i], font, _shadowBrush, x, y);
			}
			if (bySegment[num, i] == 1)
			{
				Fill(apt[i], font, brush, x, y);
			}
		}
		return x + 42f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
	}

	private float MinusSign(Font font, Brush brush, float x, float y)
	{
		Fill(apt[3], font, brush, x, y);
		return x + 42f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
	}

	private float DrawSpace(Font font, Brush brush, float x, float y)
	{
		return x + 36f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
	}

	private float Colon(Font font, Brush brush, float x, float y)
	{
		Point[][] array = new Point[2][]
		{
			new Point[4]
			{
				new Point(4, 12),
				new Point(16, 12),
				new Point(16, 24),
				new Point(4, 24)
			},
			new Point[4]
			{
				new Point(4, 50),
				new Point(16, 50),
				new Point(16, 62),
				new Point(4, 62)
			}
		};
		for (int i = 0; i < array.Length; i++)
		{
			Fill(array[i], font, brush, x, y);
		}
		return x + 20f * grfx.DpiX * font.SizeInPoints / 72f / 72f;
	}

	private void Fill(Point[] apt, Font font, Brush brush, float x, float y)
	{
		PointF[] array = new PointF[apt.Length];
		for (int i = 0; i < apt.Length; i++)
		{
			array[i].X = x + (float)apt[i].X * grfx.DpiX * font.SizeInPoints / 72f / 72f;
			array[i].Y = y + (float)apt[i].Y * grfx.DpiY * font.SizeInPoints / 72f / 72f;
		}
		grfx.FillPolygon(brush, array);
	}
}
