using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject bullet;
    public GameObject target;
    public bool canShoot = false;

     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AI")
        {
            Debug.Log("can shoot missile");
            canShoot = true;
            target = collision.transform.parent.gameObject;
            target.transform.GetChild(7).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AI")
        {
            target.transform.GetChild(7).gameObject.SetActive(false);
            canShoot = false;
            target = null;
            
        }
    }
    public void Shoot()
    {
        if (canShoot && target != null && target.transform.position.x > transform.position.x)
        {
            Debug.Log("shoot missile");
            canShoot = false;
            GameObject go = Instantiate(bullet,transform.position, Quaternion.identity);
            go.GetComponent<HomingMissile>().target = target.transform;
            target = null;
        }
    }
}
