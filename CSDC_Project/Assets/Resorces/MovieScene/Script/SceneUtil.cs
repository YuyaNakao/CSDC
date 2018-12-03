using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class SceneUtil
{

    // シーン事のリストを取得する
    public static List<string> GetSceneNames()
    {
        int SceneCount = SceneManager.sceneCount;
        List<string> SceneNames = new List<string>();
        for(int i= 0; i< SceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            SceneNames.Add(scene.name);
        }
        return SceneNames;
    }

    public static void MoveScene(List<string> sceneNames)
    {
        List<string> SceneNames = GetSceneNames();
        Dictionary<string,bool> SceneNameMap = SceneNames.ToDictionary(name => name,name => true);

        // シーンのロード
        foreach(string SceneName in sceneNames)
        {
            if(!SceneNameMap.ContainsKey(SceneName))
            {
                SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
            }
        }

        // シーンのアンロード
        foreach(string UnSceneName in SceneNames)
        {
            if(!SceneNameMap.ContainsKey(UnSceneName))
            {
                SceneManager.UnloadScene(UnSceneName);
            }
        }
    }
}
