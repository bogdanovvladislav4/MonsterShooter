using System;
using System.Collections.Generic;
using GameScripts;
using UnityEngine;

namespace UIScripts
{
    public class MainMenuScripts : MonoBehaviour
    {
        [SerializeField] private Game game;
        [SerializeField] private PlayerController playerController;


        private void Awake()
        {
            game.EnableMuteAllSounds();
            playerController.pauseGame = true;
        }

        public void ClickPlayFromMainMenu()
        {
            game.UseMainMenuPlayButton();
            playerController.pauseGame = false;
            game.DisableMuteAllSounds();
        }
        
        public void ClickStatistics()
        {
            game.UseStatisticsButton();
        }
        
        public void ClickTitres()
        {
            game.UseTitresButton();
        }
        
        public void ClickExit()
        {
            game.WriteSessionData(0);
            Application.Quit();
        }
    }
}