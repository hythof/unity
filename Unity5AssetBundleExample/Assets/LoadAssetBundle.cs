using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadAssetBundle : MonoBehaviour {
    [SerializeField]
    Image image;

    IEnumerator Start()
    {
        while(!Caching.ready)
        {
            Debug.Log("... wait Caching.ready");
            yield return null;
        }

        var path = "file://" + Application.dataPath + "/../MakedAssetBundle/root";
        var www = WWW.LoadFromCacheOrDownload(path, 0);
        Debug.Log("start ... " + path);

        Debug.Log("wait www ...");
        yield return www;
        if(!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError(www.error);
            yield break;
        }
        var spriteAsset = www.assetBundle.LoadAssetAsync<Sprite>("lgtm_cat");

        Debug.Log("wait loadAssetAsync ... " + spriteAsset.progress);
        while (!spriteAsset.isDone)
        {
            Debug.Log("... " + spriteAsset.progress);
            yield return null;
        }

        Debug.Log("set sprite");
        var sprite = spriteAsset.asset as Sprite;
        Debug.Log(sprite);
        image.sprite = sprite;
    }
}
