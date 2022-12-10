using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class UnityExtension
{
    [MenuItem("Edit/Dupulicate")]
    private static void Copy()
    {
        if (Selection.assetGUIDs != null && 0 < Selection.assetGUIDs.Length)
        {
            var list = new List<Object>();
            foreach (var n in Selection.assetGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(n);
                var uniquePath = AssetDatabase.GenerateUniqueAssetPath(path);
                AssetDatabase.CopyAsset(path, uniquePath);
                AssetDatabase.Refresh();
                var asset = AssetDatabase.LoadAssetAtPath(uniquePath, typeof(Object));
                list.Add(asset);
            }
            Selection.objects = list.ToArray();
        }

        if (Selection.gameObjects != null && 0 < Selection.gameObjects.Length)
        {
            
            var list = new List<Object>();
            foreach (var n in Selection.gameObjects)
            {
                var clone = GameObject.Instantiate(n);
                var parent = n.transform.parent;
                clone.transform.SetParent(parent);
                clone.transform.SetSiblingIndex(parent == null ? 0 : parent.transform.childCount - 1);
                clone.transform.localPosition = n.transform.localPosition;
                clone.transform.localRotation = n.transform.localRotation;
                clone.transform.localScale = n.transform.localScale;
                clone.name = n.name;
                clone.name = GameObjectUtility.GetUniqueNameForSibling(parent, clone.name);
                list.Add(clone);
            }
            Selection.objects = list.ToArray();
        }
    }
}