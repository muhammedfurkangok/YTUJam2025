using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
    {
        public GameObject[] enemyPrefabs;
        public Transform[] spawnPoints;
        public int maxEnemies = 20;

        public bool canSpawn = true;

        private int currentEnemyCount = 0;
        private List<GameObject> enemies = new();

        void Start()
        {
            BurstSpawn();
            InvokeRepeating(nameof(SpawnEnemy), 5f, 5f); // Her 5 saniyede bir düşman spawnla
        }

        void SpawnEnemy()
        {
            if (!canSpawn) return;
            if (currentEnemyCount >= maxEnemies) return;

            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomSpawn = Random.Range(0, spawnPoints.Length);

            var enemy = Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawn].position, Quaternion.identity);
            enemies.Add(enemy);
            currentEnemyCount++;

            Debug.Log("Yeni düşman spawnlandı!");
        }

        public void BurstSpawn()
        {
            canSpawn = true;
            foreach(var point in spawnPoints)
            {
                int randomEnemy = Random.Range(0, enemyPrefabs.Length);
                var enemy = Instantiate(enemyPrefabs[randomEnemy], point.position, Quaternion.identity);
                enemies.Add(enemy);
                currentEnemyCount++;
            }
        }

        public void ClearEnemies()
        {
            canSpawn = false;
            foreach(var enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}