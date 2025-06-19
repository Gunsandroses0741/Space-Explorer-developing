using System.Collections.Generic; // 引入泛型集合命名空间
using NPC.Enemy_AI.StateMachine.Conditions; // 引入条件相关命名空间
using UnityEngine; // 引入Unity引擎命名空间

namespace NPC.Enemy_AI // 命名空间定义
{
    [RequireComponent(typeof(EnemyMoveDataContainer), typeof(ReachDestination))] // 要求挂载EnemyMoveDataContainer和ReachDestination组件
    public class EnemyPatrol : EnemyMoveState // 敌人巡逻类，继承自EnemyMoveState
    {
        private float _rotationSmoothness; // 旋转平滑度
        private float _speed; // 移动速度
        private List<Transform> _pathPoints; // 巡逻路径点列表
        private ReachDestination _destinationCondition; // 到达目标点的条件组件
        private Transform _currentPoint; // 当前目标点

        private void OnEnable() // 脚本启用时调用
        {
            GetDependencies(); // 获取依赖数据
            _destinationCondition = GetComponent<ReachDestination>(); // 获取ReachDestination组件
            if (_pathPoints == null || _pathPoints.Count == 0) // 判断_pathPoints是否为空或数量为0
            {
                Debug.Log("NOOOOO _pathpoints!!!!"); // 控制台输出警告信息
                return; // 直接返回，避免后续空引用错误
            }

            //if (!_destinationCondition) return; // 旧的空判断，已注释

            GetRandomPoint(); // 随机选择一个巡逻点
            _destinationCondition.SetDestination(_currentPoint.position); // 设置目标点
        }

        private void GetRandomPoint() // 随机选择一个巡逻点
        {
            var pointCount = _pathPoints.Count; // 获取路径点数量
            var rand = Random.Range(0, pointCount); // 随机生成索引
            var selectedPoint = _pathPoints[rand]; // 获取随机点
            _currentPoint = selectedPoint; // 设置为当前目标点
        }

        protected override void Update() // 每帧调用
        {
            if (_pathPoints == null || _pathPoints.Count == 0) // 路径点未设置或为空
            {
                GetDependencies(); // 重新获取依赖
                return; // 直接返回
            }

            Patrol(); // 执行巡逻逻辑
        }

        private void Patrol() // 巡逻行为
        {
            var direction = _currentPoint.position - transform.position; // 计算当前位置到目标点的方向
            //vector.up is a little hard coded 
            if (
                direction.sqrMagnitude >
                1) //TODO: hardcode this is for when position is close to point it dosnt get throgh it and doesnt glitches
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up),
                    _rotationSmoothness); // 平滑旋转朝向目标点
                transform.position += direction.normalized * (_speed * Time.deltaTime); // 按速度移动到目标点
            }
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, _currentPoint.rotation, _rotationSmoothness); // 靠近目标点时平滑旋转到目标点的朝向
        }

        protected override void GetDependencies() // 获取依赖数据
        {
            var data = GetComponent<EnemyMoveDataContainer>(); // 获取数据容器组件
            if (!data) // 如果没有获取到
            {
                Debug.Log($"Add enemy move data container to : {gameObject.name} "); // 输出警告日志
                return; // 返回
            }

            _speed = data.speed; // 赋值速度
            _rotationSmoothness = data.rotationSmoothness; // 赋值旋转平滑度
            _pathPoints = data.pathPoints; // 赋值路径点列表
            if (_pathPoints.Count != 0) // 路径点不为空
                GetRandomPoint(); // 随机选择一个巡逻点
        }
    }
}
