using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class PlayerControllerScript: MonoBehaviour
{
	Thread receiveThread;
	UdpClient client;
    IPEndPoint anyIP;
    int port;

    public GameObject glove;
    public Camera oculusCam;
    public Renderer rend, rendSphere;
    public GameObject interactObject;
    public GameObject plane;
    public Boolean colorShift;

    Vector3 point = new Vector3();
    float x;
    float y;

    bool detected;

    private void Awake()
    {
        // Switch to 640 x 480 full-screen
        //Screen.SetResolution(1080, 1200, true);
        detected = false;
        colorShift = false;

        rend = glove.GetComponent<Renderer>();
        rend.enabled = false;
    }
    void Start () 
	{
        rendSphere = interactObject.GetComponent<Renderer>();
        port = 5065;
        anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
        InitUDP();

        //Fetch the Renderer from the GameObject


        x = 0;
        y = 0;
    }

	// InitUDP
	private void InitUDP()
	{
		print ("UDP Initialized");
		receiveThread = new Thread (new ThreadStart(ReceiveData)); 
		receiveThread.IsBackground = true; 
		receiveThread.Start (); 
	}

	// 4. Receive Data
	private void ReceiveData()
	{
		client = new UdpClient (port); 
		while (true) 
		{
			try
			{
				byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);
                //print (">> " + text);
                string[] coordinates = text.Split('#');
                //print ("x coordinate ONLY = " + coordinates[0]);
                detected = true;
                x = float.Parse(coordinates[0]);
                y = float.Parse(coordinates[1]);

            } catch(Exception e)
			{
				print (e.ToString());
			}
		}
	}

	public void SendMessage()
	{
        print("hand detected");
	}

	// Check for variable value, and send message!
	void FixedUpdate() 
	{
		if(detected == true)
		{
            SendMessage();

            if(colorShift == false)
            {
                //rendSphere.material.shader = Shader.Find("_colorSphere");
                rendSphere.material.SetColor("_Color", Color.blue);
                colorShift = true;
            } else
            {
                //rendSphere.material.shader = Shader.Find("_colorSphere");
                rendSphere.material.SetColor("_Color", Color.red);
                colorShift = false;
            }  

            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = new Vector3(x, y, 0);
            rend.enabled = true;
            y = y - 416f;
            //point = oculusCam.ScreenToWorldPoint(new Vector3(x + 150f, Math.Abs(y), oculusCam.nearClipPlane + 3f));
            //point = oculusCam.ScreenToWorldPoint(new Vector3(x, y, interactObject.transform.position.z));
            point = new Vector3(plane.transform.position.x, plane.transform.position.y, plane.transform.position.z - 2f);
            
            glove.transform.position = point;
            detected = false;
        } else
        {
            //rend.enabled = false;
        }

	}

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        receiveThread.Abort();
        client.Close();
    }
}
