using UnityEngine;

namespace UIScripts
{
    public class BackButtonScript : MonoBehaviour
    {
        [SerializeField] private GameObject previousPanel;
        [SerializeField] private GameObject currentPanel;

        public void ClickBackButton()
        {
            previousPanel.SetActive(true);
            currentPanel.SetActive(false);
        }
    }
}