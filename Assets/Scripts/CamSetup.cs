using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.IO.Ports;
using UnityEngine.UI;

public class CamSetup : MonoBehaviour {

    //la raw image sur laquel je vais rendre ma texture de webcam
    [SerializeField] private RawImage planeToRenderTexture;

    //la webcam que je choisi
    private WebCamDevice device;

    //la texture de ma webcam activée
    //private WebCamTexture mCameraTexture = null;
    public int test;

    //if using a plane or prefab gameobject
    public GameObject WebcamTexurePrefab;

    WebCamTexture mCameraTexture;

    // Use this for initialization
    void Start()
    {

        WebCamDevice[] devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        //mCameraTexture = new WebCamTexture(devices[test].name, 10000, 10000, 30);

        
        mCameraTexture = new WebCamTexture(devices[test].name);
        mCameraTexture.requestedFPS = 30f;                   //Merveilleux, un float pour le FPS...
        mCameraTexture.requestedWidth = 1920;   //int de calibration, à vous de choisir
        mCameraTexture.requestedWidth = 1080;	//int de calibration, à vous de choisir
        planeToRenderTexture.texture = mCameraTexture;				//RawImage d'affichage
        mCameraTexture.Play();


        /*WebCamTexture mCameraTexture = new WebCamTexture(devices[test].name, 1920, 1080, 30);
        planeToRenderTexture.texture = mCameraTexture;              //RawImage d'affichage
        mCameraTexture.Play();*/

        //OnCameraDisplay();
    }

     public void OnCameraDisplay()
    {
        //je connais la texture qui contiendra l'image de ma webcam il est tant que je l'applique à ma Raw Image
        //planeToRenderTexture.texture = mCameraTexture;

        //if using plane
        //WebcamTexurePrefab.GetComponent<Renderer>().material.mainTexture = mCameraTexture;

        //on lance la webcam
        //mCameraTexture.Play();

    }

    void Update()
    {
        //transform.rotation = baseRotation * Quaternion.AngleAxis(mCameraTexture.videoRotationAngle, Vector3.up);
    }

}
