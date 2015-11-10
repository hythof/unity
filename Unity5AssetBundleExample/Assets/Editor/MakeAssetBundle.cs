using UnityEngine;
using UnityEditor;

public class MakeAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("MakedAssetBundle");
    }

    [MenuItem("Assets/Get AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        Debug.Log("AssetBundleCount: " + names.Length);
        foreach (var name in names)
        {
            Debug.Log("AssetBundle: " + name);
        }
    }
}