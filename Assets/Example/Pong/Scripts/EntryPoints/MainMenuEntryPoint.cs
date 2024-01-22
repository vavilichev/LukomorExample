﻿using System;
using Lukomor.DI;
using Lukomor.MVVM;
using UnityEngine;

namespace Lukomor.Example.Pong
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private View _uiRootView;

        private void Awake()
        {
            Debug.Log("Init main menu entry point");
        }

        public void Process(DIContainer container)
        {
            RegisterViewModels(container);
            BindViewModels(container);
            OpenDefaultScreen(container);
        }

        private void RegisterViewModels(DIContainer container)
        {
            var scenesService = container.Resolve<ScenesService>();
            
            container.RegisterSingleton(_ => new ScreenMainMenuViewModel(scenesService.LoadGameplayScene));
            container.RegisterSingleton(c => new UIRootMainMenuViewModel(() => c.Resolve<ScreenMainMenuViewModel>()));
        }

        private void BindViewModels(DIContainer container)
        {
            var uiRootViewModel = container.Resolve<UIRootMainMenuViewModel>();

            _uiRootView.Bind(uiRootViewModel);
        }

        private void OpenDefaultScreen(DIContainer container)
        {
            var uiRootViewModel = container.Resolve<UIRootMainMenuViewModel>();
            
            uiRootViewModel.OpenMainMenu();
        }
    }
} 