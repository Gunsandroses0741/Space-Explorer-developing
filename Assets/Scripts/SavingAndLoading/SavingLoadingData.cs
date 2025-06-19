using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SavingAndLoading
{
    // 存档与加载数据管理类
    public class SavingLoadingData : MonoBehaviour
    {
        // 保存对象到本地
        public static void Save<T>(T saveThisThing, string saveName)
        {
            // 定义存档路径，使用Unity的持久化数据路径
            string path = Application.persistentDataPath + "/Uncertainty/saves/";
            // 检查存档目录是否存在，如果不存在则创建
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // 创建二进制格式化器实例
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // 使用using语句确保文件流正确关闭
            using (FileStream fileStream = new FileStream(path + saveName + ".dat", FileMode.Create))
            {
                // 将对象序列化并写入文件
                binaryFormatter.Serialize(fileStream, saveThisThing);
            }
        }

        // 加载本地对象
        public static T Load<T>(string saveName)
        {
            // 声明泛型变量用于存储加载的数据
            T getThisThing;
            // 获取存档文件路径
            string path = Application.persistentDataPath + "/Uncertainty/saves/";
            // 创建二进制格式化器实例
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // 使用using语句打开并读取文件
            using (FileStream fileStream = new FileStream(path + saveName + ".dat", FileMode.Open))
            {
                // 从文件中反序列化数据并转换为指定类型
                getThisThing = (T) binaryFormatter.Deserialize(fileStream);
            }

            // 返回加载的数据
            return getThisThing;
        }

        // 判断存档是否存在
        public static bool SaveExist(string saveName)
        {
            // 构建完整的存档文件路径
            string saveFile = Application.persistentDataPath + "/Uncertainty/saves/" + saveName + ".dat";
            // 返回文件是否存在的结果
            return File.Exists(saveFile);
        }

        // 删除存档
        public static void DeleteSave(string saveName)
        {
            // 构建要删除的存档文件路径
            string saveFile = Application.persistentDataPath + "/Uncertainty/saves/" + saveName + ".dat";
            // 删除文件
            File.Delete(saveFile);
        }
    }
}