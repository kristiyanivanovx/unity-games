using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float forwardInput;

    public float speed = 40f;
    public float xRange = 24f;

    public float backBound = 7.94f;
    public float forwardBound = 43f;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {    
        // boundaries
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < backBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,  backBound);
        }
        if (transform.position.z > forwardBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, forwardBound);
        }

        // shooting logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }

        // moving the player
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        //forwardInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * speed);
    }
}
