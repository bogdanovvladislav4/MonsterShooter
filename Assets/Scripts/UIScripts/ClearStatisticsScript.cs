using UnityEngine;

namespace UIScripts
{
    public class ClearStatisticsScript : MonoBehaviour
    {
        [SerializeField] private GameObject statisticsPanel;
        public void ClickClearStatisticsButton()
        {
            PlayerPrefs.DeleteAll();
            statisticsPanel.SetActive(false);
            statisticsPanel.SetActive(true);
        }
    }
}