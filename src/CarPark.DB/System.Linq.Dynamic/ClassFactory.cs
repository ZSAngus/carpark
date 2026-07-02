using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace System.Linq.Dynamic;

internal class ClassFactory
{
	public static readonly ClassFactory Instance;

	private ModuleBuilder module;

	private Dictionary<Signature, Type> classes;

	private int classCount;

	private ReaderWriterLock rwLock;

	static ClassFactory()
	{
		Instance = new ClassFactory();
	}

	private ClassFactory()
	{
		AssemblyName name = new AssemblyName("DynamicClasses");
		AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
		module = assemblyBuilder.DefineDynamicModule("Module");
		classes = new Dictionary<Signature, Type>();
		rwLock = new ReaderWriterLock();
	}

	public Type GetDynamicClass(IEnumerable<DynamicProperty> properties)
	{
		rwLock.AcquireReaderLock(-1);
		try
		{
			Signature signature = new Signature(properties);
			if (!classes.TryGetValue(signature, out var value))
			{
				value = CreateDynamicClass(signature.properties);
				classes.Add(signature, value);
			}
			return value;
		}
		finally
		{
			rwLock.ReleaseReaderLock();
		}
	}

	private Type CreateDynamicClass(DynamicProperty[] properties)
	{
		LockCookie lockCookie = rwLock.UpgradeToWriterLock(-1);
		try
		{
			string name = "DynamicClass" + (classCount + 1);
			TypeBuilder typeBuilder = module.DefineType(name, TypeAttributes.Public, typeof(DynamicClass));
			FieldInfo[] fields = GenerateProperties(typeBuilder, properties);
			GenerateEquals(typeBuilder, fields);
			GenerateGetHashCode(typeBuilder, fields);
			Type result = typeBuilder.CreateType();
			classCount++;
			return result;
		}
		finally
		{
			rwLock.DowngradeFromWriterLock(ref lockCookie);
		}
	}

	private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
	{
		FieldInfo[] array = new FieldBuilder[properties.Length];
		for (int i = 0; i < properties.Length; i++)
		{
			DynamicProperty dynamicProperty = properties[i];
			FieldBuilder fieldBuilder = tb.DefineField("_" + dynamicProperty.Name, dynamicProperty.Type, FieldAttributes.Private);
			PropertyBuilder propertyBuilder = tb.DefineProperty(dynamicProperty.Name, PropertyAttributes.HasDefault, dynamicProperty.Type, null);
			MethodBuilder methodBuilder = tb.DefineMethod("get_" + dynamicProperty.Name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, dynamicProperty.Type, Type.EmptyTypes);
			ILGenerator iLGenerator = methodBuilder.GetILGenerator();
			iLGenerator.Emit(OpCodes.Ldarg_0);
			iLGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
			iLGenerator.Emit(OpCodes.Ret);
			MethodBuilder methodBuilder2 = tb.DefineMethod("set_" + dynamicProperty.Name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, null, new Type[1] { dynamicProperty.Type });
			ILGenerator iLGenerator2 = methodBuilder2.GetILGenerator();
			iLGenerator2.Emit(OpCodes.Ldarg_0);
			iLGenerator2.Emit(OpCodes.Ldarg_1);
			iLGenerator2.Emit(OpCodes.Stfld, fieldBuilder);
			iLGenerator2.Emit(OpCodes.Ret);
			propertyBuilder.SetGetMethod(methodBuilder);
			propertyBuilder.SetSetMethod(methodBuilder2);
			array[i] = fieldBuilder;
		}
		return array;
	}

	private void GenerateEquals(TypeBuilder tb, FieldInfo[] fields)
	{
		MethodBuilder methodBuilder = tb.DefineMethod("Equals", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool), new Type[1] { typeof(object) });
		ILGenerator iLGenerator = methodBuilder.GetILGenerator();
		LocalBuilder local = iLGenerator.DeclareLocal(tb);
		Label label = iLGenerator.DefineLabel();
		iLGenerator.Emit(OpCodes.Ldarg_1);
		iLGenerator.Emit(OpCodes.Isinst, tb);
		iLGenerator.Emit(OpCodes.Stloc, local);
		iLGenerator.Emit(OpCodes.Ldloc, local);
		iLGenerator.Emit(OpCodes.Brtrue_S, label);
		iLGenerator.Emit(OpCodes.Ldc_I4_0);
		iLGenerator.Emit(OpCodes.Ret);
		iLGenerator.MarkLabel(label);
		foreach (FieldInfo fieldInfo in fields)
		{
			Type fieldType = fieldInfo.FieldType;
			Type type = typeof(EqualityComparer<>).MakeGenericType(fieldType);
			label = iLGenerator.DefineLabel();
			iLGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
			iLGenerator.Emit(OpCodes.Ldarg_0);
			iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
			iLGenerator.Emit(OpCodes.Ldloc, local);
			iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
			iLGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("Equals", new Type[2] { fieldType, fieldType }), null);
			iLGenerator.Emit(OpCodes.Brtrue_S, label);
			iLGenerator.Emit(OpCodes.Ldc_I4_0);
			iLGenerator.Emit(OpCodes.Ret);
			iLGenerator.MarkLabel(label);
		}
		iLGenerator.Emit(OpCodes.Ldc_I4_1);
		iLGenerator.Emit(OpCodes.Ret);
	}

	private void GenerateGetHashCode(TypeBuilder tb, FieldInfo[] fields)
	{
		MethodBuilder methodBuilder = tb.DefineMethod("GetHashCode", MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(int), Type.EmptyTypes);
		ILGenerator iLGenerator = methodBuilder.GetILGenerator();
		iLGenerator.Emit(OpCodes.Ldc_I4_0);
		foreach (FieldInfo fieldInfo in fields)
		{
			Type fieldType = fieldInfo.FieldType;
			Type type = typeof(EqualityComparer<>).MakeGenericType(fieldType);
			iLGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
			iLGenerator.Emit(OpCodes.Ldarg_0);
			iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
			iLGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("GetHashCode", new Type[1] { fieldType }), null);
			iLGenerator.Emit(OpCodes.Xor);
		}
		iLGenerator.Emit(OpCodes.Ret);
	}
}
