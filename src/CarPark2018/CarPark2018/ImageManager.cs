using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CarPark2018;

public class ImageManager
{
	private static string AppPath = Application.StartupPath + "/Image";

	public static Dictionary<string, Dictionary<string, Image>> imageList = new Dictionary<string, Dictionary<string, Image>>();

	public static void InitPicImageList()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(AppPath);
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		FileInfo[] files = directoryInfo.GetFiles();
		imageList.Add("", new Dictionary<string, Image>());
		FileInfo[] array = files;
		foreach (FileInfo fileInfo in array)
		{
			if (fileInfo.Extension == ".png" && !imageList.Keys.Contains(fileInfo.Name))
			{
				imageList[""].Add(fileInfo.Name, Image.FromFile(fileInfo.FullName));
			}
		}
		DirectoryInfo[] array2 = directories;
		foreach (DirectoryInfo directoryInfo2 in array2)
		{
			if (!imageList.Keys.Contains(directoryInfo2.Name))
			{
				imageList.Add(directoryInfo2.Name, new Dictionary<string, Image>());
			}
			files = directoryInfo2.GetFiles();
			FileInfo[] array3 = files;
			foreach (FileInfo fileInfo2 in array3)
			{
				if (fileInfo2.Extension == ".png" && !imageList.Keys.Contains(fileInfo2.Name))
				{
					imageList[directoryInfo2.Name].Add(fileInfo2.Name, Image.FromFile(fileInfo2.FullName));
				}
			}
		}
	}

	public static Image GetImage(string FolderName, string FileName)
	{
		FileName += ".png";
		if (imageList.Keys.Contains(FolderName) && imageList[FolderName].Keys.Contains(FileName))
		{
			return imageList[FolderName][FileName];
		}
		return null;
	}
}
