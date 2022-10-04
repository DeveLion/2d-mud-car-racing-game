using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarReset : MonoBehaviour
{

    CarController carController;

    bool canReset = true;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(transform.localRotation.eulerAngles.z);

        if (transform.localRotation.eulerAngles.z >= 60f && transform.localRotation.eulerAngles.z <= 100f && canReset)
        {
            canReset = false;
            carController.canMove = false;
            ResetCar();
        }
    }


    private void ResetCar()
    {
        transform.eulerAngles = new Vector3(0,0,0);
        transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(2f);
        carController.canMove = true;
        canReset = true;
        StopCoroutine(Timer());
    }
}
