using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PDK.ASPNET
{
    public class ATU
    {
        //public static string Get(Action action) => "";
        //public static string Get<TResult>(Func<TResult> action) => "";
        //public static string Get<T1, TResult>(Func<T1, TResult> action) => "";
        //public static string Get<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> action) => "";
    }
    public delegate int Getc<T1, T2>(T1 arg1, T2 arg2);
}
