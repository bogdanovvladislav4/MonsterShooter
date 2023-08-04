using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class HealthCounter : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Slider healthBar;


        private void Start()
        {
            healthBar.maxValue = player.GetHealth();
        }

        private void Update()
        {
            healthBar.value = player.GetHealth();
        }
    }
}
