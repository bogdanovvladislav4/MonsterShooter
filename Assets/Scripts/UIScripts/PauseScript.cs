using GameScripts;
using UnityEngine;

namespace UIScripts
{
    public class PauseScript : MonoBehaviour
    {
        [SerializeField] private Game game;
    
        public void ClickPause()
        {
            game.UsePauseButton();
        }
    }
}
