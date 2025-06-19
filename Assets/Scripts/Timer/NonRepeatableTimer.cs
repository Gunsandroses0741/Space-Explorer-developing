using System;

namespace Timer
{
    // 一次性计时器，到时后触发事件
    public class NonRepeatableTimer : CustomTimer
    {
        public event Action OnTimerEnded; // 计时结束事件

        public NonRepeatableTimer(float duration) : base(duration)
        {
        }

        // 每帧计时逻辑
        public override void Tick(float deltaTime)
        {
            _remainingTime -= deltaTime;
            if (_remainingTime <= 0)
            {
                OnTimerEnded?.Invoke();
            }
        }
    }
}