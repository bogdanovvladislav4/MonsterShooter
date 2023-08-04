using GameScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class KillAllEnemiesBoosterScript : MonoBehaviour
    {
        [SerializeField] private Game game;
        [SerializeField] private Slider cooldownBoosterBar;
        [SerializeField] private Button useBoosterButton;
        [SerializeField] private float cooldownBooster;

        private float _lastTimeUseBooster;
        public void CLickKillAllEnemiesBooster()
        {
            game.UseKillAllEnemies();
            _lastTimeUseBooster = Time.time;
        }
        
        private void Start()
        {
            cooldownBoosterBar.maxValue = cooldownBooster;
            cooldownBoosterBar.value = 0;
        }

        private void Update()
        {
            cooldownBoosterBar.value = Time.time - _lastTimeUseBooster;
            if (Time.time - _lastTimeUseBooster >= cooldownBooster)
            {
                useBoosterButton.gameObject.SetActive(true);
                cooldownBoosterBar.gameObject.SetActive(false);
            }
            else
            {
                useBoosterButton.gameObject.SetActive(false);
                cooldownBoosterBar.gameObject.SetActive(true);
            }
        }
    }
}
