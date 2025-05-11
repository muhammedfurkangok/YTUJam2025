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
            //CutSceneCameraController.Instance.DisableObjects();
            DeathCameraCutscene.Instance.DieAnim();
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
