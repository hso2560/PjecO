using UnityEditor;
using UnityEngine;

public class BuildAssetBundles : MonoBehaviour
{
   
    [MenuItem("Bundles/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}
