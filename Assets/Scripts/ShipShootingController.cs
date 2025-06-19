using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Weapon;

// 飞船射击控制器，负责瞄准与射击逻辑
[RequireComponent(typeof(ShipInputValueHandler))]
public class ShipShootingController : MonoBehaviour
{
    [SerializeField]
    private LayerMask whatIsAimAbleLayer; // 可瞄准的层

    [SerializeField] private Camera shipCamera; // 飞船摄像机

    [SerializeField]
    private WeaponBase weapon; // 当前武器

    [SerializeField]
    private Transform shootPoint; // 武器射击点

    [SerializeField] private float aimRadius = 100; // 瞄准半径
    [SerializeField] private float aimMinDistance = 5f; // 最小瞄准距离

    private ShipInputValueHandler _inputHandler;
    private Vector3 _screenSpaceAimPoint;

    private void Start() => _inputHandler = GetComponent<ShipInputValueHandler>();

    private void Update()
    {
        CalculateScreenSpaceAimPoint(); // 计算屏幕空间瞄准点
        var worldSpaceAimPoint = shipCamera.ScreenToWorldPoint(_screenSpaceAimPoint);
        GetRaycastHitPoint(ref worldSpaceAimPoint); // 获取射线命中点
        shootPoint.LookAt(worldSpaceAimPoint); // 让射击点朝向目标
        if (_inputHandler.GetShootInput())
        {
            weapon.Shoot(); // 执行射击
        }
    }

    // 获取射线命中点，修正瞄准位置
    private void GetRaycastHitPoint(ref Vector3 worldSpaceAimPoint)
    {
        Vector3 direction = worldSpaceAimPoint - shootPoint.position;
        if (Physics.Raycast(shipCamera.transform.position, direction, out var raycastHit, 50, whatIsAimAbleLayer))
            if (Vector3.Distance(raycastHit.point, shootPoint.position) > aimMinDistance)
                worldSpaceAimPoint = raycastHit.point;
    }

    // 计算屏幕空间的瞄准点
    private void CalculateScreenSpaceAimPoint()
    {
        Vector3 mousePos = Mouse.current.position.value;
        Vector2 difference = mousePos - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector2 result = difference;
        float differenceLenght = difference.magnitude;
        if (differenceLenght > aimRadius)
            result = difference * (aimRadius / differenceLenght);
        _screenSpaceAimPoint = new Vector3(Screen.width / 2 + result.x, Screen.height / 2 + result.y, 50);
    }
}