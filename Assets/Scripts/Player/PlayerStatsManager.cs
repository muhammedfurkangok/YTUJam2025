using System;
using System.Collections;
using Player;
using UnityEngine;
using Utilities;

public class PlayerStatsManager : SingletonMonoBehaviour<PlayerStatsManager>
{
    [Header("Health Settings")]
    public int health = 100;
    public int maxHealth = 100;

    [Header("Stamina Settings")]
    public float stamina = 100f;
    public float maxStamina = 100f;

    private float regenDelay = 1f;
    private float regenTimer = 0f;
    private Coroutine reduceStaminaCoroutine;
    private bool isUsingStamina = false;
    private bool isRegenerating = false;

    public event Action OnDeath;


    private void Start()
    {
        PlayerCardEffectsController.MoreBrain += MoreBrainStats;
        PlayerCardEffectsController.RestartAllStats += RestartAllStats;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0f)
        {
            if (!isUsingStamina)
            {
                isUsingStamina = true;
                if (reduceStaminaCoroutine == null)
                    reduceStaminaCoroutine = StartCoroutine(ReduceStaminaOverTime());
            }

            regenTimer = 0f;
            isRegenerating = false;
        }
        else
        {
            if (isUsingStamina)
            {
                isUsingStamina = false;
                regenTimer = 0f;
                isRegenerating = false;

                if (reduceStaminaCoroutine != null)
                {
                    StopCoroutine(reduceStaminaCoroutine);
                    reduceStaminaCoroutine = null;
                }

                AudioManager.Instance.PlayOneShotSound(SoundType.TiredBreath);
            }

            if (!isRegenerating && stamina < maxStamina)
            {
                regenTimer += Time.deltaTime;

                if (regenTimer >= regenDelay)
                {
                    isRegenerating = true;
                }
            }

            if (isRegenerating)
            {
                RegenerateStamina();
            }
        }
    }

    private IEnumerator ReduceStaminaOverTime()
    {
        while (stamina > 0f)
        {
            stamina = Mathf.Max(0f, stamina - 1f);
            UIManager.Instance.UpdateStaminaBar(stamina / maxStamina);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void RegenerateStamina()
    {
        stamina = Mathf.Min(stamina + 20f * Time.deltaTime, maxStamina);
        UIManager.Instance.UpdateStaminaBar(stamina / maxStamina);
    }

    public void IncreaseHealth(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        UIManager.Instance.UpdateHealthBar((float)health / maxHealth);
    }

    public void DecreaseHealth(int amount)
    {
        health = Mathf.Max(health - amount, 0);
        UIManager.Instance.UpdateHealthBar((float)health / maxHealth);
        if (health == 0)
        {
            OnDeath?.Invoke();
            Debug.Log("Player is dead");
        }
    }

    public void MoreBrainStats()
    {
        maxHealth = 150;
        health = 150;
    }

    public void RestartAllStats()
    {
        health = 100;
        maxHealth = 100;
        stamina = 100f;
        maxStamina = 100f;
    }

    private void OnDisable()
    {
        PlayerCardEffectsController.MoreBrain -= MoreBrainStats;
        PlayerCardEffectsController.RestartAllStats -= RestartAllStats;
    }
}
