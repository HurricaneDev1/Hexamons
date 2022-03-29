using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonPicture : MonoBehaviour
{
    public int superSize = 2;
    private int _shotIndex = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) {
            Debug.Log("Mon Saved!");
            ScreenCapture.CaptureScreenshot($"Screenshot{_shotIndex}.png", superSize);
            _shotIndex++;
        }
    }
}
