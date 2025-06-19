using UnityEngine;
using Weapons.Projectile;

namespace Weapons.Weapon
{
    // 爆能枪武器，实现射击逻辑
    class Blaster : WeaponBase
    {
        private int _ownerID; // 拥有者ID

        private void Start()
        {
            var theCollider = GetComponentInParent<Collider>();
            if (theCollider != null)
            {
                _ownerID = theCollider.GetInstanceID();
            }
        }

        // 执行射击
        public override void Shoot()
        {
            _timer.Tick(Time.deltaTime);
            if (_timer.IsReady())
            {
                var projectile = NormalBulletPool.Instance.Get();
                SetProjectileSettings(projectile);
            }
        }

        // 设置子弹参数
        private void SetProjectileSettings(NormalBullet projectile)
        {
            projectile.SetOwner(_ownerID);
            var tempTransform = projectile.transform;
            tempTransform.position = shotPoint.position;
            tempTransform.rotation = shotPoint.rotation;
            projectile.SetDamage(damage);
            projectile.gameObject.SetActive(true);
        }
    }
}