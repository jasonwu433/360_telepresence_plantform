using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.SceneManagement;

public class XRSocketEvent : MonoBehaviour
{ 
    private XRSocketInteractor socket; //The Current Socket
    private string operatorName; //Operator   
    public socketProperties socketProperty; //Socket property
    public enum socketProperties {none, NA1, NA2, NA3, NA4, OA1, OA2, OA3 };

    // Start is called before the first frame update
    void Awake()
    {    
        socket = gameObject.GetComponent<XRSocketInteractor>(); //Get socket
        socket.selectEntered.AddListener(GetObjectProperty); //Socket in event
        socket.selectExited.AddListener(ResetObjectProperty); //Socket out event
    }

    //Get the property of the object and referrence the number or operator to the Game management script
    public void GetObjectProperty(SelectEnterEventArgs args)
    {
        if (SceneManager.GetActiveScene().name == "Calculation Games")
        {
            XRBaseInteractable obj = args.interactable; //The interactable object that enter the socket
                                                        //Catergory the number and operator 
            if (obj.gameObject.GetComponent<Snap_objects>().snapObjectProperty == Snap_objects.snapObjectProperties.number)
            {
                switch (socketProperty)
                {
                    case socketProperties.NA1:
                        GameManager.Instance.number1 = int.Parse(obj.gameObject.GetComponentInChildren<TMP_Text>().text);
                        break;
                    case socketProperties.NA2:
                        GameManager.Instance.number2 = int.Parse(obj.gameObject.GetComponentInChildren<TMP_Text>().text);
                        break;
                    case socketProperties.NA3:
                        GameManager.Instance.number3 = int.Parse(obj.gameObject.GetComponentInChildren<TMP_Text>().text);
                        break;
                    case socketProperties.NA4:
                        GameManager.Instance.number4 = int.Parse(obj.gameObject.GetComponentInChildren<TMP_Text>().text);
                        break;
                    default:
                        break;
                }
            }
            else if (obj.gameObject.tag == "Operator")
            {
                switch (obj.gameObject.GetComponent<Snap_objects>().snapObjectProperty)
                {
                    case Snap_objects.snapObjectProperties.divide:
                        operatorName = "/";
                        break;
                    case Snap_objects.snapObjectProperties.times:
                        operatorName = "*";
                        break;
                    case Snap_objects.snapObjectProperties.minus:
                        operatorName = "-";
                        break;
                    case Snap_objects.snapObjectProperties.plus:
                        operatorName = "+";
                        break;
                    default:
                        break;
                }

                switch (socketProperty)
                {
                    case socketProperties.OA1:
                        GameManager.Instance.operator1 = operatorName;
                        break;
                    case socketProperties.OA2:
                        GameManager.Instance.operator2 = operatorName;
                        break;
                    case socketProperties.OA3:
                        GameManager.Instance.operator3 = operatorName;
                        break;
                    default:
                        break;
                }
            }
            GameManager.Instance.Warning(socketProperty); //Show the warning and guideline 
        }
        else
            InteractionTutorial.Instance.Warning(socketProperty);       
    }

    private void ResetObjectProperty(SelectExitEventArgs args)
    {
        if (SceneManager.GetActiveScene().name == "Calculation Games")
        {
            XRBaseInteractable obj = args.interactable; //The interactable object that enter the socket
                                                        //Catergory the number and operator 
            if (obj.gameObject.GetComponent<Snap_objects>().snapObjectProperty == Snap_objects.snapObjectProperties.number)
            {
                switch (socketProperty)
                {
                    case socketProperties.NA1:
                        GameManager.Instance.number1 = 0;
                        break;
                    case socketProperties.NA2:
                        GameManager.Instance.number2 = 0;
                        break;
                    case socketProperties.NA3:
                        GameManager.Instance.number3 = 0;
                        break;
                    case socketProperties.NA4:
                        GameManager.Instance.number4 = 0;
                        break;
                    default:
                        break;
                }
            }
            else if (obj.gameObject.tag == "Operator")
            {
                operatorName = null;

                switch (socketProperty)
                {
                    case socketProperties.OA1:
                        GameManager.Instance.operator1 = operatorName;
                        break;
                    case socketProperties.OA2:
                        GameManager.Instance.operator2 = operatorName;
                        break;
                    case socketProperties.OA3:
                        GameManager.Instance.operator3 = operatorName;
                        break;
                    default:
                        break;
                }
            }
        }
        else
            return;
        
    }
}
