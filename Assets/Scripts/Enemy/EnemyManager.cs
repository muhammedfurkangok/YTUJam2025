using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
    {
        public GameObject[] enemyPrefabs;
        public GameObject DoctorSyringePrefab;
        public Transform doctorSyringeSpawnPoint;
        public Transform[] spawnPoints;
        public int maxEnemies = 20;

        public bool canSpawn = true;

        private int currentEnemyCount = 0;
        private List<GameObject> enemies = new();

        public bool doctorSyringe = false;


        private void OnEnable()
        {
            PlayerCardEffectsController.DoctorFinal += DoctorFinalInitialize;
            PlayerCardEffectsController.RestartAllStats += RestartAllStats;
        }

        void Start()
        {
            BurstSpawner();
        }

        private void RestartAllStats()
        {
            doctorSyringe = false;
        }

        private void DoctorFinalInitialize()
        {
            doctorSyringe = true;
            ClearEnemies();
            DoctorSyringe();
        }

        private void BurstSpawner()
        {
            if (doctorSyringe)
            {
                DoctorSyringe();
            }

            BurstSpawn();
            InvokeRepeating(nameof(SpawnEnemy), 5f, 5f); // Her 5 saniyede bir düşman spawnla
        }

        private void DoctorSyringe()
        {
            Instantiate(DoctorSyringePrefab, doctorSyringeSpawnPoint.position, Quaternion.identity);
            UIManager.Instance.ShowDoctorSyringeUI();
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
            foreach (var point in spawnPoints)
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
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        private void OnDisable()
        {
            PlayerCardEffectsController.DoctorFinal -= DoctorFinalInitialize;
            PlayerCardEffectsController.RestartAllStats -= RestartAllStats;
        }
    }
}