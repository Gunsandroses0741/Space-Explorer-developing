using HealthSystem.Damage.DamageDealer;
using HealthSystem.Damage.DamageTaker;
using Timer;
using UnityEngine;

namespace Weapons.Projectile
{
    [RequireComponent(typeof(NormalDamage))]
    // 普通子弹类，实现碰撞、伤害和生命周期
    public class NormalBullet : MonoBehaviour, IProjectile
    {
        [SerializeField] private float speed = 1f; // 子弹速度
        [SerializeField] private float lifeTime = 2f; // 生命周期
        [SerializeField] private LayerMask whatIsCollidable; // 可碰撞层

        private int _owner; // 拥有者ID
        private NonRepeatableTimer _timer; // 生命周期计时器
        private DamageDealer _dealer; // 伤害处理
        private NormalDamage _normalDamage; // 伤害数据

        private void OnEnable() => _timer.ResetTimer();

        private void Awake()
        {
            _normalDamage = GetComponent<NormalDamage>();
            _timer = new NonRepeatableTimer(lifeTime);
            _timer.OnTimerEnded += DeSpawn;
        }

        private void Start() => _dealer = GetComponent<DamageDealer>();

        // 碰撞检测
        public void OnTriggerEnter(Collider other)
        {
            if (_owner == other.GetInstanceID())
                return;
            if ((whatIsCollidable.value & (1 << other.gameObject.layer)) <= 0)
                return;
            var target = other.GetComponents<ITakeDamage>();
            if (target.Length <= 0)
                return;
            foreach (var taker in target)
            {
                taker.TakeDamage(_dealer.DamageTypes);
            }
            DeSpawn();
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        }

        // 设置拥有者ID
        public void SetOwner(int ownerID) => _owner = ownerID;
        // 设置伤害值
        public void SetDamage(float damage) => _normalDamage.damageAmount = damage;
        // 回收到对象池
        private void DeSpawn() => NormalBulletPool.Instance.ReturnToPool(this);
    }
}