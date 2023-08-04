using GameScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class EnemiesCounterScript : MonoBehaviour
    {
        [SerializeField] private Game game;
        [SerializeField] private Text counter;
    
        void Update()
        {
            counter.text = game.GetCountKilledEnemy().ToString();
            Debug.Log(game.GetCountKilledEnemy());
        }
    }
}
