#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using static UnityEngine.UI.Button;
using UnityEngine.UI;


[CustomEditor(typeof(ButtonAnimation))]
public class ButtonAnimationEditor : ButtonEditor
{
    private SerializedProperty _onClick;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onClick = serializedObject.FindProperty("onClickMy");
    }

    public override void OnInspectorGUI()
    {
        ButtonAnimation component = (ButtonAnimation)target;

        component.PressedColor = EditorGUILayout.ColorField("Pressed Color", component.PressedColor);
        component.PressedScaleProcent = EditorGUILayout.Slider("Pressed Scale Procent", component.PressedScaleProcent, 0, 1);
        component.ScaleDuration = EditorGUILayout.FloatField("Scale duration", component.ScaleDuration);
        component.ChangeColorDuration = EditorGUILayout.FloatField("Change Color Duration", component.ChangeColorDuration);
        component.RectTransform = (RectTransform)EditorGUILayout.ObjectField("Rect transofrm", component.RectTransform, typeof(RectTransform), true);
        component.ButtonImage = (Image)EditorGUILayout.ObjectField("ButtonImage", component.ButtonImage, typeof(Image), true);
        component.Settings = (ButtonScriptableObject)EditorGUILayout.ObjectField("ButtonSettings", component.Settings, typeof(ButtonScriptableObject), true);

        serializedObject.Update();

        EditorGUILayout.PropertyField(_onClick, true);

        serializedObject.ApplyModifiedProperties();
    }
}

#endif