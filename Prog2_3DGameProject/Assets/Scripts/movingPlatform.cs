using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    // Platform starting and end position values
    public Vector3 myStartPosition = new Vector3(-2, 0, -4);
    public Vector3 myEndPosition = new Vector3(-2, 0, 1);
    public int speed = 2;
    public bool forward = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,gameObject.transform.position.z + 1);

        //Platforms goes backward when end position is reached
        if (gameObject.transform.position.z >= myEndPosition.z)
        {
            forward = false;
        }
        //Platform switches forward when reaching start position 
        if (gameObject.transform.position.z <= myStartPosition.z)
        {
            forward = true;
        }

        //Add speed when moving forward
        if (forward == true)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + (Time.deltaTime * speed));
        }
        //Subtract speed when going backward
        if (forward == false)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - (Time.deltaTime * speed));
        }
    }
}
