using UnityEngine;
using Utils;

namespace NPC
{
    // 黄蜂机器人对象池，管理敌人生成与回收
    public class WaspBotPool : PoolBase<WaspBot>
    {
        private int _count = 0; // 总生成数
        private int _alive = 0; // 当前存活数
        private int _deathCount = 0; // 死亡计数

        // 获取当前存活敌人数
        public int GetNumberOfAlive() => _alive;

        // 回收敌人并统计击杀
        public override void ReturnToPool(WaspBot returnObject)
        {
            base.ReturnToPool(returnObject);
            _alive -= 1;
            _deathCount++;
            GameManager.Instance.SaveThis(_deathCount, "PlayerKillCount");
            int killScore = GameManager.Instance.LoadThis<int>("PlayerKillCount");
            Debug.Log(killScore);
        }

        // 获取敌人对象
        public override WaspBot Get()
        {
            if (_objects.Count < 1)
            {
                AddToPool(1);
            }
            return _objects.Dequeue();
        }

        // 向池中添加敌人对象
        protected override void AddToPool(int count)
        {
            base.AddToPool(count);
            _count += count;
            _alive += count;
        }
    }
}