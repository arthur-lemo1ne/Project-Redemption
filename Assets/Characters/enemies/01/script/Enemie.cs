using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{

    public float Life;
    public float Damage;

    public Transform PlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        PlayerPosition = GameObject.Find("Player").transform;
        Vector3 TargetDirection = PlayerPosition.position - transform.position;
        float angle = (Vector3.Angle(TargetDirection, transform.forward));
        float distance = Vector3.Distance(PlayerPosition.position, transform.position);

        
        if (angle < 80f && angle > -80f && distance < 30f )
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, TargetDirection, out hit);
            if (hit.transform.CompareTag("Player"))
            {
                transform.LookAt(PlayerPosition);
                Debug.Log("shooting player");
            }
        }

        if (Life < 0)
        {
            Life = 0;
            Death();
        }
    }

    public void TakeDamage(float x)
    {
        Life -= x;
    }

    void Death()
    {
        Destroy(gameObject);
    }

}
