using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GeorgeFabish.Editor
{
    [CustomPropertyDrawer(typeof(RenderIfAttribute))]
    public class RenderIfAttributeDrawer : PropertyDrawer
    {
        private Func<bool> _shouldRenderFunc;
        private bool _isInitialized;
        private bool _renderedThisFrame;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _renderedThisFrame = true;
            if (!_isInitialized)
            {
                Initialize(property);
            }
            if (_shouldRenderFunc != null)
            {
                bool shouldRender = _shouldRenderFunc.Invoke();
                if (!shouldRender)
                {
                    _renderedThisFrame = false;
                    return;
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label);
                }

            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
        private void Initialize(SerializedProperty property)
        {

            var atr = attribute as RenderIfAttribute;
            var method = fieldInfo.DeclaringType.GetMethod(atr.FunctionName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            if (method == null)
            {
                Debug.LogWarning("Could not find method named " + atr.FunctionName);
            }
            else
            {
                bool isValidMethod = method.ReturnType == typeof(bool) && method.GetParameters().Length == 0;
                if (isValidMethod)
                {
                    if (method.IsStatic)
                    {
                        _shouldRenderFunc = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), method);
                    }
                    else
                    {
                        _shouldRenderFunc = (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), property.serializedObject.targetObject, atr.FunctionName);

                    }
                }
                else
                {
                    Debug.LogWarning($"Method {atr.FunctionName} has an invalid signature type. Expecting a function that returns a boolean with no arguments.");
                }
            }
            _isInitialized = true;
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!_renderedThisFrame)
            {
                return 0;
            }
            return base.GetPropertyHeight(property, label);
        }
    }
}
