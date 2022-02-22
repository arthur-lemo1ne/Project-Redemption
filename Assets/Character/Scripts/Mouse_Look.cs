using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{

    public float Mouse_Sensitivity = 100f;

    public Transform Player_Body;

    float XRotation = 0f;

       
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float Mouse_X = Input.GetAxis("Mouse X") * Mouse_Sensitivity * Time.deltaTime;
        float Mouse_Y = Input.GetAxis("Mouse Y") * Mouse_Sensitivity * Time.deltaTime;

        XRotation -= Mouse_Y;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);


        Player_Body.Rotate(Vector3.up * Mouse_X);
    }
}
