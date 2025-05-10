using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject[] enemyPrefabs;
        public Transform[] spawnPoints;
        public int maxEnemies = 10;

        private int currentEnemyCount = 0;

        void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 2f, 5f); // Her 5 saniyede bir düşman spawnla
        }

        void SpawnEnemy()
        {
            if (currentEnemyCount >= maxEnemies) return;

            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomSpawn = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawn].position, Quaternion.identity);
            currentEnemyCount++;

            Debug.Log("Yeni düşman spawnlandı!");
        }
    }
}