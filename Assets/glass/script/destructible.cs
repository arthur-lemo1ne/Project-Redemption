using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructible : MonoBehaviour
{

    public GameObject DestroyedVersion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        GameObject des = Instantiate(DestroyedVersion, transform.position, transform.rotation);
        des.transform.localScale = gameObject.transform.localScale/100;
        Destroy(gameObject);

        for (int i = 0; i < des.transform.childCount; i++)
        {
            Transform Children = des.transform.GetChild(i);
            Rigidbody rigid = Children.GetComponent<Rigidbody>();
            GameObject gun = GameObject.Find("GunPosition");
            rigid.AddForce(gun.transform.forward * Random.Range(1f,20f));
        }
        Destroy(des, 5f);
    }

}
