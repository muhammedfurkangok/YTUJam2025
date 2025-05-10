using Unity.Cinemachine;
                    using UnityEngine;
                    using Utilities;
                    using Weapon;
                    
                    public class Shotgun : WeaponBase
                    {
                        [Header("Shotgun Settings")]
                        public int pelletCount = 8; // Number of bullets fired at once
                        public float spreadAngle = 10f; // Spread angle in degrees
                    
                        public override void Fire()
                        {
                            if (isReloading || currentAmmo <= 0 || Time.time < lastFireTime + data.fireRate)
                                return;
                    
                            if (!data.isNoNeedAmmo)
                                currentAmmo--;
                    
                            lastFireTime = Time.time;
                    
                            ShowMuzzleFlash();
                            PlayFireEffects();
                    
                            for (int i = 0; i < pelletCount; i++)
                            {
                                // Randomize a direction within the cone
                                float randomYaw = Random.Range(-spreadAngle, spreadAngle);
                                float randomPitch = Random.Range(-spreadAngle, spreadAngle);
                                Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);
                    
                                // Calculate the bullet direction
                                Vector3 bulletDirection = spreadRotation * firePoint.forward;
                    
                                // Instantiate the bullet
                                Bullet bullet = Instantiate(data.bulletPrefab, firePoint.position, Quaternion.LookRotation(bulletDirection));
                                bullet.Initialize(bulletDirection);
                            }
                    
                            UIManager.Instance.UpdateAmmoText(currentAmmo, data.maxAmmo, data.isNoNeedAmmo);
                    
                            if (currentAmmo == 0)
                                Reload();
                        }
                    
                        protected override void PlayFireEffects()
                        {
                            base.PlayFireEffects();
                            WeaponManager.Instance.ShakeCamera();
                        }
                    }