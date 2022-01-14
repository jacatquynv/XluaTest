using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using XLuaTest;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DownloadAsset());
    }

    IEnumerator DownloadAsset()
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("https://drive.google.com/uc?export=download&id=1vZOZalanI7YBXcZhP0Xq8MqCiKObXwK2"))

        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
                var canvas = bundle.LoadAsset("Canvas");
                Instantiate(canvas);

                LuaBehaviour lua = Resources.Load<LuaBehaviour>("lua");
                TextAsset luaScript = bundle.LoadAsset<TextAsset>("Lua");
                lua.luaScript = luaScript;
                Instantiate(lua);

                Text text = GameObject.Find("Text").GetComponent<Text>();
                text.text = luaScript.text;
            }
        }
    }
}
