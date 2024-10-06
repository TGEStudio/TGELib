using UnityEngine;

namespace TGELib.Grid
{
    public class GridBase
    {
        private Vector2 _gPos;
        private Sprite _gSprite;

        public Vector2 gPos { get { return _gPos; } }
        public Sprite gSprite { get { return _gSprite; } }
        public GridBase(Vector2 gPos, Sprite gSprite)
        {
            _gPos = gPos;
            _gSprite = gSprite;
        }
        public void SetSprite(Sprite gSprite)
        {
            _gSprite = gSprite;
        }
    }
}
