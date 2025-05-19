using System;
using UnityEngine;

namespace Player
{
    public class PlayerCardEffectsController : SingletonMonoBehaviour<PlayerCardEffectsController>
    {
        public static event Action MoreBrain;
        public static event Action Berserk;
        public static event Action BoomHeadshot;
        public static event Action GrimReaper;
        public static event Action DoctorsSyringe;
        public static event Action DoctorFinal;

        public static event Action RestartAllStats;

        public void ExecuteCardEffect(CardData data)
        {
            Debug.Log("Applying effect: " + data.effectType);
            
            RestartAll();
            ApplyEffect(data.effectType);
        }

        public void RestartAll()
        {
            RestartAllStats?.Invoke();
        }

        private void ApplyEffect(CardEffectType effect)
        {
            switch (effect)
            {
                case CardEffectType.Default:
                    RestartAllStats?.Invoke();
                    break;
                case CardEffectType.MoreBrain:
                    MoreBrain?.Invoke(); // koşamıyor
                    break;
                case CardEffectType.Berserk:
                    Berserk?.Invoke(); //sadece el
                    break;
                case CardEffectType.BoomHeadshot:
                    BoomHeadshot?.Invoke();
                    break;
                case CardEffectType.GrimReaper:
                    GrimReaper?.Invoke();
                    break;
                case CardEffectType.DoctorsSyringe:
                    DoctorsSyringe?.Invoke();
                    break;
                case CardEffectType.DoctorFinal:
                    DoctorFinal?.Invoke();
                    break;

                default:
                    Debug.Log("Effect: None");
                    break;
            }
        }

        private void OnDisable()
        {
            MoreBrain = null;
            Berserk = null;
            BoomHeadshot = null;
            GrimReaper = null;
            DoctorsSyringe = null;
            DoctorFinal = null;
        }
    }
}