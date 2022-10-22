using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
namespace GeorgeFabish.Editor
{
    [CustomPropertyDrawer(typeof(LocalDependencyAttribute))]
    public class LocalDependencyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            if (property.objectReferenceValue == null)
            {
                var gb = property.serializedObject.targetObject as MonoBehaviour;
                if (gb != null)
                {

                    Component component = null;
                    LocalDependencyAttribute atr = attribute as LocalDependencyAttribute;
                    switch (atr.Location)
                    {
                        case LocalDependencyAttribute.DependencyLocation.OnGameObject:
                            component = gb.GetComponent(fieldInfo.FieldType);
                            break;
                        case LocalDependencyAttribute.DependencyLocation.InParent:
                            component = gb.GetComponentInParent(fieldInfo.FieldType, atr.SearchInactiveObjects);
                            break;
                        case LocalDependencyAttribute.DependencyLocation.InChildren:
                            component = gb.GetComponentInChildren(fieldInfo.FieldType, atr.SearchInactiveObjects);
                            break;

                    }
                    if (component != null)
                    {
                        property.objectReferenceValue = component;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                    else
                    {
                        Debug.LogWarning($"Field {fieldInfo.Name} decorated with LocalDependency attribute could not find dependency of type {fieldInfo.FieldType.Name}");
                    }
                }
            }
            EditorGUI.BeginProperty(position, label, property);
            if (property.objectReferenceValue == null)
            {
                label.text = $"Something went wrong trying to load local dependency";
            }
            else
            {
                label.text = $"{property.displayName} : {fieldInfo.FieldType}";
            }
            EditorGUI.LabelField(position, label);
            EditorGUI.EndProperty();

        }
    }

}
