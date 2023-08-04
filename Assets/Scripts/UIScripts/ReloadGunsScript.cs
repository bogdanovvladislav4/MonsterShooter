using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class ReloadGunsScript : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Slider reloadGuns;

        private void Start()
        {
            reloadGuns.maxValue = player.GetDelayBetweenShot();
        }

        private void Update()
        {
            reloadGuns.value = player.GetCurrentTimeReloadGuns();
        }
    }
}
