using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace NetStandardLibrary
{
    public class Class1
    {
        public static void TestDefineParameters()
        {
            Type[] mathArgs = { typeof(double), typeof(double) };

            var powerOf = new DynamicMethod("PowerOf",
                typeof(double),
                mathArgs,
                typeof(double).Module);

            ILGenerator il = powerOf.GetILGenerator(256);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Call, typeof(Math).GetMethod("Pow"));
            il.Emit(OpCodes.Ret);
            
            powerOf.DefineParameter(1, ParameterAttributes.In, "base");
            powerOf.DefineParameter(2, ParameterAttributes.Out, "exponent");
            
            object[] invokeArgs = { 2, 5 };
            object objRet = powerOf.Invoke(null, BindingFlags.ExactBinding, null, invokeArgs, new CultureInfo("en-us"));

            foreach (var parameter in powerOf.GetParameters())
            {
                Console.WriteLine($"Param: {parameter.Name} -- In?: {parameter.IsIn} -- Out?: {parameter.IsOut}");
            }
        } 
    }
}
