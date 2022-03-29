using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SaveMon : MonoBehaviour
{
    private static SaveMon instance;
    private Camera myCamera;
    private bool getImageNextFrame;

    private void Awake(){
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender(){
        if(getImageNextFrame){
            getImageNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32,false);
            Rect rect = new Rect(0,0, renderTexture.width,renderTexture.height);
            renderResult.ReadPixels(rect,0,0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/SavedMon.PNG",byteArray);
            Debug.Log("Saved SavedMon.PNG");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    private void TakeScreenShot(int width, int height){
        myCamera.targetTexture = RenderTexture.GetTemporary(width,height,16);
        getImageNextFrame = true;
    }

    public static void TakeScreenShot_Static(int width, int height){
        instance.TakeScreenShot(width,height);
    }
}
