using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float followSpeed;

    Vector2 targetPos;
    float zPos;

    // Start is called before the first frame update
    void Start()
    {
        zPos = -10f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPos = new Vector3(target.position.x, target.position.y, zPos);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
