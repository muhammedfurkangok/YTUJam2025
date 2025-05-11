using UnityEngine;
using UnityEngine.SceneManagement;

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
        }
    }
}
