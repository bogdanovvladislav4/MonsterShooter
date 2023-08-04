using GameScripts;
using UnityEngine;

namespace UIScripts
{
    public class PlayButtonScript : MonoBehaviour
    {
        [SerializeField] private Game game;

        public void ClickPlay()
        {
            game.UsePlayButton();
        }
        
        
    }
}
