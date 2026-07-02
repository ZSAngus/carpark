using System;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;

namespace CarPark.DB;

public class EntityHelper
{
	public static void CopyEntity(EntityObject source, EntityObject dest)
	{
		Type type = source.GetType();
		Type type2 = dest.GetType();
		if (!type.Equals(type2))
		{
			throw new Exception("类型不匹配");
		}
		PropertyInfo[] properties = type2.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
		for (int i = 0; i < properties.Length; i++)
		{
			if (properties[i].CanWrite && properties[i].PropertyType.FullName != "System.Data.EntityKey")
			{
				try
				{
					properties[i].SetValue(dest, properties[i].GetValue(source, null), null);
				}
				catch (TargetInvocationException)
				{
				}
			}
		}
	}

	public static void CopyEntity(object source, EntityObject detail)
	{
		Type type = source.GetType();
		PropertyInfo[] pros = detail.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
		PropertyInfo[] properties = type.GetProperties();
		int i;
		for (i = 0; i < pros.Length; i++)
		{
			if (!pros[i].CanWrite || !(pros[i].PropertyType.FullName != "System.Data.EntityKey"))
			{
				continue;
			}
			try
			{
				PropertyInfo propertyInfo = properties.FirstOrDefault((PropertyInfo m) => m.Name == pros[i].Name);
				if (propertyInfo != null)
				{
					object value = propertyInfo.GetValue(source, null);
					Type propertyType = pros[i].PropertyType;
					if (typeof(decimal?) == propertyType && value.GetType() == typeof(decimal))
					{
						decimal? num = (decimal)Convert.ChangeType(value, typeof(decimal));
						pros[i].SetValue(detail, num, null);
					}
					else
					{
						pros[i].SetValue(detail, Convert.ChangeType(propertyInfo.GetValue(source, null), pros[i].PropertyType), null);
					}
				}
			}
			catch (TargetInvocationException)
			{
			}
		}
	}

	public static EntityObject GetClone(EntityObject source)
	{
		Type type = source.GetType();
		EntityObject entityObject = (EntityObject)Activator.CreateInstance(type);
		try
		{
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.CanWrite && propertyInfo.PropertyType.FullName != "System.Data.EntityKey")
				{
					propertyInfo.SetValue(entityObject, propertyInfo.GetValue(source, null), null);
				}
			}
		}
		catch
		{
		}
		return entityObject;
	}
}
