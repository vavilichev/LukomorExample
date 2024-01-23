using System;

namespace Lukomor.Example.Pong
{
    public class ScreenPauseViewModel : ScreenViewModel
    {
        private readonly GameSessionService _gameSessionsService;
        private readonly ScenesService _scenesService;
        private readonly Action _openGameplayScreen;

        public ScreenPauseViewModel(GameSessionService gameSessionsService, ScenesService scenesService, Action openGameplayScreen)
        {
            _gameSessionsService = gameSessionsService;
            _scenesService = scenesService;
            _openGameplayScreen = openGameplayScreen;
        }

        public void HandleResumeButtonClick()
        {
            _gameSessionsService.Unpause();
            _openGameplayScreen();
        }

        public void HandleMainMenuButtonClick()
        {
            _scenesService.LoadMainMenuScene();
        }
    }
}