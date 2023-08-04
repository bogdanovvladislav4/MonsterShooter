using System;
using GameScripts;
using UnityEngine;

namespace UIScripts
{
    public class StatisticsScript : MonoBehaviour
    {
        [SerializeField] private GameObject statisticRecordPrefab;
        [SerializeField] private GameObject content;
        [SerializeField] private Game game;
        
        private void Start()
        {
            for (int i = 1; i <= PlayerPrefs.GetInt(game.GetTotalNumberOfSessionsKey()); i++)
            {
                GameObject record = Instantiate(statisticRecordPrefab, content.transform);
                StatisticRecord statisticRecord = record.GetComponent<StatisticRecord>();
                statisticRecord.SetDataForRecord(PlayerPrefs.GetString(game.GetDateSessionKey() + i), 
                    PlayerPrefs.GetInt(game.GetEnemiesKilledNumberKey() + i),
                    PlayerPrefs.GetString(game.GetTimeSpentInGameSessionKey() + i));
            }
        }
    }
}