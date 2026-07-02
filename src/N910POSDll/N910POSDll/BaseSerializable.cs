using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace N910POSDll;

[Serializable]
public class BaseSerializable
{
	protected static string configPath;

	static BaseSerializable()
	{
		configPath = null;
	}

	public BaseSerializable()
	{
		configPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\{GetType().Name}.dat";
	}

	public virtual T CreateNew<T>() where T : BaseSerializable
	{
		return (T)Activator.CreateInstance(GetType());
	}

	public static T FromBinaryConfig<T>() where T : BaseSerializable
	{
		object obj = null;
		configPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\{typeof(T).Name}.dat";
		if (File.Exists(configPath))
		{
			obj = FromBinaryConfig<T>(File.ReadAllBytes(configPath));
		}
		return (T)obj;
	}

	public static T FromBinaryConfig<T>(byte[] Config) where T : BaseSerializable
	{
		object obj = null;
		using (MemoryStream serializationStream = new MemoryStream(Config))
		{
			obj = new BinaryFormatter().Deserialize(serializationStream);
		}
		return (T)obj;
	}

	public static T FromJSon<T>(string string_0)
	{
		DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
		using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(string_0));
		return (T)dataContractJsonSerializer.ReadObject(stream);
	}

	public static T FromSOAPConfig<T>(byte[] Config) where T : BaseSerializable
	{
		object obj = null;
		using (MemoryStream serializationStream = new MemoryStream(Config))
		{
			obj = new SoapFormatter().Deserialize(serializationStream);
		}
		return (T)obj;
	}

	public static T FromSOAPConfig<T>(string Config) where T : BaseSerializable
	{
		return FromSOAPConfig<T>(Encoding.UTF8.GetBytes(Config));
	}

	public static T FromXmlConfig<T>() where T : BaseSerializable
	{
		return FromXmlConfig<T>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\{typeof(T).Name}.xml", Encoding.UTF8));
	}

	public static T FromXmlConfig<T>(byte[] Config) where T : BaseSerializable
	{
		object obj = null;
		using (MemoryStream stream = new MemoryStream(Config))
		{
			obj = new XmlSerializer(typeof(T)).Deserialize(stream);
		}
		return (T)obj;
	}

	public static T FromXmlConfig<T>(string Config) where T : BaseSerializable
	{
		return FromXmlConfig<T>(Encoding.UTF8.GetBytes(Config));
	}

	public static string GetDefaultConfigPath<T>()
	{
		return $"{AppDomain.CurrentDomain.BaseDirectory}\\{typeof(T).Name}.dat";
	}

	public virtual void SaveBinaryConfig()
	{
		configPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\{GetType().Name}.dat";
		File.WriteAllBytes(configPath, ToBinaryConfig());
	}

	public virtual void SaveToXmlConfig(string configFilePath)
	{
		File.WriteAllText(configFilePath, ToXmlConfig(), Encoding.UTF8);
	}

	public virtual void SaveXmlConfig()
	{
		File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\{GetType().Name}.xml", ToXmlConfig(), Encoding.UTF8);
	}

	public virtual byte[] ToBinaryConfig()
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, this);
		return memoryStream.ToArray();
	}

	public virtual byte[] ToBinarySOAPConfig()
	{
		SoapFormatter soapFormatter = new SoapFormatter();
		using MemoryStream memoryStream = new MemoryStream();
		soapFormatter.Serialize(memoryStream, this);
		return memoryStream.ToArray();
	}

	public virtual byte[] ToBinaryXmlConfig()
	{
		XmlSerializer xmlSerializer = new XmlSerializer(GetType());
		using MemoryStream memoryStream = new MemoryStream();
		xmlSerializer.Serialize(memoryStream, this);
		return memoryStream.ToArray();
	}

	public static string ToJson(object graph)
	{
		DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(graph.GetType());
		using MemoryStream memoryStream = new MemoryStream();
		dataContractJsonSerializer.WriteObject(memoryStream, graph);
		return Encoding.UTF8.GetString(memoryStream.ToArray());
	}

	public virtual string ToSOAPConfig()
	{
		return Encoding.UTF8.GetString(ToBinarySOAPConfig());
	}

	public virtual string ToXmlConfig()
	{
		return Encoding.UTF8.GetString(ToBinaryXmlConfig());
	}
}
