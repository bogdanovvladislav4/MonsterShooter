using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class StatisticRecord : MonoBehaviour
    {
        [SerializeField] private Text numberOfSession;
        [SerializeField] private Text enemiesKilledNumber;
        [SerializeField] private Text spentTimeInSession;

        public void SetDataForRecord(string numOfSes, int enemiesKilledNum, string timeSpentInSes)
        {
            numberOfSession.text = numOfSes;
            enemiesKilledNumber.text = enemiesKilledNum.ToString();
            spentTimeInSession.text = timeSpentInSes;
        }
        
    }
}