using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GroupName : MonoBehaviour
{
    [Group("Group 1", "Red")]
    public int Var1;

    [Group("Group 1", "Green")]
    public int Var2;

    [Group("Group 2", "Yellow")]
    public string Var3;

    public string Var4;
    public string Var5;
}

public class GroupAttribute : PropertyAttribute
{
    public string Name;
    public Color Color;

    public GroupAttribute(string name, string color)
    {
        Name = name;
        switch (color.ToLower())
        {
            case "red":
                Color = Color.red;
                break;

            case "green":
                Color = Color.green;
                break;

            case "yellow":
                Color = Color.yellow;
                break;

            case "brown":
                Color = new Color(0.65f, 0.16f, 0.16f);
                break;

            default:
                Color = Color.white;
                break;
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(MonoBehaviour), true)]
public class GroupedInspector : Editor
{
    private class Group
    {
        public string Name;
        public Color Color;
        public List<SerializedProperty> Properties = new List<SerializedProperty>();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var groups = new Dictionary<string, Group>();
        var noGroupProperties = new List<SerializedProperty>();

        var iterator = serializedObject.GetIterator();
        iterator.NextVisible(true); // Skip the "m_Script" property

        while (iterator.NextVisible(false))
        {
            var property = serializedObject.FindProperty(iterator.propertyPath);
            var fieldInfo = target.GetType().GetField(iterator.propertyPath, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var attributes = fieldInfo?.GetCustomAttributes(typeof(GroupAttribute), true);
            if (attributes != null && attributes.Length > 0)
            {
                var attribute = attributes[0] as GroupAttribute;
                var groupName = attribute.Name;

                if (!groups.ContainsKey(groupName))
                {
                    groups[groupName] = new Group { Name = groupName, Color = attribute.Color };
                }

                groups[groupName].Properties.Add(property);
            }
            else
            {
                noGroupProperties.Add(property);
            }
        }

        // Display the groups first
        foreach (var group in groups.Values)
        {
            GUIStyle style = new GUIStyle(EditorStyles.foldout);
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = group.Color; // Normal color
            style.onNormal.textColor = Color.black; // Color when expanded
            bool foldout = EditorPrefs.GetBool(group.Name, false);
            bool newFoldout = EditorGUILayout.Foldout(foldout, group.Name, true, style);
            if (newFoldout != foldout)
            {
                EditorPrefs.SetBool(group.Name, newFoldout);
            }

            if (newFoldout)
            {
                EditorGUI.indentLevel++;
                foreach (var property in group.Properties)
                {
                    EditorGUILayout.PropertyField(property, true);
                }
                EditorGUI.indentLevel--;
            }
        }

        // Then display properties that don't have a group
        foreach (var property in noGroupProperties)
        {
            EditorGUILayout.PropertyField(property, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif