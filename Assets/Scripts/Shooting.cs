using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject bullet;
    public GameObject target;
    public bool canShoot = false;
    public bool canPlayeShoot = false;

     
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerToStartShoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AI" && canPlayeShoot)
        {
            //Debug.Log("can shoot missile");
            canShoot = true;
            target = collision.transform.parent.gameObject;
            target.transform.GetChild(7).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AI" && canPlayeShoot)
        {
            target.transform.GetChild(7).gameObject.SetActive(false);
            canShoot = false;
            target = null;
            
        }
    }
    public void Shoot()
    {
        if (canPlayeShoot && canShoot && target != null && target.transform.position.x > transform.position.x)
        {
            Debug.Log("shoot missile");
            canShoot = false;
            GameObject go = Instantiate(bullet,transform.position, Quaternion.identity);
            go.GetComponent<HomingMissile>().target = target.transform;
            target = null;
        }
    }


    IEnumerator TimerToStartShoot()
    {
        yield return new WaitForSeconds(5f);
        canPlayeShoot = true;
        StopCoroutine(TimerToStartShoot());
    }
}
