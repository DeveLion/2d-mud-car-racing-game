using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public Rigidbody2D FrontWheel;
    public Rigidbody2D BackWheel;
    public Rigidbody2D Car;
    public float carSpeed;
    public float maxSpeed;
    public float input;

    public bool canMove;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove )
        {

            if (FrontWheel.velocity.magnitude < maxSpeed && BackWheel.velocity.magnitude < maxSpeed && Car.velocity.magnitude < maxSpeed)
            {
                FrontWheel.AddTorque(input * carSpeed + Time.fixedDeltaTime);
                BackWheel.AddTorque(input * carSpeed + Time.fixedDeltaTime);
                Car.AddTorque(input * carSpeed + Time.fixedDeltaTime);
            }
           
        }
        else
        {
            FrontWheel.velocity = Vector2.zero;
            BackWheel.velocity = Vector2.zero;
            Car.velocity = Vector2.zero;
        }
      
    }


    public void StopCar()
    {
        canMove = false;
        FrontWheel.velocity = Vector2.zero;
        BackWheel.velocity = Vector2.zero;
        Car.velocity = Vector2.zero;
    }

    public void ResetCarMovement()
    {
        canMove = true;
    }

    public void SetAIDifficult(string difficult) {


        if (difficult == "easy")
        {
            carSpeed = -2f;
            maxSpeed = -5f;
        }
        else if (difficult == "medium")
        {
            carSpeed = -2f;
            maxSpeed = 10f;
        }
        else if (difficult == "hard")
        {
            carSpeed = -3f;
            maxSpeed = 15f;
        }
    }
}
