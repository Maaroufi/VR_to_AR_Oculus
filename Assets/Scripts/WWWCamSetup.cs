using UnityEngine;
using System.Collections;

public class WWWCamSetup : MonoBehaviour
{
    // https://homepages.cae.wisc.edu/~ece533/images/airplane.png
    public string uri;

    Texture2D cam;


    public void Start()
    {
        cam = new Texture2D(1, 1, TextureFormat.RGB24, false);
        StartCoroutine(Fetch());
    }


    public IEnumerator Fetch()
    {
        while (true)
        {
            Debug.Log("fetching... " + Time.realtimeSinceStartup);

            WWW www = new WWW(uri);
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
                throw new UnityException(www.error);

            www.LoadImageIntoTexture(cam);
        }
    }


    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), cam);
    }

}
