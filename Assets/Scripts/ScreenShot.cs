using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityTemplateProjects;

public class ScreenShot : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenImageWindow(string url);

    // Use this for initialization
    public void Screenshot()
    {
        StartCoroutine(UploadPNG());
    }

    private IEnumerator UploadPNG()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();

        //string ToBase64String byte[]
        string encodedText = System.Convert.ToBase64String(bytes);
        Debug.Log("Original: " + encodedText);
        
        byte[] scaledBytes = TextureScaler.scaled(tex, width / 3, height / 3, FilterMode.Point).EncodeToPNG();
        
        string scaledEncodedText = System.Convert.ToBase64String(scaledBytes);
        Debug.Log("scaled: " + scaledEncodedText);

        Destroy(tex);
        var image_url = "data:image/png;base64," + scaledEncodedText;

        Debug.Log(image_url);

#if !UNITY_EDITOR
        OpenImageWindow(image_url);
#endif
    }
}