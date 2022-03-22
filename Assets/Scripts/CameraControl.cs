using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject referencePoint;
    public float distanceOffset = 3.0f;
    public enum cameraType { body, head}

    public cameraType type = cameraType.body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    public void UpdateCamera()
    {
        if(type == cameraType.body)
        {
            var temp = referencePoint.transform.position.x - distanceOffset;
            gameObject.transform.position = new Vector3(temp, referencePoint.transform.position.y, referencePoint.transform.position.z);
        }
    }

    
}
