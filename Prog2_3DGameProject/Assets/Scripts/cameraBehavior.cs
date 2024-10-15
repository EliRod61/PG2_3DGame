using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehavior : MonoBehaviour
{

    Vector3 myLook;
    public float lookSpeed = 100f;
    public Camera myCam;
    public float camLock = 90f;

    float onStartTimer;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        myLook = transform.localEulerAngles;

    }

    //public Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);
    //private Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        //_target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    /*void LateUpdate()
    {
        this.transform.position = _target.TransformPoint(CamOffset);
      
        this.transform.LookAt(_target);
    }*/
    void Update()
    {
        myLook += DeltaLook() * lookSpeed * Time.deltaTime;
        myLook.y = Mathf.Clamp(myLook.y, -camLock, camLock);

        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);

        myCam.transform.rotation = Quaternion.Euler(-myLook.y, myLook.x, 0f);
        
    }

    //here we're going to calculate the difference in mouse position (on screen) relative to the previous frame
    Vector3 DeltaLook()
    {
        Vector3 dLook;
        float rotY = Input.GetAxisRaw("Mouse Y");
        float rotX = Input.GetAxisRaw("Mouse X");
        dLook = new Vector3(rotX, rotY, 0);
        if (dLook != Vector3.zero) { Debug.Log("delta look " + dLook); }

        if(onStartTimer < 1f)
        {
            dLook = Vector3.ClampMagnitude(dLook, onStartTimer);
        }
        return dLook;
    }
}
