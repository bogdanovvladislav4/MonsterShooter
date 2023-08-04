using PlayerScripts;
using UnityEngine;

namespace UIScripts
{
    public class AttackButtonScript : MonoBehaviour
    {
        [SerializeField] private Player player;

        public void ClickAttackButton()
        {
            player.pressedAttackButton = true;
        }
    }
}