using System.Collections.Generic;
using System.Linq;
using NPC.Enemy_AI.StateMachine.Conditions;

namespace Utils
{
    // 条件扩展方法类
    public static class ConditionExtension
    {
        // 判断所有条件是否都满足
        public static bool AreMet(this List<Condition> conditions) => conditions.All(item => item.IsMet);
    }
}