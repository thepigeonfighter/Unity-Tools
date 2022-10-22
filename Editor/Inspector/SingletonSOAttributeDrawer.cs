using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace GeorgeFabish.Editor
{
    [CustomPropertyDrawer(typeof(SingletonSOAttribute))]
    public class SingletonSOAttributeDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!fieldInfo.FieldType.IsSubclassOf(typeof(ScriptableObject)))
            {
                Debug.LogWarning("Singleton SO attributes must only be used on fields that subclass Scriptable Object");

            }
            else if (property.objectReferenceValue == null)
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{fieldInfo.FieldType.Name}");
                ScriptableObject so = null;
                if (guids.Length != 0)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    so = (ScriptableObject)AssetDatabase.LoadAssetAtPath(path, fieldInfo.FieldType);
                }
                if (so == null)
                {
                    Debug.LogWarning($"Could not find instance of {fieldInfo.FieldType.Name}");
                }
                else
                {
                    property.objectReferenceValue = so;
                    property.serializedObject.ApplyModifiedProperties();
                }


            }
            EditorGUI.BeginProperty(position, label, property);
            if (property.objectReferenceValue == null)
            {
                label.text = $"Something went wrong trying to Singleton Scriptable Object";
            }
            else
            {
                label.text = $"{property.displayName} : {fieldInfo.FieldType.Name}";
            }
            EditorGUI.LabelField(position, label);
            EditorGUI.EndProperty();
        }
    } 
}
