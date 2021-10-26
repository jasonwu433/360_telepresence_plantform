using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int number1, number2, number3, number4;

    [HideInInspector]
    public string operator1, operator2, operator3;

    //Reference objects
    public Camera vrCamera;
    public GameObject[] numberCubes;
    public GameObject[] operatorSpheres;
    public GameObject[] snapZones;
    public GameObject[] levels;
    public AudioClip[] audioClips;
    public GameObject door;
    public TMP_Text warning;
    public TMP_Text targetNumberText;
    public TMP_Text resultText;

    private int[] targetNumbers;
    private int[,] numberSets;
    private bool isGameStart = false;
    private bool isCorrect = false;
    private bool isEquationReady = false;
    private int currentLevel;
    private string textHistory;

    // Singleton instance
    protected static GameManager instance = null;
    public static GameManager Instance
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

    // Start is called before the first frame update
    void Start()
    {
        Initial();
    }

    private void Initial()
    {
        targetNumbers = new int[4] { 34, 13, 4, 9 };
        numberSets = new int[4, 4] { {3, 4, 5, 9 },{2, 6, 7, 8 },{1, 2, 3, 4 },{3, 5, 6, 7 } };
        number1 = 0; number2 = 0; number3 = 0; number4 = 0;
        operator1 = null; operator2 = null; operator3 = null;
        isGameStart = true;
        currentLevel = 0;
    }
    // Update is called once per frame
    void Update()
    {
        CheckEquationStatus(ref isEquationReady);
        ProccedureControl(numberCubes, operatorSpheres);
        CheckLevelStatus();
    }
    public void Warning(XRSocketEvent.socketProperties socketProperty)
    {
        if (socketProperty == XRSocketEvent.socketProperties.NA1 || socketProperty == XRSocketEvent.socketProperties.NA2 || socketProperty == XRSocketEvent.socketProperties.NA3 || socketProperty == XRSocketEvent.socketProperties.NA4)
            warning.text = "Please put operators in the spheres!";
        if (socketProperty == XRSocketEvent.socketProperties.OA1 || socketProperty == XRSocketEvent.socketProperties.OA2 || socketProperty == XRSocketEvent.socketProperties.OA3)
            warning.text = "Please put numbers in the cubes! ";
    }
    private void CheckEquationStatus(ref bool isEquationReady)
    {
        if (number1 != 0 && number2 != 0 && number3 != 0 && number4 != 0 && operator1 != null && operator2 != null && operator3 != null)
            isEquationReady = true;
        else
            isEquationReady = false;
    }
    private void CheckLevelStatus()
    {
        if (levels[levels.Length-1].GetComponent<Renderer>().material.color == Color.green)
            door.GetComponent<SlideDoor>().OpenTheDoor();
    }
    private void ChangeLevelStatus(int level)
    {
        if(level < targetNumbers.Length)
        {
            // Change the level capsule color from red to green
            var levelRender = levels[level].GetComponent<Renderer>();
            levelRender.material.SetColor("_Color", Color.green);
        }
    }
    // 24 combinations for the squeence of numbers and operators
    private bool Calculation(int n1, int n2, int n3, int n4, string o1, string o2, string o3, int currentLevel)
    {
        int result;
        if (isEquationReady)
        {
            if (o1 == "+" && o2 == "-" && o3 == "*")
            {
                result = n1 + n2 - n3 * n4;
            }
            else if (o1 == "-" && o2 == "+" && o3 == "*")
            {
                result = n1 - n2 + n3 * n4;
            }
            else if (o1 == "*" && o2 == "+" && o3 == "-")
            {
                result = n1 * n2 + n3 - n4;
            }
            else if (o1 == "+" && o2 == "*" && o3 == "-")
            {
                result = n1 + n2 * n3 - n4;
            }
            else if (o1 == "-" && o2 == "*" && o3 == "+")
            {
                result = n1 - n2 * n3 + n4;
            }
            else if (o1 == "*" && o2 == "-" && o3 == "+")
            {
                result = n1 * n2 - n3 + n4;
            }
            else if (o1 == "*" && o2 == "-" && o3 == "/")
            {
                result = n1 * n2 - n3 / n4;
            }
            else if (o1 == "-" && o2 == "*" && o3 == "/")
            {
                result = n1 - n2 * n3 / n4;
            }
            else if (o1 == "/" && o2 == "*" && o3 == "-")
            {
                result = n1 / n2 * n3 - n4;
            }
            else if (o1 == "*" && o2 == "/" && o3 == "-")
            {
                result = n1 * n2 / n3 - n4;
            }
            else if (o1 == "-" && o2 == "/" && o3 == "*")
            {
                result = n1 - n2 / n3 * n4;
            }
            else if (o1 == "/" && o2 == "-" && o3 == "*")
            {
                result = n1 / n2 - n3 * n4;
            }
            else if (o1 == "/" && o2 == "+" && o3 == "*")
            {
                result = n1 / n2 + n3 * n4;
            }
            else if (o1 == "+" && o2 == "/" && o3 == "*")
            {
                result = n1 + n2 / n3 * n4;
            }
            else if (o1 == "*" && o2 == "/" && o3 == "+")
            {
                result = n1 * n2 / n3 + n4;
            }
            else if (o1 == "/" && o2 == "*" && o3 == "+")
            {
                result = n1 / n2 * n3 + n4;
            }
            else if (o1 == "+" && o2 == "*" && o3 == "/")
            {
                result = n1 + n2 * n3 / n4;
            }
            else if (o1 == "*" && o2 == "+" && o3 == "/")
            {
                result = n1 * n2 + n3 / n4;
            }
            else if (o1 == "-" && o2 == "+" && o3 == "*")
            {
                result = n1 - n2 + n3 * n4;
            }
            else if (o1 == "-" && o2 == "+" && o3 == "/")
            {
                result = n1 - n2 + n3 / n4;
            }
            else if (o1 == "+" && o2 == "-" && o3 == "/")
            {
                result = n1 + n2 - n3 / n4;
            }
            else if (o1 == "/" && o2 == "-" && o3 == "+")
            {
                result = n1 / n2 - n3 + n4;
            }
            else if (o1 == "-" && o2 == "/" && o3 == "+")
            {
                result = n1 - n2 / n3 + n4;
            }
            else if (o1 == "+" && o2 == "/" && o3 == "-")
            {
                result = n1 + n2 / n3 - n4;
            }
            else
            {
                result = n1 / n2 + n3 - n4;
            }
            return (targetNumbers[currentLevel] == result) ? true : false;
        }
        else
            return false;
            

    }

    private void AudioControl(string resultText)
    {
        if(textHistory == resultText) { return; }
        textHistory = resultText;

        if (resultText == "Correct!" && numberCubes[0].GetComponentInChildren<TMP_Text>().text == numberSets[3,0].ToString())
            AudioSource.PlayClipAtPoint(audioClips[2], vrCamera.transform.position);
        else if (resultText == "Correct!")
            AudioSource.PlayClipAtPoint(audioClips[0], vrCamera.transform.position);
        else if (resultText == "Wrong!")
            AudioSource.PlayClipAtPoint(audioClips[1], vrCamera.transform.position);      
    }
    private void ProccedureControl(GameObject[] numberCubes, GameObject[] operatorSpheres)
    {
        if (isGameStart)
        {
            if (currentLevel < targetNumbers.Length)
            {
                targetNumberText.text = targetNumbers[currentLevel].ToString(); // show the target number
                isCorrect = Calculation(number1, number2, number3, number4, operator1, operator2, operator3, currentLevel); // calculate the equation
                ChangeResultsText(isCorrect, isEquationReady); // show the result
            }
            else
            {
                targetNumberText.text = " ";
                warning.text = "Congratulation!";
            }

            //play audio for the result status
            AudioControl(resultText.text);
        }

    }

    private void ChangeResultsText(bool isCorrect, bool isEquationReady)
    {
        if (isCorrect)
        {   
            resultText.text = "Correct!";
            ChangeLevelStatus(currentLevel);
            StartCoroutine(SessionReset(currentLevel, numberCubes, operatorSpheres));
        }
        else if (!isCorrect && !isEquationReady)
            resultText.text = " ";
        else if (!isCorrect && isEquationReady)
            resultText.text = "Wrong!";
    }

    private IEnumerator SessionReset(int n, GameObject[] numberCubes, GameObject[] operatorSpheres)
    {
        yield return new WaitForSeconds(2);
        n += 1; //change the game level

        // disable XR socket interactor
        for (int i = 0; i < snapZones.Length; i++)
        {
            snapZones[i].GetComponent<XRSocketInteractor>().socketActive = false;
        }

        yield return new WaitForSeconds(1f);
        TransformsReset(numberCubes, operatorSpheres);

        if (snapZones[0].GetComponent<XRSocketInteractor>().socketActive == false)
        {      
            currentLevel = n;  //As IEnumerator cannot use ref parameter in and out, so we assign n to current level number again.
            NumbersReset(n, numberCubes); //Numbers on cubes for each level
        }

        // Enable XR socket interactor
        for (int i = 0; i < snapZones.Length; i++)
        {
            snapZones[i].GetComponent<XRSocketInteractor>().socketActive = true;
        }

        isCorrect = false;
    }

    private void TransformsReset(GameObject[] numberCubes, GameObject[] operatorSpheres)
    {
        // Reset cubes and spheres position
        numberCubes[0].transform.position = new Vector3(15.8f, 0.25f, 7.6f);
        numberCubes[1].transform.position = new Vector3(14.24f, 0.25f, 7.6f);
        numberCubes[2].transform.position = new Vector3(12.38f, 0.25f, 7.6f);
        numberCubes[3].transform.position = new Vector3(10.35f, 0.25f, 7.6f);
        operatorSpheres[0].transform.position = new Vector3(14.075f, 0.25f, 3.74f);
        operatorSpheres[1].transform.position = new Vector3(13.025f, 0.25f, 3.74f);
        operatorSpheres[2].transform.position = new Vector3(11.9f, 0.25f, 3.74f);
        operatorSpheres[3].transform.position = new Vector3(10.47f, 0.25f, 3.74f);

        // Reset cubes and spheres angle
        for (int i = 0; i < numberCubes.Length; i++)
        {
            numberCubes[i].transform.eulerAngles = Vector3.zero;
            operatorSpheres[i].transform.eulerAngles = Vector3.zero;
        }
    }

    private void NumbersReset(int n, GameObject[] numberCubes)
    {
        if (n < targetNumbers.Length)
        {
            for (int i = 0; i < numberCubes.Length; i++)
            {
                numberCubes[i].GetComponentInChildren<TMP_Text>().text = numberSets[n, i].ToString();
            }
        }
        else
            return;
    }


}
