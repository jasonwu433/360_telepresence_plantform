using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject referencePoint;
    public float distanceOffset = 1.5f;
    public float hightOffset = 0.5f;
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
            var temp1 = referencePoint.transform.position.z + distanceOffset;
            var temp2 = referencePoint.transform.position.y + hightOffset;
            gameObject.transform.position = new Vector3(referencePoint.transform.position.x, temp2, temp1);
        }
    }

    
}
