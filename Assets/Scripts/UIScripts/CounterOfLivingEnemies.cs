using System;
using GameScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class CounterOfLivingEnemies : MonoBehaviour
    {
        [SerializeField] private Game game;
        [SerializeField] private Text counter;

        private void Update()
        {
            counter.text = game.GetNumberOfLeavingEnemies().ToString();
        }
    }
}
