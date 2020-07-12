namespace Assets.Scripts.Constans
{
    public class Constants
    {
        public struct States
        {
            public const string isAlive = "isAlive";
            public const string isGrounded = "isGrounded";
            public const string isDead = "isDead";
        }

        public struct tags
        {
            public const string collectable = "Collectable";
            public const string enemy = "Enemy";
            public const string rock = "Rock";
        }

        public struct Language
        {
            public const string spanish = "es";
            public const string english = "en";
        }

        public struct Numbers
        {
            public const int DistanceCamera = -10;
        }

        public struct courrutines{
            public const string TirePlayer = "TirePlayer";
        }

        public struct CollectableLimits
        {
            public const int MaxHealth = 100;
            public const int MinHealth = 50;
            public const int MaxMana = 50;
            public const int MinMana = 50;
            public const string maxScore = "maxScore";
        }

    }
}
