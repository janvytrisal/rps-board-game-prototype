/*
 * Author: Jan Vytrisal
 */

using UnityEngine;
using UnityEditor;

/// <summary>
/// Draws property name with its integer value.
/// </summary>
[CustomPropertyDrawer(typeof(IntegerMask))]
public class IntegerMaskEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //Draw property name as a label.
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var valueRect = new Rect(position.x, position.y, position.width, position.height);

        //Draw integer value of the mask.
        SerializedProperty value = property.FindPropertyRelative("_value");
        EditorGUI.PropertyField(valueRect, value, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
