using Microsoft.Xna.Framework;

namespace Tank2D_XNA.Screens
{
    abstract class Screen : Entity
    {
        public abstract void ButtonClicked(Vector2 loc);
    }
}
