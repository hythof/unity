# WWW.LoadFromCacheOrDownload()の挙動

正常読み込みできるか
|       |AssetBundle有|AssetBundle無|
|Cache有|     o       |      o      |
|Cache無|     o       |      x      |

Cacheを参照するか
|       |AssetBundle有|AssetBundle無|
|Cache有|     o       |      o      |
|Cache無|     o       |      o      |

AssetBundleを参照するか
|       |AssetBundle有|AssetBundle無|
|Cache有|     x       |      x      |
|Cache無|     o       |      o      |

※「file://」と「http://」の場合で同じ挙動

# アセットバンドル作成手順

1. 対象のフォルダを選択
2. Inspectorの最下部でフォルダに命名(今回はroot)
3. スクリプトでAssetBundle作成
::
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("MakedAssetBundle");
    }

4. 対象のフォルダ上で右クリックし「Build AssetBundles」を実行
5. スクリプトでアセットバンドルを読み込み
::
    IEnumerator Start()
    {
        while(!Caching.ready)
        {
            Debug.Log("... wait Caching.ready");
            yield return null;
        }
        Caching.CleanCache();

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
