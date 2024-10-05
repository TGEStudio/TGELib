using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGElib.Grid
{
    public class GridDisplay : MonoBehaviour
    {
        private Grid _grid;
        private SpriteRenderer _spriteRenderer;
        public Grid grid { get { return _grid; } set { _grid = value; } }

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