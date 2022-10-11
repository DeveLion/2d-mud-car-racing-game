using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag  == "Player")
        {
            Debug.Log("finish line");
            GameManager.instance.RaceFinished();
        }
        else if (collision.gameObject.tag == "AI") {

            Debug.Log("ai passfinish line");
            collision.transform.parent.gameObject.GetComponent<CarInputHandle>().canMove = false;
            collision.transform.parent.gameObject.GetComponent<CarControllerNew>().canMove = false;
            //collision.transform.parent.gameObject.GetComponent<CarControllerNew>().StopCar();
            collision.transform.parent.gameObject.GetComponent<CarManager>().ManageCollidersAndRigidbodiesFinish(false);
            collision.transform.parent.gameObject.GetComponent<CarManager>().ResetCar();
        }
    }

    
}
