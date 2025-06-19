using UnityEngine;

// 飞船输入处理类，负责处理玩家输入并转换为控制信号
public class ShipInputValueHandler : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float deadZoneRadius = 0.6f; // 死区半径
    [SerializeField] private float deadZoneCoef = 3; // 死区系数

    private Controls  _controls;
    private bool _isFreeLook = false; // 是否为自由视角

    private void Awake()
    {
        _controls = new Controls();
        _controls.SpaceShip.Free_Look.performed += _ => ToggleFreeLook();
    }

    private void OnEnable() => _controls.Enable(); // 启用输入

    private void OnDisable() => _controls.Disable(); // 禁用输入

    // 获取射击输入
    public bool GetShootInput() => _controls.SpaceShip.Shoot.ReadValue<float>() > .1f;

    // 获取瞄准输入
    public bool GetAimingInput() => _controls.SpaceShip.Aiming.ReadValue<float>() > .1f;

    // 切换自由视角
    private void ToggleFreeLook() => _isFreeLook = !_isFreeLook;

    // 获取鼠标输入并进行归一化处理
    public Vector2 GetMouseValue()
    {
        if (_isFreeLook)
            return Vector2.zero;
        Vector2 mouseVec = _controls.SpaceShip.Steering_mouse.ReadValue<Vector2>();
        int height = Screen.height / 2;
        mouseVec = new Vector2(mouseVec.x - (Screen.width / 2), mouseVec.y - height) / height;
        mouseVec /= deadZoneRadius;
        if (mouseVec.sqrMagnitude > deadZoneRadius * deadZoneRadius)
            mouseVec = mouseVec.normalized;
        mouseVec *= deadZoneCoef;
        return mouseVec;
    }

    // 获取滚转输入
    public float GetRollValue => _controls.SpaceShip.Roll.ReadValue<float>();
    // 获取前后移动输入
    public float GetMoveValue => _controls.SpaceShip.Move_Forward_Backward.ReadValue<float>();
}