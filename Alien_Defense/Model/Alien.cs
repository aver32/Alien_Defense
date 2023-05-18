using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    internal class Alien
    {
        public List<Rectangle> path = new List<Rectangle>();
        public int currentPathIndex;
        public Vector2 currentPosition;
        private double _damage;
        public float speed;

        public Alien(Vector2 initPos, List<Rectangle> alienPath, double damage, float speed)
        {
            currentPosition = initPos;
            path = alienPath;
            this.speed = speed;
            _damage = damage;
        }
    }
}
