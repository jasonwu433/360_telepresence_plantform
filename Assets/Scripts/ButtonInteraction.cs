using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonInteraction : MonoBehaviour
{
    public TMP_Text jobButtonContentText, helpButtonContentText;

    public void OnJobButtongClicked()
    {
        jobButtonContentText.text = "You have to put the number cubes and operator spheres in the right order!";
    }

    public void OnHelpButtonClicked()
    {
        helpButtonContentText.text = "Please help me, I donot know how to do it!";
    }
}
