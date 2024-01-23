﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lukomor.Example.Pong
{
    public class ScenesService
    {
        public const string SCENE_GAMEPLAY = "PongGameplay";
        public const string SCENE_MAIN_MENU = "PongMainMenu";
        public const string SCENE_BOOT = "PongBoot";

        public PongGameplayMode CachedGameplayMode { get; private set; }

        private AsyncOperation _cachedAsyncOperation;

        public string GetActiveSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
        
        public void LoadGameplayScene(PongGameplayMode mode)
        {
            CachedGameplayMode = mode;
            
            SceneManager.LoadScene(SCENE_GAMEPLAY);
        }

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(SCENE_MAIN_MENU);
        }
    }
}