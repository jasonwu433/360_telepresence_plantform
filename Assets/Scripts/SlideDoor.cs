using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    public float speed = 5f;
    public float maxOpen = 15f;
    
    public void OpenTheDoor()
    {
        if (-gameObject.transform.position.z < maxOpen)
        {
            gameObject.transform.Translate(0f, 0f, -speed * Time.deltaTime);
        }
    }

}
