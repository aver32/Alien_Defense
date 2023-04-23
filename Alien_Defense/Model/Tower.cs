using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    internal class Tower
    {
        private int Damage { get; }
        private int Cost { get; }
        public Tower(int damage, int cost)
        {
            Damage = damage;
            Cost = cost;
        }
    }
}
