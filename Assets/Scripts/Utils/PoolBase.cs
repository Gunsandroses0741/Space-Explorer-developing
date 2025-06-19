using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    // 泛型对象池基类，用于对象复用
    public abstract class PoolBase<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T prefab; // 预制体
        public static PoolBase<T> Instance { get; private set; } // 单例
        protected Queue<T> _objects = new Queue<T>(); // 对象队列

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            AddToPool(5); // 初始化时预创建5个对象
        }

        // 回收对象到池
        public virtual void ReturnToPool(T returnObject)
        {
            if (returnObject == null)
            {
                Debug.LogWarning($"尝试返回空对象到 {typeof(T).Name} 池");
                return;
            }
            returnObject.gameObject.SetActive(false);
            _objects.Enqueue(returnObject);
        }

        // 获取对象
        public virtual T Get()
        {
            if (prefab == null)
            {
                Debug.LogError($"{typeof(T).Name} 池的预制体未设置！");
                return null;
            }
            if (_objects.Count == 0)
            {
                AddToPool(1);
            }
            if (_objects.Count > 0)
            {
                T obj = _objects.Dequeue();
                if (obj == null)
                {
                    Debug.LogWarning($"从 {typeof(T).Name} 池中获取到空对象，创建新对象替代");
                    obj = CreateNewObject();
                }
                return obj;
            }
            Debug.LogError($"无法从 {typeof(T).Name} 池中获取对象");
            return null;
        }

        // 向池中添加对象
        protected virtual void AddToPool(int count)
        {
            if (prefab == null)
            {
                Debug.LogError($"{typeof(T).Name} 池的预制体未设置！");
                return;
            }
            for (int i = 0; i < count; i++)
            {
                T newObject = CreateNewObject();
                if (newObject != null)
                {
                    newObject.gameObject.SetActive(false);
                    _objects.Enqueue(newObject);
                }
            }
        }

        // 创建新对象
        private T CreateNewObject()
        {
            try
            {
                T newObject = Instantiate(prefab);
                newObject.transform.SetParent(transform);
                return newObject;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"创建 {typeof(T).Name} 对象时发生错误: {e.Message}");
                return null;
            }
        }

        // 获取当前活跃对象数量（需子类实现）
        public virtual int GetNumberOfAlive()
        {
            return 0;
        }

        // 清理单例
        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        // 验证预制体设置
        private void OnValidate()
        {
            if (prefab == null)
            {
                Debug.LogWarning($"{typeof(T).Name} 池的预制体未设置！");
            }
        }
    }
}