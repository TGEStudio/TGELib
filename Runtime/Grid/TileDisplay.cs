using UnityEngine;

namespace TGELib.Grid
{
    public class TileDisplay : MonoBehaviour
    {
        private TileBase _tile;
        private SpriteRenderer _spriteRenderer;
        public TileBase tile { get { return _tile; } set { _tile = value; } }

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            _spriteRenderer.sprite = _tile.gSprite;
        }
    }
}