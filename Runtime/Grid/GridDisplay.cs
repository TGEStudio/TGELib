using UnityEngine;

namespace TGELib.Grid
{
    public class GridDisplay : MonoBehaviour
    {
        private GridBase _grid;
        private SpriteRenderer _spriteRenderer;
        public GridBase grid { get { return _grid; } set { _grid = value; } }

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            _spriteRenderer.sprite = _grid.gSprite;
        }
    }
}