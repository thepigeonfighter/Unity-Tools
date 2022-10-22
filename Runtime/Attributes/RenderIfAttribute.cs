using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GeorgeFabish
{
    /// <summary>
    /// Decorate a field that you only want shown in the inspector under a certain boolean function
    /// which is on the same object with the name equal to <see cref="FunctionName"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RenderIfAttribute : PropertyAttribute
    {
        public string FunctionName { get; }

        public RenderIfAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }

}
