using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    internal class Tower
    {
        private int Damage { get; }
        public int Cost { get; }
        public TowerCell Cell { get; }
        
        public Texture2D Texture { get; set; }


        public Tower(int damage, int cost)
        {
            Damage = damage;
            Cost = cost;
        }
    }
}
