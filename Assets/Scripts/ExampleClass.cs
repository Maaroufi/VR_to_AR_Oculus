// Add this script to a GameObject. The Start() function fetches an
// image from the documentation site.  It is then applied as the
// texture on the GameObject.

using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    private string url = "http://localhost:5000/video_feed";

    IEnumerator Start()
    {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        using (WWW www = new WWW(url))
        {
            yield return www;
            www.LoadImageIntoTexture(tex);
            GetComponent<Renderer>().material.mainTexture = tex;
        }
    }
}