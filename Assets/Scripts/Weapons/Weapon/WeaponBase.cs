using Timer;
using UnityEngine;

namespace Weapons.Weapon
{
    // 武器基类，定义武器通用属性和接口
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] private float fireRate; // 射击频率
        [SerializeField] protected float damage; // 伤害值
        [SerializeField] protected Transform shotPoint; // 射击点

        protected RepeatableTimer _timer; // 射速计时器

        private void Awake() => _timer = new RepeatableTimer(fireRate);

        // 射击方法（需子类实现）
        public abstract void Shoot();
    }
}