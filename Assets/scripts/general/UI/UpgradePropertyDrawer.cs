using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*[CustomPropertyDrawer(typeof(UpgradeSCDef))]
public class UpgradePropertyDrawer : PropertyDrawer
{
 
   
     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
     {
            //override logic
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty StatType = property.FindPropertyRelative("StatType");
        SerializedProperty Value = property.FindPropertyRelative("Value");

        Rect labelPosition = new Rect(position.x, position.y, position.width, position.height);

        position = EditorGUI.PrefixLabel(
            position,
            GUIUtility.GetControlID(FocusType.Passive),
            new GUIContent(
                StatType.objectReferenceValue.ToString()
                )
            );

        int indent = EditorGUI.indentLevel; 
        EditorGUI.indentLevel = 0;

        float widthSize = position.width / 2;
        float offsetSize = 2;

        Rect pos1 = new Rect(position.x, position.y, widthSize, position.height);
        Rect pos2 = new Rect(position.x + widthSize * 1, position.y, widthSize, position.height);

        EditorGUI.PropertyField(pos1, StatType, GUIContent.none);
        EditorGUI.PropertyField(pos2, Value, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();    
    }
}*/
