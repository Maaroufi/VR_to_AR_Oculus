using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create3DBox : MonoBehaviour {

    public Camera oculusCam;

// Convert the 2D position of the mouse into a
// 3D position.  Display these on the game window.


    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = oculusCam.pixelHeight - currentEvent.mousePosition.y;
        GUI.contentColor = Color.black;
        point = oculusCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, oculusCam.nearClipPlane));
        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + oculusCam.pixelWidth + ":" + oculusCam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }
}
