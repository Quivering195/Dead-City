using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    [MinMax(0f, 10f)]
    public float someFloat;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}

public class MinMaxAttribute : PropertyAttribute
{
    public float Min { get; private set; }
    public float Max { get; private set; }

    public MinMaxAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(MinMaxAttribute))]
public class MinMaxDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MinMaxAttribute minMax = (MinMaxAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.Float)
        {
            property.floatValue = Mathf.Clamp(property.floatValue, minMax.Min, minMax.Max);
            EditorGUI.PropertyField(position, property, label);
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            property.intValue = Mathf.Clamp(property.intValue, (int)minMax.Min, (int)minMax.Max);
            EditorGUI.PropertyField(position, property, label);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use MinMax with float or int.");
        }
    }
}

#endif