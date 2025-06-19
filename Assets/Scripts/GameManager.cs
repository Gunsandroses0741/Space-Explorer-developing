using SavingAndLoading;
using UnityEngine;

// 游戏管理器，负责管理全局游戏状态和存档加载
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // 玩家角色的Transform

    public static GameManager Instance; // 单例实例

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // 获取玩家Transform
    public Transform GetPlayerTransform() => playerTransform;

    // 保存任意对象到本地
    public void SaveThis<T>(T thing, string saveName) => SavingLoadingData.Save(thing, saveName);

    // 从本地加载对象
    public T LoadThis<T>(string saveName) => SavingLoadingData.Load<T>(saveName);
}