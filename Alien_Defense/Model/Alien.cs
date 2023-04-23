using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Alien_Defense.Model
{
    public class Alien
    {
        public int Health { get; }
        public int Damage { get; }
        public float MoveSpeed { get; }
        public Vector2 InitialPosition { get; }
        public Vector2 CurrentPosition { get; private set; }
        public Alien(Vector2 initialPosition)
        {
            Health = 50;
            Damage = 25;
            MoveSpeed = 3;

            InitialPosition = initialPosition;
            CurrentPosition = InitialPosition;
        }
    }
}
