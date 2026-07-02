using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CarPark.Device;

internal class Class3
{
	private static bool bool_0 = false;

	private static Hashtable hashtable_0 = new Hashtable();

	internal static void lLHifFIsCLsZtjvFfN0i()
	{
		if (!bool_0)
		{
			bool_0 = true;
			AppDomain.CurrentDomain.AssemblyResolve += smethod_0;
		}
	}

	private static Assembly smethod_0(object object_0, ResolveEventArgs resolveEventArgs_0)
	{
		lock (hashtable_0)
		{
			string text = resolveEventArgs_0.Name.Trim();
			object obj = hashtable_0[text];
			if (obj == null)
			{
				try
				{
					string text2 = smethod_1(text);
					Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
					foreach (Assembly assembly in assemblies)
					{
						if (assembly.GetName().Name.ToUpper() == text2.ToUpper())
						{
							return assembly;
						}
					}
				}
				catch
				{
				}
			}
			if (obj == null)
			{
				try
				{
					RSACryptoServiceProvider.UseMachineKeyStore = true;
					MD5 mD = new MD5CryptoServiceProvider();
					string text3 = smethod_1(text);
					byte[] bytes = Encoding.Unicode.GetBytes(text3);
					string text4 = "b0494a1f-4bd3-" + Convert.ToBase64String(mD.ComputeHash(bytes));
					Stream manifestResourceStream = typeof(Class3).Assembly.GetManifestResourceStream(text4);
					if (manifestResourceStream != null)
					{
						try
						{
							BinaryReader binaryReader = new BinaryReader(manifestResourceStream);
							binaryReader.BaseStream.Position = 0L;
							BinaryReader binaryReader2 = binaryReader;
							byte[] array = new byte[manifestResourceStream.Length];
							binaryReader2.Read(array, 0, array.Length);
							binaryReader2.Close();
							bool flag = false;
							Assembly assembly2 = null;
							try
							{
								assembly2 = Assembly.Load(array);
							}
							catch (FileLoadException)
							{
								flag = true;
							}
							catch (BadImageFormatException)
							{
								flag = true;
							}
							if (flag)
							{
								string path = Path.Combine(Path.Combine(Path.GetTempPath(), text4), text3 + ".dll");
								if (!File.Exists(path))
								{
									Directory.CreateDirectory(Path.GetDirectoryName(path));
									FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
									fileStream.Write(array, 0, array.Length);
									fileStream.Close();
								}
								assembly2 = Assembly.LoadFile(path);
								hashtable_0.Add(text, assembly2);
								return assembly2;
							}
							hashtable_0.Add(text, assembly2);
							return assembly2;
						}
						catch
						{
						}
					}
				}
				catch
				{
				}
				return null;
			}
			return (Assembly)obj;
		}
	}

	private static string smethod_1(string string_0)
	{
		string text = string_0.Trim();
		int num = text.IndexOf(',');
		if (num >= 0)
		{
			text = text.Substring(0, num);
		}
		return text;
	}
}
