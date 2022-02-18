﻿using UnityEngine;

namespace MalbersAnimations
{
    public class CreateScriptableAssetAttribute : PropertyAttribute
    {
        public bool isAsset = true;

        public CreateScriptableAssetAttribute(bool isAsset) => this.isAsset = isAsset;
        public CreateScriptableAssetAttribute() => this.isAsset = true;

    }


#if UNITY_EDITOR
    [UnityEditor.CustomPropertyDrawer(typeof(CreateScriptableAssetAttribute), true)]
    public class CreateAssetDrawer : UnityEditor.PropertyDrawer
    {
        // private Color color = new Color(1,0.4f,0.4f,1);

        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            label = UnityEditor.EditorGUI.BeginProperty(position, label, property);
            position = UnityEditor.EditorGUI.PrefixLabel(position, label);


            var element = property.objectReferenceValue;

            var attr = attribute as CreateScriptableAssetAttribute;

            if (element == null)
            {
                position.width -= 22;
                UnityEditor.EditorGUI.PropertyField(position, property, GUIContent.none);
                var AddButtonRect = new Rect(position) { x = position.width + position.x + 2, width = 20 };

                if (GUI.Button(AddButtonRect, "+"))
                {
                    if (attr.isAsset)
                        MTools.CreateScriptableAsset(property, MTools.Get_Type(property), MTools.GetSelectedPathOrFallback());
                    else
                        MTools.CreateScriptableAssetInternal(property, MTools.Get_Type(property));
                }
            }
            else
            {
                UnityEditor.EditorGUI.PropertyField(position, property, GUIContent.none);
            }
        }
    }
#endif
}