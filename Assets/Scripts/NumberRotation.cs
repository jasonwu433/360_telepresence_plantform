using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberRotation : MonoBehaviour
{
    public float speed;
    private float yRotation;

    // Update is called once per frame
    void Update()
    {
        yRotation = 30f * speed * Time.deltaTime;
        gameObject.transform.Rotate(new Vector3(0, yRotation, 0));
    }
}
