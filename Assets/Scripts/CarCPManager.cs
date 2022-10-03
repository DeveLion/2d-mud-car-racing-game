using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCPManager : MonoBehaviour
{
    public int CarNumber;
    public int cpCrossed = 0;
    public int CarPosition;

    public PositionSystem positionSystem;

    public bool canPass = true;

    public int maxCheckpoint;

    public int currentLap;
    string carName;
    private void Start()
    {
        carName = gameObject.tag;

        positionSystem = GameObject.FindObjectOfType<PositionSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("CP"))
        {
           
            if (gameObject.layer == collision.gameObject.layer)
            {
                
                if (canPass)
                {

                    canPass = false;
                    if (cpCrossed < maxCheckpoint - 1)
                    {
                        cpCrossed += 1;
                    }
                    else
                    {
                        cpCrossed = 0;
                        currentLap += 1;
                    }

                    ///cpCrossed += 1;
                    positionSystem.CarCollectedCp(CarNumber, cpCrossed);
                }
            }


            /*
            if (carName == "Player" && other.gameObject.layer != 7)
            {

            }
            else
            {
                if (canPass)
                {
                    canPass = false;
                    if (cpCrossed < maxCheckpoint - 1)
                    {
                        cpCrossed += 1;
                    }
                    else
                    {
                        cpCrossed = 0;
                        currentLap += 1;
                    }

                    ///cpCrossed += 1;
                    positionSystem.CarCollectedCp(CarNumber, cpCrossed);
                }
            }*/



        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CP"))
        {
            if (!canPass)
            {

                canPass = true;

            }


        }
    }
}
