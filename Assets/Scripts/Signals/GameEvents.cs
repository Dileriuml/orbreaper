namespace OrbReaper.Signals
{
    public static class GameEvents
    {
        public static class PlayerEvents
        {
            public struct HealthChangedSignal {}
        
            public struct MaxHealthChangedSignal {}

            public struct PowerChangedSignal {}
            
            public struct MaxPowerChangedSignal {}
            
            public struct PlayerDiedSignal {}
        }

        public static class EnemyEvents
        {
            public class EnemySignal
            {
                public EnemyFacade enemyFacade { get; set; } 
            }
            
            public class EnemySpawnedSignal : EnemySignal
            {
            }

            public class EnemyDiedSignal : EnemySignal
            {
            }
        }
    }
}