using UnityEngine;

namespace Controller
{
    // 物理可移动接口
    public interface IPhysicalMovable
    {
        Rigidbody Rigidbody { get; } // 刚体属性
        Vector3 Direction { get; } // 移动方向
        float Speed { get; } // 移动速度
    }
}