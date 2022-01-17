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
    [SerializeField] private RenderTexture avatarCameraTexture;
    [SerializeField] private RawImage receiveImage;
    
    public void StartButtonOnclick()
    {
        startButton.interactable = false;

        if(avatarCameraTexture != null)
        {
            receiveImage.texture = avatarCameraTexture;
            receiveImage.color = Color.white;
        }

        
    }
}
