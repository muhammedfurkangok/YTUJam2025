using System;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatsManager.Instance.DecreaseHealth(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            return;
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}