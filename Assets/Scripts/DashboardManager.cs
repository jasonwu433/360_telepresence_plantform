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
    [SerializeField] private RenderTexture avatarBodyCameraTexture;
    [SerializeField] private RenderTexture avatarFaceCameraTexture;
    [SerializeField] private RawImage avatarBodyWindowImage;
    [SerializeField] private RawImage avatarFaceWindowImage;
    [SerializeField] private Camera faceCamera;
    [SerializeField] private GameObject avatarHead;
    public float facCameraOffset = 0.5f;

    private void Update()
    {
        ControlAvatarFaceCamera();
    }

    public void StartButtonOnclick()
    {
        startButton.interactable = false;
        AvatarDisplay(avatarBodyWindowImage, avatarBodyCameraTexture, avatarFaceWindowImage, avatarFaceCameraTexture);
    }

    private void AvatarDisplay(RawImage bodyWindow, RenderTexture bodyCameraRenderTexture, RawImage faceWindow, RenderTexture faceCameraRenderTexture)
    {
        VirtualCameraDisplay(bodyWindow, bodyCameraRenderTexture); // bodywindow
        VirtualCameraDisplay(faceWindow, faceCameraRenderTexture); // facewindow
    }

    private void VirtualCameraDisplay(RawImage img, RenderTexture texture)
    {
        if (img != null && texture != null)
        {
            img.texture = texture;
            img.color = Color.white;
        }
    }

    private void ControlAvatarFaceCamera()
    {
        if (faceCamera != null && avatarHead != null)
        {
            //calculate camera relative position
            faceCamera.transform.position = new Vector3(avatarHead.transform.position.x, avatarHead.transform.position.y, avatarHead.transform.position.z + facCameraOffset);
        }
        else
            Debug.LogError("Please reference the avatar face camera and head");
    }


}
