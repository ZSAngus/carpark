using System;

namespace CarPark.Lib;

[Flags]
public enum ConfigActions
{
	Add = 2,
	All = 0,
	Browse = 0x10,
	Delete = 8,
	Detail = 0x20,
	Edit = 4,
	Non = 0x100,
	Print = 0x80,
	PrintPreview = 0x40,
	Save = 1
}
