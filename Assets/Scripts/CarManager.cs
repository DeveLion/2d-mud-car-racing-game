using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{

    Animator anim;

    CarController CarController;
    CarControllerNew CarControllerNew;
    InputHandler inputHandler;

 


    public BoxCollider2D FrontCollider;
    public BoxCollider2D BackCollider;

    public Rigidbody2D[] carRigibodeis;
    public Collider2D[] carColliders;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CarControllerNew = GetComponent<CarControllerNew>();
        inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void GetHit()
    {
        Debug.Log(gameObject.name+ "get hitted");
        ManageCollidersAndRigidbodies(false);
        FrontCollider.enabled = false;
        BackCollider.enabled = false;
        inputHandler.canMove = false;
        CarController.StopCar();
        anim.SetBool("Hit", true);
        StartCoroutine(WaitToRespawn());
    }

    public void ResetCar()
    {
        ManageCollidersAndRigidbodies(true);
        anim.SetBool("Hit", false);
        FrontCollider.enabled = true;
        BackCollider.enabled = true;
        inputHandler.canMove = true;
        CarController.ResetCarMovement();
    }

    public void ManageCollidersAndRigidbodies(bool enable)
    {

        if (enable)
        {
            foreach (Rigidbody2D rb in carRigibodeis)
            {
                rb.gravityScale = 1;
            }

            foreach (Collider2D cd in carColliders)
            {
                cd.enabled = true;
            }

            carRigibodeis[0].gravityScale = 5;
        }
        else
        {
            foreach (Rigidbody2D rb in carRigibodeis)
            {
                rb.gravityScale = 0;
            }

            foreach (Collider2D cd in carColliders)
            {
                cd.enabled = false;
            }
        }
    }


    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        ManageCollidersAndRigidbodies(true);
        anim.SetBool("Hit", false);
        FrontCollider.enabled = true;
        BackCollider.enabled = true;
        inputHandler.canMove = true;
        CarController.ResetCarMovement();
    }


    public void Explosion()
    {
        CarController.canMove = false;
        ManageCollidersAndRigidbodies(false);
        anim.SetBool("Explosion", true);
    }

    public void CanDestroy()
    {
        gameObject.SetActive(false);
    }


    
}
