using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public Transform platform;
    private bool forward;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        forward = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (platform.gameObject == null)
        {
            return;
        }
        if (forward)
        {
            if (platform.position.x < end.position.x)
            {
                platform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
            }
            if (platform.position.x >= end.position.x)
            {
                forward = false;
            }
        }
        else
        {
            if (platform.position.x > start.position.x)
            {
                platform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime);
            }
            if (platform.position.x <= start.position.x)
            {
                forward = true;
            }
        }
    }
}
