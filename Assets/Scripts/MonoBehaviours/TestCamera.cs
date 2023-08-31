using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public float speed = 30, rotationSpeed = 140;
    public float zoomSpeed = 4.6f;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.Q) && transform.position.y > 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - zoomSpeed * Time.deltaTime, transform.position.z);
        }
        if(Input.GetKey(KeyCode.E) && transform.position.y < 300)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + zoomSpeed * Time.deltaTime, transform.position.z);
        }

        if(transform.position.y < minHeight)
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);

        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * h * rotationSpeed * Time.deltaTime);
        minHeight = /*Terrain.activeTerrain.SampleHeight(transform.position) +*/ 1.5f;
    }

    float minHeight = 0;
}
