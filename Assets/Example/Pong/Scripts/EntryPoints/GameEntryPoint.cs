using Lukomor.DI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lukomor.Example.Pong
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        private DIContainer _rootContainer;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void StartGame()
        {
            _instance = new GameEntryPoint();
            _instance.Init();
        }

        private void Init()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            _rootContainer = new DIContainer();
            
            var scenesService = _rootContainer
                .RegisterSingleton(_ => new ScenesService())
                .CreateInstance();
            var sceneName = scenesService.GetActiveSceneName();

            if (sceneName == ScenesService.SCENE_GAMEPLAY)
            {
                StartGameplay(PongGameplayMode.OnePlayer);
                return;
            }

            if (sceneName == ScenesService.SCENE_MAIN_MENU)
            {
                StartMainMenu();
                return;
            }

            if (sceneName != ScenesService.SCENE_BOOT)
            {
                return; // If scene isn't from the example project - do nothing.
            }
            
            scenesService.LoadMainMenuScene();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var sceneName = scene.name;
            
            if (sceneName == ScenesService.SCENE_MAIN_MENU)
            {
                StartMainMenu();
                return;
            }

            if (sceneName == ScenesService.SCENE_GAMEPLAY)
            {
                var gameplayMode = _rootContainer.Resolve<ScenesService>().CachedGameplayMode;
                StartGameplay(gameplayMode);
            }
        }

        private void StartMainMenu()
        {
            var entryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
            var mainMenuContainer = new DIContainer(_rootContainer);
            
            entryPoint.Process(mainMenuContainer);
        }

        private void StartGameplay(PongGameplayMode mode)
        {
            var entryPoint = Object.FindObjectOfType<GameplayEntryPoint>();
            var gameplayContainer = new DIContainer(_rootContainer);
            
            entryPoint.Process(gameplayContainer, mode);
        }
    }
}