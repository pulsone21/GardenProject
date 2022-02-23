using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class AssetCapture : MonoBehaviour
    {
        public string PictureName;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T) && Input.GetKey(KeyCode.LeftControl))
            {
                if (PictureName.Length < 1)
                {
                    Debug.LogError("Please enter an PictureName!");
                    return;
                }
                ScreenCapture.CaptureScreenshot($"Assets/Screenshots/RawPictures/{PictureName}.png");
                Debug.Log($"Screenshot Taken with Name {PictureName}");
            }

        }
    }
}
