using UnityEngine;

namespace Player
{
    public class PlayerRespawnManager : MonoBehaviour
    {

        public void OnDie()
        {
            //die anim
        }
        
        public void GetVaccinated()
        {
            //play animation
        }
        public void OnRespawn()
        {
            GetVaccinated();
            //card;
            //card equal thing happened.
        }
    }
}