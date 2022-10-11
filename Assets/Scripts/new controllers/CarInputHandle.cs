using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandle : MonoBehaviour
{

    //components
    CarControllerNew carController;


    public bool isPlayer;

    public bool canMove = true;

    public Shooting shooting;

    private void Awake()
    {
        carController = GetComponent<CarControllerNew>();
    }

    // Start is called before the first frame update
    void Start()
    {


        CheckOwner();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;


        if (isPlayer)
        {
            if (canMove)
            {
                inputVector.x = Input.GetAxis("Horizontal");
                inputVector.y = 0;
            }

        }
        else
        {
            if (canMove)
            {
                inputVector.x = 1;
                inputVector.y = 0;
            }

        }



        carController.SetInputVector(inputVector);


        Shoot();
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

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.tag == "Player")
        {
            shooting.Shoot();
        }
    }
}
