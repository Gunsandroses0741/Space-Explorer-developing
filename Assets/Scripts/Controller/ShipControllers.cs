using UnityEngine;

namespace Controller
{
    // 需要ShipInputValueHandler组件
    [RequireComponent(typeof(ShipInputValueHandler))]
    // 飞船控制器，实现了物理移动和旋转接口
    public class ShipControllers : MonoBehaviour, IPhysicalMovable, IPhysicalRotatable
    {
        // 飞船模型Transform
        [SerializeField] private Transform shipModel; // 飞船模型

        [Header("Physic")]
        // 移动速度
        [SerializeField] private float moveSpeed = 1f; // 移动速度
        // 旋转速度
        [SerializeField] private float torqueSpeed = 1f; // 旋转速度
        // 瞄准时的旋转速度
        [SerializeField] private float aimingTorqueSpeed = 1f; // 瞄准时旋转速度
        // 横滚速度
        [SerializeField] private float rollSpeed = 1f; // 横滚速度

        [Header("Visual")]
        // 横滚视觉效果强度
        [Range(0, 50)] [SerializeField] private float rollFanciness = 1f; // 横滚视觉效果强度
        // 俯仰视觉效果强度
        [Range(0, 50)] [SerializeField] private float pitchFanciness = 1f; // 俯仰视觉效果强度
        // 视觉效果插值速度
        [SerializeField] private float fancinessSpeed = 1f; // 视觉插值速度

        // 刚体属性
        public Rigidbody Rigidbody => _rb; // 刚体引用
        // 当前鼠标输入对应的扭矩值
        public Vector2 TorqueForceValue => (_mouseValue * _currentTorqueSpeed); // 当前扭矩
        // 当前输入对应的横滚力
        public float RollForceValue => (_inputHandler.GetRollValue * rollSpeed); // 横滚力
        // 飞船前进方向
        public Vector3 Direction => transform.forward; // 前进方向
        // 当前速度
        public float Speed => (_inputHandler.GetMoveValue * moveSpeed); // 当前速度

        // 物理移动逻辑
        private PhysicalMovement _physicalMovement;
        // 物理旋转逻辑
        private PhysicalRotation _physicalRotation;
        // 刚体引用
        private Rigidbody _rb = null;
        // 输入处理器
        private ShipInputValueHandler _inputHandler;
        // 鼠标输入值
        private Vector2 _mouseValue = Vector2.zero;
        // 当前扭矩速度
        private float _currentTorqueSpeed;

        // 初始化
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<ShipInputValueHandler>();
            _physicalMovement = new PhysicalMovement(this);
            _physicalRotation = new PhysicalRotation(this);
        }

        // 固定帧更新，处理物理移动和旋转
        private void FixedUpdate()
        {
            _physicalMovement.Move(); // 物理移动
            _physicalRotation.Rotate(); // 物理旋转
        }

        // 每帧更新，处理输入和视觉效果
        private void Update()
        {
            _mouseValue = _inputHandler.GetMouseValue(); // 获取鼠标输入
            Aim(); // 判断是否瞄准
            VisualRotate(); // 视觉旋转效果
        }

        // 判断是否瞄准，切换扭矩速度
        private void Aim() => _currentTorqueSpeed = _inputHandler.GetAimingInput() ? aimingTorqueSpeed : torqueSpeed;

        // 视觉旋转效果
        private void VisualRotate()
        {
            // fancy part
            var targetRotation = Quaternion.Euler(_mouseValue.y * pitchFanciness, 180, _mouseValue.x * rollFanciness);
            shipModel.localRotation = Quaternion.Lerp(
                shipModel.localRotation,
                targetRotation,
                Time.deltaTime * fancinessSpeed
            );
        }
    }
}