using UnityEngine;

namespace Player
{
    public class PlayerRespawnManager : MonoBehaviour
    {
        private void Awake()
        {
            PlayerStatsManager.Instance.OnDeath += OnDie;
        }
        public void OnDie()
        {
            DeathCameraCutscene.Instance.DieAnim();
        }
        
        public void GetVaccinated()
        {
            //play animation
            CutSceneController.Instance.CutSceneStart();
        }
        public void OnRespawn()
        {
            GetVaccinated();
            //card;
            //card equal thing happened.
        }
    }
}
