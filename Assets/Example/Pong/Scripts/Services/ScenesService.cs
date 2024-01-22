using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lukomor.Example.Pong
{
    public class ScenesService
    {
        public const string SCENE_GAMEPLAY = "PongGameplay";
        public const string SCENE_MAIN_MENU = "PongMainMenu";
        public const string SCENE_BOOT = "PongBoot";

        public event Action<string> SceneCompletelyLoaded;

        public PongGameplayMode CachedGameplayMode { get; private set; }

        private AsyncOperation _cachedAsyncOperation;

        public string GetActiveSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }
        
        public void LoadGameplayScene(PongGameplayMode mode)
        {
            if (_cachedAsyncOperation != null)
            {
                return;
            }
            
            CachedGameplayMode = mode;
            
            _cachedAsyncOperation = SceneManager.LoadSceneAsync(SCENE_GAMEPLAY);

            _cachedAsyncOperation.completed += _ =>
            {
                _cachedAsyncOperation = null;
                
                SceneCompletelyLoaded?.Invoke(SCENE_GAMEPLAY);
            };
        }

        public void LoadMainMenuScene()
        {
            if (_cachedAsyncOperation != null)
            {
                return;
            }
            
            _cachedAsyncOperation = SceneManager.LoadSceneAsync(SCENE_MAIN_MENU);

            _cachedAsyncOperation.completed += _ =>
            {
                _cachedAsyncOperation = null;
                
                SceneCompletelyLoaded?.Invoke(SCENE_MAIN_MENU);
            };
        }
    }
}