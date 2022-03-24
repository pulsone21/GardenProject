using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GardenProject
{
    public class MouseCourserManager : MonoBehaviour
    {
        public static MouseCourserManager Instance;
        public enum CursorType { Dig, Harvest, Destroy, Seed, Fertilize, Watering }
        [SerializeField] private Texture2D defaultCourser;
        [SerializeField] private Texture2D fallBackCourser;
        [SerializeField] private Texture2D[] CursorSprites = new Texture2D[System.Enum.GetValues(typeof(CursorType)).Length];

        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }

            for (int i = 0; i < CursorSprites.Length; i++)
            {
                if (CursorSprites[i] == null) CursorSprites[i] = fallBackCourser;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            SetDefaultCursor();
        }

        public void SetCursor(CursorType cursorType)
        {
            Cursor.SetCursor(CursorSprites[(int)cursorType], Vector2.zero, CursorMode.Auto);
        }

        public void SetDefaultCursor()
        {
            Cursor.SetCursor(defaultCourser, new Vector2(0, 0), CursorMode.Auto);
        }
    }
}
