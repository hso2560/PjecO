using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetBundle : MonoBehaviour
{
    [SerializeField] string assetBundleName = "bundle0";
    [SerializeField] string url;

    void LoadAsset()
    {
        StartCoroutine(InstantiateObject());
    }

    IEnumerator InstantiateObject()
    {
       
        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(req);

        GameObject o = Instantiate(bundle.LoadAsset<GameObject>("Square"));
        

    }
}
