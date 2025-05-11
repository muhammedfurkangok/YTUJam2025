using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using Runtime.System.InputSystem;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : SingletonMonoBehaviour<MovementController>
    {
        public float movementSpeed = 5f;
        public float runSpeedMultiplier = 2f;
        public float jumpSpeed = 8f;
        public float gravity = 9.8f;
        [SerializeField, Child] private CinemachineCamera virtualCamera;
        public Animator characterAnimator;

        public GameObject speedEffect;

        private CharacterController controller;
        private Vector3 moveDirection;

        private float verticalVelocity;

        private bool isRunning;
        private bool isBoosting;
        private bool finishedBoost;
        private Coroutine boostCoroutine;
        private bool isWalkingSoundPlaying;
        public bool isBoostingActive = true;

        public bool isGreamReaperActive = false;
        private Coroutine healthDecreaseCoroutine;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            PlayerCardEffectsController.MoreBrain += DisableRun;
            PlayerCardEffectsController.GrimReaper += RunningCausesDamage;
            PlayerCardEffectsController.RestartAllStats += RestartStats;
        }

        void Update()
        {
            MoveCharacter();
            ApplyGravity();
        }

        void MoveCharacter()
        {
            if (InputManager.Instance.blockMovementInput) return;
            
            Vector2 input = InputManager.Instance.GetMovementInput();
            Vector3 forward = virtualCamera.transform.forward;
            Vector3 right = virtualCamera.transform.right;
            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            moveDirection = forward * input.y + right * input.x;
            moveDirection *= movementSpeed;

            if (input.magnitude > 0)
            {
                characterAnimator.SetBool("IsWalking", true);
            }
            else
            {
                characterAnimator.SetBool("IsWalking", false);
            }


            if (InputManager.Instance.IsRunning() && PlayerStatsManager.Instance.stamina > 0 && isBoostingActive)
            {
                moveDirection *= runSpeedMultiplier;
                isRunning = true;
                speedEffect.SetActive(true);
                characterAnimator.SetBool("IsRunning", true);

                if (isGreamReaperActive)
                {
                    if (healthDecreaseCoroutine == null)
                    {
                        healthDecreaseCoroutine = StartCoroutine(DecreaseHealthOverTime());
                    }
                }
            }
            else
            {
                isRunning = false;
                speedEffect.SetActive(false);
                characterAnimator.SetBool("IsRunning", false);

                if (healthDecreaseCoroutine != null)
                {
                    StopCoroutine(healthDecreaseCoroutine);
                    healthDecreaseCoroutine = null;
                }
            }

            

            if (InputManager.Instance.IsJumping() && controller.isGrounded)
            {
                verticalVelocity = jumpSpeed;
            }

            controller.Move(moveDirection * Time.deltaTime);
        }

        void ApplyGravity()
        {
            if (!controller.isGrounded)
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
            else if (verticalVelocity < 0)
            {
                verticalVelocity = -1f;
            }

            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        }

        public void PlayWalkSound()
        {
            AudioManager.Instance.PlayOneShotSound(SoundType.Walk); 
        }

        public void DisableRun()
        {
            Debug.Log("Disable run");
            isRunning = false;
            speedEffect.SetActive(false);
            characterAnimator.SetBool("IsRunning", false);
        }
        public void RestartStats()
        {
            isBoostingActive = true;
            isGreamReaperActive = false;
        }
        
        private IEnumerator DecreaseHealthOverTime()
        {
            while (isGreamReaperActive)
            {
                PlayerStatsManager.Instance.DecreaseHealth(1); // Sağlık daha yavaş azalır
                yield return new WaitForSeconds(1f); // Her 1 saniyede bir azalma
            }
        }
        
        public void RunningCausesDamage()
        {
            isGreamReaperActive = true;
          
        }

        private void OnDisable()
        {
            PlayerCardEffectsController.MoreBrain -= DisableRun;
            PlayerCardEffectsController.GrimReaper -= RunningCausesDamage;
            PlayerCardEffectsController.RestartAllStats -= RestartStats;
        }

        public void KillYourselfAnim()
        {
           characterAnimator.SetTrigger("ShootYourself");
        }
    }
}