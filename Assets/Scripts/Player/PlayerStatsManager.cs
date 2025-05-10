using System;
using System.Collections;
using UnityEngine;
using Utilities;

public class PlayerStatsManager : SingletonMonoBehaviour<PlayerStatsManager>
{
    [Header("Health Settings")]
    public int health = 100;
    public int maxHealth = 100;

    [Header("Stamina Settings")]
    public float stamina = 100;
    public int maxStamina = 100;

    private float regenTimer = 0f;
    private Coroutine reduceStaminaCoroutine;
    private bool isStaminaUsed = false;

    public event Action OnDeath;

    public void ReduceStamina()
    {
        if (stamina > 0)
        {
            stamina = stamina - 0.1f;
            regenTimer = 0f;
            isStaminaUsed = true;
            UIManager.Instance.UpdateStaminaBar((float)stamina / maxStamina);
        }
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
        if(health == 0) OnDeath?.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (reduceStaminaCoroutine == null && stamina > 0)
            {
                reduceStaminaCoroutine = StartCoroutine(ReduceStaminaOverTime());
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            AudioManager.Instance.PlayOneShotSound(SoundType.TiredBreath);

            if (reduceStaminaCoroutine != null)
            {
                StopCoroutine(reduceStaminaCoroutine);
                reduceStaminaCoroutine = null;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            StaminaIncrease();
        }
    }

    private IEnumerator ReduceStaminaOverTime()
    {
        while (stamina > 0)
        {
            ReduceStamina();
            yield return new WaitForSeconds(10f);
        }
    }

    private void StaminaIncrease()
    {
        if (stamina < maxStamina)
        {
            float waitTime = (stamina == 0 || isStaminaUsed) ? 0.6f : 0.05f;
            regenTimer += Time.deltaTime;

            if (regenTimer >= waitTime)
            {
                stamina++;
                regenTimer = 0f;
                UIManager.Instance.UpdateStaminaBar((float)stamina / maxStamina);
                isStaminaUsed = false;
            }
        }
    }
}