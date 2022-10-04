using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject bullet;
    public GameObject target;
    bool canShoot = false;

     
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
    public void Shoot()
    {
        if (canShoot)
        {
            Debug.Log("shoot missile");
            canShoot = false;
            GameObject go = Instantiate(bullet,transform.position, Quaternion.identity);
            go.GetComponent<HomingMissile>().target = target.transform;
        }
    }
}
