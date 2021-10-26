using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    [SerializeField] InputActionReference xButtonInputAction;
    [SerializeField] string[] scenes;
    private string currentScene;
    private bool xButtonPressed;

    private void Start()
    {
        Initial();
    }

    private void Initial()
    { 
        if(scenes == null)
            Debug.LogWarning("Put the name of all the scenes in the inspector");
        xButtonPressed = false;
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void OnEnable()
    {
        xButtonInputAction.action.performed += XButtonPressed;
    }

    private void OnDisable()
    {
        xButtonInputAction.action.performed -= XButtonPressed;
    }

    private void XButtonPressed(InputAction.CallbackContext obj)
    {
        xButtonPressed = true;
        if (xButtonPressed)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i] == currentScene)
                    SceneManager.LoadScene(scenes[i+1]);
            }
        }
        else
            return;
        xButtonPressed = !xButtonPressed;
    } 

}
