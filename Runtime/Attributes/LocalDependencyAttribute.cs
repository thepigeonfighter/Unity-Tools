using System;
using UnityEngine;
namespace GeorgeFabish
{
    /// <summary>
    /// Decorate field that hooks up dependency automatically by looking either on the gameObject
    /// or in its parent or children for the required component.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class LocalDependencyAttribute : PropertyAttribute
    {
        public enum DependencyLocation { OnGameObject, InParent, InChildren }
        public DependencyLocation Location { get; set; } = DependencyLocation.OnGameObject;
        public bool SearchInactiveObjects { get; set; } = false;

    }

}