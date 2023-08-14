using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
   public static Scene targetScene;

    public static void Load(Scene targetetScene) {
        Loader.targetScene = targetetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

      public static void LoaderCallback(){
        SceneManager.LoadScene(targetScene.ToString());
    }

    public enum Scene {
        MainMenuScene,
            GameScene,
        LoadingScene
    }

}
