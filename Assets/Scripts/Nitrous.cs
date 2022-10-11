using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitrous : MonoBehaviour
{

    public CarController carController;
    public CarControllerNew carControllerNew;
    Animator camAnim;

    public GameObject nitrousEffect;

    


    // Start is called before the first frame update
    void Start()
    {
        camAnim = Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Nitrous")
        {
            
            ActivateNitrous();
            Destroy(collision.gameObject);
        }
    }


    void ActivateNitrous()
    {
        //carController.carSpeed = carController.carSpeed * 2;
        //carController.maxSpeed = carController.maxSpeed + 10;
        carControllerNew.isNitrous = true;
        camAnim.SetBool("Shake", true);
        nitrousEffect.SetActive(true);
        StartCoroutine(DisactivateNitrous());
    }

    IEnumerator DisactivateNitrous()
    {
        yield return new WaitForSeconds(3f);
        //carController.carSpeed = carController.defaultCarSpeed;
        //carController.maxSpeed = carController.defaultMaxSpeed;
        carControllerNew.isNitrous = false;
        camAnim.SetBool("Shake", false);
        nitrousEffect.SetActive(false);
        StopCoroutine(DisactivateNitrous());
    }
}
