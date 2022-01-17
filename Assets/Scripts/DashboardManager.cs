using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//using Button = UnityEngine.UI.Button;

public class DashboardManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Camera virtualCamera;
    [SerializeField] private RawImage receiveImage;

    private WebCamTexture tex;
    

    private void OnStart()
    {
        startButton.interactable = false;

        if(virtualCamera != null)
        {
            tex = new WebCamTexture(virtualCamera.name);
        }

        receiveImage.texture = tex;
        receiveImage.color = Color.white;
        tex.Play();
    }
}
