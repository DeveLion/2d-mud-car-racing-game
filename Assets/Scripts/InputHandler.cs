using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    float input;
    bool isPlayer;

    public bool canMove;

    CarController carController;


    // Start is called before the first frame update
    void Start()
    {

        carController = GetComponent<CarController>();

        CheckOwner();
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayer)
        {
            if (canMove) { 
                input = Input.GetAxis("Horizontal"); 
            }

        }
        else
        {
            if (canMove) { 
                input = 1f; 
            }

        }
        Accelerate();
    }

    void CheckOwner()
    {

        if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

    }

    void Accelerate()
    {
        carController.input = input;

    }
}
