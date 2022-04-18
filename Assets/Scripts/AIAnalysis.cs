using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Sockets;
using System;
using System.Threading;

public class AIAnalysis : MonoBehaviour
{
    public TMP_Text HR, GSR, emotion;
    [SerializeField] private RenderTexture avatarFaceCameraTexture;
    [HideInInspector]
    public static string hr_str, gsr_str, emotion_str;


    static TcpClient middleware = null;
    string Host = "localhost";
    public Int32 Port = 8080;
    Thread thread;
    string[] emotions = new string[] { "angry", "calm", "disgust", "fearful", "happy", "neutral", "sad", "surprise" };

    // Start is called before the first frame update
    void Start()
    {
        ReadExternalData();
        if(middleware == null)
        {
            middleware = new TcpClient();
            try
            {
                middleware.Connect(Host, Port);
            }
            catch (Exception e)
            {
                Debug.Log("Socket error: " + e);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(sendToMiddlewareEngine());
        InformationDisplay();
    }

    public void ReadExternalData()
    {
        var temp = "Loading...";
        hr_str = temp;
        gsr_str = temp;
        emotion_str = temp;
    }

    public void InformationDisplay()
    {
        HR.text = "HR: " + hr_str;
        GSR.text = "GSR: " + gsr_str;
        emotion.text = "Emotion: " + emotion_str;
    }

    IEnumerator sendToMiddlewareEngine()
    {
        if (thread == null || !thread.IsAlive)
        {
            // We should only read the screen buffer after rendering is complete
            yield return new WaitForEndOfFrame();

            RenderTexture.active = avatarFaceCameraTexture;
            Texture2D tex = new Texture2D(avatarFaceCameraTexture.width, avatarFaceCameraTexture.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, avatarFaceCameraTexture.width, avatarFaceCameraTexture.height), 0, 0);
            RenderTexture.active = null;

            byte[] bytes;
            bytes = tex.EncodeToJPG();
        
            thread = new Thread(() => SendMessage(bytes));
            thread.Start();
        }
    }

    public static void SendVoiceData(byte[] data)
    {
        string emotion = AIAnalysis.SendMessage(data);
    }

    private static string SendMessage(byte[] data)
    {
        try
        {
            byte[] rec_data = new byte[30];
            // Get a stream object for writing. 			
            NetworkStream stream = middleware.GetStream();
            if (stream.CanWrite)
            {
                stream.Write(data, 0, data.Length);
                Debug.Log("Client sent his message - should be received by server");

                if (stream.CanRead)
                {
                    stream.Read(rec_data, 0, rec_data.Length);
                    string emotion = System.Text.Encoding.UTF8.GetString(rec_data, 0, rec_data.Length);

                    //emotion_str = emotion;//emotions[int.Parse(emotion)];
                    Debug.Log("Recived from server : " + emotion);
                    return emotion;
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
        return "";
    }
}
