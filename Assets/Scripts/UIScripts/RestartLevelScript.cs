using GameScripts;
using UnityEngine;

namespace UIScripts
{
    public class RestartLevelScript : MonoBehaviour
    {
        [SerializeField] private Game game;

        public void ClickRestartLevel()
        {
            game.UseRestartButton();
        }
    }
}
