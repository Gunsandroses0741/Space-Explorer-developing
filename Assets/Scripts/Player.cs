using UnityEngine;

// 玩家控制类，管理玩家经验值和死亡逻辑
public class Player : MonoBehaviour
{
    public float xp; // 玩家经验值
    
    // 玩家死亡时调用，禁用玩家对象
    public void PlayerDeath() => gameObject.SetActive(false);
}