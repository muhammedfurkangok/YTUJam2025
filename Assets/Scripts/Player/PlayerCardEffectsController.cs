using UnityEngine;

namespace Player
{
    public class PlayerCardEffectsController : SingletonMonoBehaviour<PlayerCardEffectsController>
    {
        public CameraController cameraController;

        public void ExecuteCardEffect(CardData data)
        {

            if (data.ifCutScene)
            {
                cameraController.FocusOnTarget(() =>
                {
                    ApplyEffect(data.effectType);
                    cameraController.ReturnToDefault();
                });
            }
            else
            {
                ApplyEffect(data.effectType);
            }
        }

        private void ApplyEffect(CardEffectType effect)
        {
            switch (effect)
            {
                case CardEffectType.SpawnUnit:
                    Debug.Log("Effect: Spawn Unit");
                    break;
                case CardEffectType.HealPlayer:
                    Debug.Log("Effect: Heal Player");
                    break;
                case CardEffectType.DamageEnemy:
                    Debug.Log("Effect: Damage Enemy");
                    break;
                case CardEffectType.ChangeWeather:
                    Debug.Log("Effect: Change Weather");
                    break;
                case CardEffectType.FocusOnBuilding:
                    Debug.Log("Effect: Focus On Building");
                    break;
                default:
                    Debug.Log("Effect: None");
                    break;
            }
        }
    }
}