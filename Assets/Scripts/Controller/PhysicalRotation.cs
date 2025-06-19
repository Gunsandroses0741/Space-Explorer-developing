using UnityEngine;

namespace Controller
{
    // 物理旋转处理类
    public class PhysicalRotation
    {
        private IPhysicalRotatable _rotatable; // 可旋转对象接口

        // 构造函数，传入可旋转对象
        public PhysicalRotation(IPhysicalRotatable rotatable)
        {
            _rotatable = rotatable;
        }

        // 执行旋转逻辑
        public void Rotate()
        {
            _rotatable.Rigidbody.AddRelativeTorque(
                -_rotatable.TorqueForceValue.y,
                _rotatable.TorqueForceValue.x,
                -_rotatable.RollForceValue,
                ForceMode.VelocityChange
            );
        }
    }
}