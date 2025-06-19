namespace Controller
{
    // 物理移动处理类
    public class PhysicalMovement
    {
        private IPhysicalMovable _movable; // 可移动对象接口

        // 构造函数，传入可移动对象
        public PhysicalMovement(IPhysicalMovable movable) => _movable = movable;

        // 执行移动逻辑
        public void Move() => _movable.Rigidbody.AddForce(_movable.Direction * _movable.Speed);
    }
}