using UnityEngine;

namespace Controller
{
    // 物理可旋转接口
    public interface IPhysicalRotatable
    {
        Rigidbody Rigidbody { get; } // 刚体属性
        Vector2 TorqueForceValue { get; } // 扭矩力值
        float RollForceValue { get; } // 横滚力值
    }
}