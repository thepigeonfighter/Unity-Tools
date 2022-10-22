using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GeorgeFabish
{
    /// <summary>
    /// Decorate a field to tell Unity that there is only one of these scriptable objects in your 
    /// game so go find it and hook it up auto-magically
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class SingletonSOAttribute : PropertyAttribute
    {
    }

}