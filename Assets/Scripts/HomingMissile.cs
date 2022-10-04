using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{

    public Transform target;

    public float speed = 5f;
    public float rotationSpeed = 200f;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        rb.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AI")
        {
            collision.transform.parent.gameObject.GetComponent<CarManager>().GetHit();
            Destroy(this.gameObject);
        }
    }
}
