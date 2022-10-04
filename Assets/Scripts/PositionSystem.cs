using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionSystem : MonoBehaviour
{
    public static PositionSystem instance { get; private set; }


    public GameObject Cp;
    public GameObject CheckpointsHolder;


    public GameObject[] Cars;
    public Transform[] CheckpointPositions;
    public GameObject[] CheckpointForEachCar;

    private int totalCars;
    private int totalCheckpoints;


    public TMP_Text PositionText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartPositioning()
    {
        totalCars = Cars.Length;
        totalCheckpoints = CheckpointsHolder.transform.childCount;

        SetCheckpoint();
        SetCarPosition();

        Cars[0].transform.GetChild(5).GetComponent<CarCPManager>().CarPosition = totalCars;
        PositionText.text = "POS " + Cars[0].transform.GetChild(5).GetComponent<CarCPManager>().CarPosition + "/" + totalCars;
    }

    void SetCheckpoint()
    {
        CheckpointPositions = new Transform[totalCheckpoints];

        for (int i = 0; i < totalCheckpoints; i++)
        {
            CheckpointPositions[i] = CheckpointsHolder.transform.GetChild(i).transform;
        }

        CheckpointForEachCar = new GameObject[totalCars];

        for (int i = 0; i < totalCars; i++)
        {
            CheckpointForEachCar[i] = Instantiate(Cp, CheckpointPositions[0].position, CheckpointPositions[0].rotation);
            CheckpointForEachCar[i].name = "CP_Car_AI" + i.ToString();
            CheckpointForEachCar[i].layer = 6 + i;


            Cars[i].transform.GetChild(5).GetComponent<CarCPManager>().maxCheckpoint = CheckpointsHolder.transform.childCount;

        }


    }

    void SetCarPosition()
    {
        for (int i = 0; i < totalCars; i++)
        {
            Cars[i].transform.GetChild(5).GetComponent<CarCPManager>().CarPosition = i + 1;
            Cars[i].transform.GetChild(5).GetComponent<CarCPManager>().CarNumber = i;
        }

        PositionText.text = "POS " + Cars[0].transform.GetChild(5).GetComponent<CarCPManager>().CarPosition + "/" + totalCars;
    }

    public void CarCollectedCp(int carNumber, int cpNumber)
    {
        CheckpointForEachCar[carNumber].transform.position = CheckpointPositions[cpNumber].transform.position;
        CheckpointForEachCar[carNumber].transform.rotation = CheckpointPositions[cpNumber].transform.rotation;

        ComparePosition(carNumber);
    }



    void ComparePosition(int carNumber)
    {
        //if car isn't at first place already
        if (Cars[carNumber].transform.GetChild(5).GetComponent<CarCPManager>().CarPosition > 1)
        {
            GameObject currentCar = Cars[carNumber];
            int currentCarPos = currentCar.transform.GetChild(5).GetComponent<CarCPManager>().CarPosition;
            int currentCarCP = currentCar.transform.GetChild(5).GetComponent<CarCPManager>().cpCrossed;
            int currentCarLap = currentCar.transform.GetChild(5).GetComponent<CarCPManager>().currentLap;

            GameObject carInFront = null;
            int carInFrontPos = 0;
            int carInFrontCp = 0;
            int carInFrontLap = 0;

            for (int i = 0; i < totalCars; i++)
            {
                if (Cars[i].transform.GetChild(5).GetComponent<CarCPManager>().CarPosition == currentCarPos - 1)  //car in front
                {
                    carInFront = Cars[i];
                    carInFrontCp = carInFront.transform.GetChild(5).GetComponent<CarCPManager>().cpCrossed;
                    carInFrontPos = carInFront.transform.GetChild(5).GetComponent<CarCPManager>().CarPosition;
                    carInFrontLap = carInFront.transform.GetChild(5).GetComponent<CarCPManager>().currentLap;
                    break;
                }
            }

            //this car has crossed the car in front
            if (currentCarCP > carInFrontCp && currentCarLap >= carInFrontLap)
            {


                currentCar.transform.GetChild(5).GetComponent<CarCPManager>().CarPosition = currentCarPos - 1;
                carInFront.transform.GetChild(5).GetComponent<CarCPManager>().CarPosition = carInFrontPos + 1;

            }

            PositionText.text = "POS " + Cars[0].transform.GetChild(5).GetComponent<CarCPManager>().CarPosition + "/" + totalCars;
        }
    }


    public void SetUIText()
    {
        PositionText = GameObject.Find("Text - Pos").GetComponent<TMP_Text>();


    }


    public void SetCarsAtRuntime(GameObject[] aicars)
    {
        for (int i = 0; i < aicars.Length; i++)
        {
            Cars[i+1] = aicars[i];
            
        }



    }
}
