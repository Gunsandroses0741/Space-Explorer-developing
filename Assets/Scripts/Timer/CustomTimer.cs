namespace Timer
{
    // 自定义计时器基类
    public abstract class CustomTimer
    {
        protected float Duration { get; } // 计时总时长
        protected float _remainingTime; // 剩余时间

        protected CustomTimer(float duration)
        {
            Duration = duration;
            _remainingTime = duration;
        }

        // 重置计时器
        public void ResetTimer() => _remainingTime = Duration;

        // 每帧计时逻辑（需子类实现）
        public abstract void Tick(float deltaTime);
    }
}