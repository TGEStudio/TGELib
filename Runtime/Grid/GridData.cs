using UnityEngine;

namespace TGELib.Grid
{
    [CreateAssetMenu(fileName = "Default Grid", menuName = "TGE/New Grid")]
    public class GridData : ScriptableObject
    {
        [Header("Spawn Object")]
        public GameObject gSpawnObject;

        [Header("Spawn Position")]
        public Vector2 gSpawnAnchor;

        [Header("Grid Asset")]
        public Vector2 gSize;
        public Sprite gSpriteIn;
        public Sprite gSpriteOut;
    }
}
