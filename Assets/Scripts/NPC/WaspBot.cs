using System.Collections.Generic;
using NPC.Enemy_AI;
using UnityEngine;

namespace NPC
{
    /// <summary>
    /// 黄蜂机器人敌人，负责路径、速度设置与回收
    /// </summary>
    [RequireComponent(typeof(EnemyMoveDataContainer))]
    public class WaspBot : MonoBehaviour
    {
        private EnemyMoveDataContainer _container;

        private void OnEnable()
        {
            if (_container == null)
                _container = GetComponent<EnemyMoveDataContainer>();
        }

        /// <summary>
        /// 设置路径点
        /// </summary>
        public void SetPathPoints(List<Transform> points) => _container.pathPoints = points;

        /// <summary>
        /// 设置移动速度
        /// </summary>
        public void SetSpeed(float speed) => _container.speed = speed;

        /// <summary>
        /// 回收到对象池
        /// </summary>
        public void ReturnToPool() => WaspBotPool.Instance.ReturnToPool(this);

        /// <summary>
        /// 减少存活敌人数（可扩展）
        /// </summary>
        public void ReduceNumberOfAliveBots()
        {
        }
    }
}