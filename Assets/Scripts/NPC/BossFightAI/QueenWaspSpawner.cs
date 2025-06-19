using System.Collections.Generic;
using Timer;
using UnityEngine;

namespace NPC.BossFightAI
{
    public class QueenWaspSpawner : MonoBehaviour
    {
        [SerializeField] private int difficulty = 1;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private int maxAliveBots = 3;
        [SerializeField] private float spawnRate = 10f;
        [SerializeField] private List<Transform> pathPoints;

        private WaspBotPool _pool;
        private RepeatableTimer _timer;
        private int _currentBotCount = 0;

        private void Start()
        {
            _pool = WaspBotPool.Instance as WaspBotPool;
            _timer = new RepeatableTimer(spawnRate);
        }

        private void Update()
        {
            _timer.Tick(Time.deltaTime);

            if (_timer.IsReady())
                SpawnBot();
        }

        private void SpawnBot()
        {
            if (pathPoints == null || pathPoints.Count == 0)
            {
                Debug.LogWarning("No path points assigned!");
                return;
            }

            _currentBotCount = _pool.GetNumberOfAlive();
            if (_currentBotCount < maxAliveBots)
            {
                var newBot = WaspBotPool.Instance.Get();
                if (newBot == null) return;

                newBot.gameObject.SetActive(true);
                newBot.transform.position = spawnPosition.position;

                int portion = Mathf.Max(1, pathPoints.Count / maxAliveBots);
                int startIndex = (_currentBotCount % maxAliveBots) * portion;
                startIndex = Mathf.Min(startIndex, pathPoints.Count - portion);

                var botPointPortion = pathPoints.GetRange(startIndex, 
                    Mathf.Min(portion, pathPoints.Count - startIndex));
                    
                newBot.SetPathPoints(botPointPortion);
            }
        }

        public void ReduceAliveCount() => _currentBotCount--;
    }
}