using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(gameObject.name + "collided with" + collision.gameObject);
         CheckAction(collision.collider);
    }

    private void CollisionEnter2D(Collider2D collision)
    {

        
        


       


    }

    void CheckAction(Collider2D hittedObject)
    {
        if (gameObject.name == "FrontCollider")
        {
            //call action to destroy enemy
            /*
            GetComponent<BoxCollider2D>().enabled = false;
            hittedObject.gameObject.GetComponent<CarManager>().GetHit();*/
        }
        else if (gameObject.name == "BackCollider")
        {
            //call action to destroy this object
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.parent.gameObject.GetComponent<CarManager>().GetHit();

        }
    }



}
