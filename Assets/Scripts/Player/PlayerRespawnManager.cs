using UnityEngine;

namespace Player
{
    public class PlayerRespawnManager : MonoBehaviour
    {

        public void OnDie()
        {
            //die anim
            //DeathCameraCutscene.DieAnim
        }
        
        public void GetVaccinated()
        {
            //play animation
            //CutSceneController.CutSceneStart
        }
        public void OnRespawn()
        {
            GetVaccinated();
            //card;
            //card equal thing happened.
        }
    }
}
