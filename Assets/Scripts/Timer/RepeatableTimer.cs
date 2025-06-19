namespace Timer
{
    // 可重复使用的计时器，到时后可重置
    public class RepeatableTimer : CustomTimer
    {
        private bool _isReady = false; // 是否准备好

        public RepeatableTimer(float duration) : base(duration)
        {
        }

        // 每帧计时逻辑
        public override void Tick(float deltaTime)
        {
            if (_isReady) return;

            _remainingTime -= deltaTime;
            _isReady = _remainingTime <= 0 ? true : false;
        }

        // 检查计时器是否到时，并自动重置
        public bool IsReady()
        {
            if (_isReady)
            {
                //reset timer
                _isReady = false;
                _remainingTime = Duration;
                return true;
            }

            return false;
        }
    }
}