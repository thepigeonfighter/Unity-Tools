using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GeorgeFabish
{
    /// <summary>
    /// Decorate scriptable object fields with this attribute to draw their field data
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ExtendedSOEditorAttribute : PropertyAttribute
    {

    }

}