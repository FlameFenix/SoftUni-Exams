namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {

        private Dictionary<int, IEnemy> enemies = new Dictionary<int, IEnemy>();

        public int Size => enemies.Count;

        public bool Contains(IEnemy enemy) => enemies.ContainsKey(enemy.AttackSpeed);

        public void Create(IEnemy enemy)
        {
            if (!Contains(enemy))
            {
                enemies.Add(enemy.AttackSpeed, enemy);
            }
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            IEnemy enemy = null;

            if (enemies.ContainsKey(speed))
            {
                enemy = enemies[speed];
            }
            
            return enemy;
        }

        public List<IEnemy> GetFaster(int speed)
        => enemies.Values.Where(x => x.AttackSpeed > speed).ToList();

        public IEnemy GetFastest()
        {
            if(Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies");
            }

            var fastest = enemies.Max(x => x.Key);

            return enemies[fastest];
        }

        public IEnemy[] GetOrderedByHealth()
            => enemies.Values.OrderByDescending(x => x.Health).ToArray();

        public List<IEnemy> GetSlower(int speed)
        => enemies.Values.Where(x => x.AttackSpeed < speed).ToList();

        public IEnemy GetSlowest()
        {
            if(Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            int slower = enemies.Min(x => x.Key);

            return enemies[slower];
        }

        public void ShootFastest()
        {
           if(Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            int fastest = enemies.Max(x => x.Key);

            enemies.Remove(fastest);
        }

        public void ShootSlowest()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            int slowest = enemies.Min(x => x.Key);

            enemies.Remove(slowest);
        }
    }
}
