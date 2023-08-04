using System;
using System.Collections.Generic;
using System.Linq;
using EnemyScripts;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScripts
{
    public class Game : MonoBehaviour
    {
        private const String DateGameSessionKey = "SessionNumbers";
        private const String EnemiesKilledNumberKey = "EnemiesKilledNumber";
        private const String TimeSpentInCurrentSessionKey = "TimeSpentInCurrentSession";
        private const String TotalNumberOfSessionsKey = "TotalNumberOfSessions";
        private const String StateGameKey = "isRestart";
    
        [Header("UI Panels")]
        [SerializeField] private GameObject playerDiedPanel;
        [SerializeField] private GameObject lossPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject inGamePanel;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject titresPanel;
        [SerializeField] private GameObject statisticsPanel;
        
        [Header("Booster effects")]
        [SerializeField] private GameObject freezingEffectStart;
        [SerializeField] private GameObject killAllEnemiesEffect;
        [SerializeField] private GameObject freezingSpawnEffect;
        
        [Header("Game behavior")]
        [SerializeField] private SpawnEnemy spawnEnemy;
        [SerializeField] private float maxEnemyCountForLosing;
        [SerializeField] private Player player;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
        [Range(0,1)]
        [SerializeField] private int reloadLevelState;
        [SerializeField] private bool resetReloadLevelState;
        [SerializeField] private bool clearStatisticsData;

        
        private List<GameObject> _enemyList = new List<GameObject>();
        private List<Enemy> _enemies = new List<Enemy>();

        private int _countEnemiesKilled;
        private int _totalNumberOfSessions;
        private DateTime _startGameSession;
        private DateTime _dateGameSession;

        private void Awake()
        {
            if (resetReloadLevelState)
            {
                WriteSessionData(reloadLevelState);
            }

            if (clearStatisticsData)
            {
                ClearAllStatisticsData();
            }
        }

        private void Start()
        {
            _totalNumberOfSessions = PlayerPrefs.GetInt(GetTotalNumberOfSessionsKey());
            _startGameSession = DateTime.Now;
            _dateGameSession = DateTime.Now;
            
            if (PlayerPrefs.GetInt(GetStateKey()) == 0)
            {
                Time.timeScale = 0;
                EnableMuteAllSounds();
                playerController.pauseGame = true;
            }
            else
            {
                inGamePanel.SetActive(true);
                mainMenuPanel.SetActive(false);
            }

            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            Debug.Log(_enemies.Count);

            _enemyList = _enemyList.Where(x => x != null).ToList();
            _enemies = _enemies.Where(x => x != null).ToList();
            CheckForLoss();
            Debug.Log(_totalNumberOfSessions + " TotalNumberOfSessions");
        }
        
        

        public void DisableMuteAllSounds()
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.mute = false;
            }
        }
        
        public void EnableMuteAllSounds()
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.mute = true;
            }
        }

        public void UsePauseButton()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            playerController.pauseGame = true;
            EnableMuteAllSounds();
        }

        public int GetNumberOfLeavingEnemies()
        {
            return _enemyList.Count;
        }
    
        public void UseTitresButton()
        {
            titresPanel.SetActive(true);
        }

        public void UseStatisticsButton()
        {
            statisticsPanel.SetActive(true);
        }
    
        public void UsePlayButton()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            playerController.pauseGame = false;
        }
    
        public void UseMainMenuPlayButton()
        {
            mainMenuPanel.SetActive(false);
            inGamePanel.SetActive(true);
            Time.timeScale = 1;
        }
    
        public void UseRestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            mainMenuPanel.SetActive(false);
            inGamePanel.SetActive(true);
            Time.timeScale = 1;
            playerController.pauseGame = false;
        }

        public void UseSpeedShotBooster()
        {
            player.SpeedShotBooster();
        }
    
        public void AddEnemyKilled()
        {
            _countEnemiesKilled++;
        }
        public void AddNewEnemy(GameObject enemy, Enemy e)
        {
            _enemyList.Add(enemy);
            _enemies.Add(e);
        }

        public void UseFreezingEffects()
        {
            foreach (var enemy in _enemies)
            {
                enemy.AddFreezingEffects();
            }
            GameObject freezingEff = Instantiate(freezingEffectStart);
            freezingEff.transform.position = Vector3.zero;
            Destroy(freezingEff, 3);
        }

        public void UseFreezingSpawnEffects()
        {
            spawnEnemy.AddFreezingSpawn();
            GameObject freezingSpawnEff = Instantiate(freezingSpawnEffect);
            freezingSpawnEff.transform.position = Vector3.zero;
            Destroy(freezingSpawnEff, 3);
        }

        public void UseKillAllEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.AddDamage(spawnEnemy.GetBaseHealthValue());
            }
            GameObject killAllEnemiesEff = Instantiate(killAllEnemiesEffect);
            killAllEnemiesEffect.transform.position = Vector3.zero;
            Destroy(killAllEnemiesEff, 3);
        }

        public int GetCountKilledEnemy()
        {
            return _countEnemiesKilled;
        }

        public void PlayerDied()
        {
            if (!playerDiedPanel.activeSelf)
            {
                playerDiedPanel.SetActive(true);
                Time.timeScale = 0;
                playerController.pauseGame = true;
                WriteSessionData(1);
                EnableMuteAllSounds();
            }
        }

        public String GetDateSessionKey()
        {
            return DateGameSessionKey;
        }

        private String GetStateKey()
        {
            return StateGameKey;
        }
    
        public String GetEnemiesKilledNumberKey()
        {
            return EnemiesKilledNumberKey;
        }
    
        public String GetTimeSpentInGameSessionKey()
        {
            return TimeSpentInCurrentSessionKey;
        }

    
        public String GetTotalNumberOfSessionsKey()
        {
            return TotalNumberOfSessionsKey;
        }

        private void CheckForLoss()
        {
            if (_enemyList.Count > maxEnemyCountForLosing)
            {
                lossPanel.SetActive(true);
                Time.timeScale = 0;
                WriteSessionData(1);
                EnableMuteAllSounds();
            }
        }

        public void WriteSessionData(int state)
        {
            if (_countEnemiesKilled > 0)
            {
                String spentTimeCurrentSession = (DateTime.Now - _startGameSession).ToString("T");
                _totalNumberOfSessions++;
                PlayerPrefs.SetInt(GetTotalNumberOfSessionsKey(), _totalNumberOfSessions);
                PlayerPrefs.SetInt(GetEnemiesKilledNumberKey()  + _totalNumberOfSessions, _countEnemiesKilled);
                PlayerPrefs.SetString(GetDateSessionKey()  + _totalNumberOfSessions, _dateGameSession.ToString("D"));
                PlayerPrefs.SetString(GetTimeSpentInGameSessionKey()  + _totalNumberOfSessions, spentTimeCurrentSession);
            }
            PlayerPrefs.SetInt(GetStateKey(), state);
            PlayerPrefs.Save();
        }

        private void ClearAllStatisticsData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}