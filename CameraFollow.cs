using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float scale = 4f;

    private Transform t;

    private void Awake()
    {
        //getting the camera object
        
        var cam = GetComponent<Camera>();
        
        // setting the size of the camera to the size of the screen so it is dynamic
        
        cam.orthographicSize = (Screen.height/ 2f) / scale;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // set it on the camera
        t = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //move the camera with the players
            transform.position = new Vector3(t.position.x, t.position.y, transform.position.z);
        }
    }
}
