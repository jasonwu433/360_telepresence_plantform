using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionTutorial : MonoBehaviour
{
    public TMP_Text warning;

    // Singleton instance
    protected static InteractionTutorial instance = null;
    public static InteractionTutorial Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
    }

    public void Warning(XRSocketEvent.socketProperties socketProperty)
    {
        if (socketProperty == XRSocketEvent.socketProperties.NA1 || socketProperty == XRSocketEvent.socketProperties.NA2 || socketProperty == XRSocketEvent.socketProperties.NA3 || socketProperty == XRSocketEvent.socketProperties.NA4)
            warning.text = "Please put operators in the spheres!";
        if (socketProperty == XRSocketEvent.socketProperties.OA1 || socketProperty == XRSocketEvent.socketProperties.OA2 || socketProperty == XRSocketEvent.socketProperties.OA3)
            warning.text = "Please put numbers in the cubes! ";
    }
}
