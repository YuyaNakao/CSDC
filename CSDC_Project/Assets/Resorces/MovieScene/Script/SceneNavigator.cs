using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SceneNavigator : SingletonMonobehaviour<SceneNavigator>
{
    // 一個前のシーン名
    private string beforeSceneName = "";
    public string BeforeSceneName
    {
        get
        {
            return beforeSceneName;
        }
    }

    // 現在のシーン名
    private string currentSceneName = "";
    public string CurrentSceneName
    {
        get
        {
            return currentSceneName;
        }
    }

    // 次のシーン名
    private string nextSceneName = "";
    public string NextSceneName
    {
        get
        {
            return nextSceneName;
        }
    }

    // 初期化
    protected  override void Init()
    {
        base.Init();

        // 最初のシーン名を設定
        currentSceneName = SceneManager.GetSceneAt(0).name;
    }

    // シーンの変更
    public void Change(string sceneName)
    {
        Fade.FadeOut(1);

        // 次のシーン名を設定
        nextSceneName = sceneName;

        // シーン読み込み、変更
        SceneManager.LoadScene(nextSceneName);

        // シーン名更新
        beforeSceneName = currentSceneName;
        currentSceneName = nextSceneName;

    }
}
