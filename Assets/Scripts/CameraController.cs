using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float offset;
    public float smoothSpeed;
    // Start is called before the first frame update
    void Start()
    {
        smoothSpeed = 0.125f;
        offset = 6f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
