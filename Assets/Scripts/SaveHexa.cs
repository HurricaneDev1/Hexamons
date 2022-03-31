using UnityEngine;
 //System.IO;: You need this to find the files in your Directory
 using System.IO;
 
 public class SaveHexa: MonoBehaviour {
     //screenCaptureKey: Change this to any key you want. It is easily changeable in the editor
     public bool UseSimple = false;
     private StatsGenerator stats;
    //  void OnEnable() {
    //     var cam = Camera.main;
    //     // Set a mask to only draw only elements in this layer. e.g., capture your player with a transparent background.
    //     cam.cullingMask = LayerMask.GetMask("Default");
 
    //     string filename = string.Format("Screenshots/capture_{0}.png", 0);
    //     int width = Screen.width;
    //     int height = Screen.height;
    //     if (UseSimple) {
    //         CaptureScreenshot.SimpleCaptureTransparentScreenshot(cam, width, height, filename);
    //     }
    //     else {
    //         CaptureScreenshot.CaptureTransparentScreenshot(cam, width, height, filename);
    //     }
    //  }

     void Update(){
         if(Input.GetKeyDown(KeyCode.K)){
            var cam = Camera.main;
            // Set a mask to only draw only elements in this layer. e.g., capture your player with a transparent background.
            cam.cullingMask = LayerMask.GetMask("Default");
            
            stats = this.GetComponent<StatsGenerator>();
    
            string filename = string.Format("Assets/SavedHexamon/"+ stats.monName +".png", 0);
            Debug.Log("Mon Saved");
            int width = Screen.width;
            int height = Screen.height;
            if (UseSimple) {
                CaptureScreenshot.SimpleCaptureTransparentScreenshot(cam, width, height, filename);
            }
            else {
                CaptureScreenshot.CaptureTransparentScreenshot(cam, width, height, filename);
            }

         }
     }
 }
 
 public static class CaptureScreenshot {
     public static void CaptureTransparentScreenshot(Camera cam, int width, int height, string screengrabfile_path) {
         // This is slower, but seems more reliable.
         var bak_cam_targetTexture = cam.targetTexture;
         var bak_cam_clearFlags = cam.clearFlags;
         var bak_RenderTexture_active = RenderTexture.active;
 
         var tex_white = new Texture2D(width, height, TextureFormat.ARGB32, false);
         var tex_black = new Texture2D(width, height, TextureFormat.ARGB32, false);
         var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
         // Must use 24-bit depth buffer to be able to fill background.
         var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
         var grab_area = new Rect(0, 0, width, height);
 
         RenderTexture.active = render_texture;
         cam.targetTexture = render_texture;
         cam.clearFlags = CameraClearFlags.SolidColor;
 
         cam.backgroundColor = Color.black;
         cam.Render();
         tex_black.ReadPixels(grab_area, 0, 0);
         tex_black.Apply();
 
         cam.backgroundColor = Color.white;
         cam.Render();
         tex_white.ReadPixels(grab_area, 0, 0);
         tex_white.Apply();
 
         // Create Alpha from the difference between black and white camera renders
         for (int y = 0; y < tex_transparent.height; ++y) {
             for (int x = 0; x < tex_transparent.width; ++x) {
                 float alpha = tex_white.GetPixel(x, y).r - tex_black.GetPixel(x, y).r;
                 alpha = 1.0f - alpha;
                 Color color;
                 if (alpha == 0) {
                     color = Color.clear;
                 } 
                 else {
                     color = tex_black.GetPixel(x, y) / alpha;
                 }
                 color.a = alpha;
                 tex_transparent.SetPixel(x, y, color);
             }
         }
 
         // Encode the resulting output texture to a byte array then write to the file
         byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
         File.WriteAllBytes(screengrabfile_path, pngShot);
 
         cam.clearFlags = bak_cam_clearFlags;
         cam.targetTexture = bak_cam_targetTexture;
         RenderTexture.active = bak_RenderTexture_active;
         RenderTexture.ReleaseTemporary(render_texture);
 
         Texture2D.Destroy(tex_black);
         Texture2D.Destroy(tex_white);
         Texture2D.Destroy(tex_transparent);
     }
 
     public static void SimpleCaptureTransparentScreenshot(Camera cam, int width, int height, string screengrabfile_path) {
         // Depending on your render pipeline, this may not work.
         var bak_cam_targetTexture = cam.targetTexture;
         var bak_cam_clearFlags = cam.clearFlags;
         var bak_RenderTexture_active = RenderTexture.active;
 
         var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
         // Must use 24-bit depth buffer to be able to fill background.
         var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
         var grab_area = new Rect(0, 0, width, height);
 
         RenderTexture.active = render_texture;
         cam.targetTexture = render_texture;
         cam.clearFlags = CameraClearFlags.SolidColor;
 
         // Simple: use a clear background
         cam.backgroundColor = Color.clear;
         cam.Render();
         tex_transparent.ReadPixels(grab_area, 0, 0);
         tex_transparent.Apply();
 
         // Encode the resulting output texture to a byte array then write to the file
         byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
         File.WriteAllBytes(screengrabfile_path, pngShot);
 
         cam.clearFlags = bak_cam_clearFlags;
         cam.targetTexture = bak_cam_targetTexture;
         RenderTexture.active = bak_RenderTexture_active;
         RenderTexture.ReleaseTemporary(render_texture);
 
         Texture2D.Destroy(tex_transparent);
     }
 }