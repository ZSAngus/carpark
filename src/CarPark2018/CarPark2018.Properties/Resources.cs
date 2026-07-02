using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CarPark2018.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				ResourceManager resourceManager = new ResourceManager("CarPark2018.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap beijing
	{
		get
		{
			object obj = ResourceManager.GetObject("beijing", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap beijing800_600
	{
		get
		{
			object obj = ResourceManager.GetObject("beijing800_600", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap beijing86
	{
		get
		{
			object obj = ResourceManager.GetObject("beijing86", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Image1
	{
		get
		{
			object obj = ResourceManager.GetObject("Image1", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Month800_600
	{
		get
		{
			object obj = ResourceManager.GetObject("Month800_600", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap MPasss800_600
	{
		get
		{
			object obj = ResourceManager.GetObject("MPasss800_600", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal Resources()
	{
	}
}
