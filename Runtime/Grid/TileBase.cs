using UnityEngine;

namespace TGELib.Grid
{
    public class TileBase
    {
        private Vector2 _tPos;
        private Sprite _tSprite;

        public Vector2 gPos { get { return _tPos; } }
        public Sprite gSprite { get { return _tSprite; } }
        public TileBase(Vector2 tPos, Sprite tSprite)
        {
            _tPos = tPos;
            _tSprite = tSprite;
        }
        public void SetSprite(Sprite tSprite)
        {
            _tSprite = tSprite;
        }
    }
}
