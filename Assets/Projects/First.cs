using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class First : MonoBehaviour
{
    public Rigidbody ball;
    public GameObject ballObjectOrigin;
    private GameObject ballObjectInstance;
    public Transform launchPosition;
    public GameObject Launch;
    public Text m_Text;
    private Vector3 launchVector; //(Force)
    private Vector3 launchAngle; //(Angle)

    private float widthPER;
    private float heightPER;
    private float width;
    private float height;
    private float currentAngle;

    private Vector3 tempPosition;

    private bool launch = false;   
    void Start()
    {
        ballObjectInstance = null;
       width=(float)Screen.width;
       height=(float)Screen.height;

        launchVector = new Vector3(0,100f, 500f);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            launch = true;

        /*
        if (Input.GetMouseButtonDown(0))
        {
            ballObjectInstance = Instantiate(ballObjectOrigin, launchPosition.position, launchPosition.transform.rotation);
            // ballObjectInstance.GetComponent<DestroyMe>().UpdateDirection(Launch.transform.up);
            //ballObjectInstance.GetComponent<Rigidbody>().AddForce(Launch.transform.forward * 400f);
            ballObjectInstance.GetComponent<Rigidbody>().AddForce(launchVector);
        }*/
    }
    private void FixedUpdate()
    {
        if(launch==true)
        {
            tempPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 100f));
            ballObjectInstance = Instantiate(ballObjectOrigin, launchPosition.position, launchPosition.transform.rotation);
            ballObjectInstance.transform.LookAt(tempPosition+new Vector3(0,18f,0));
            //ballObjectInstance.GetComponent<Rigidbody>().AddRelativeForce(launchVector, ForceMode.Force);
            ballObjectInstance.GetComponent<Rigidbody>().AddForce(ballObjectInstance.transform.forward * 1000f);
            m_Text.text = "Pos: " + tempPosition;
            launch = false;
        }
        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            currentAngle = (Mathf.Acos(100.0f/(width/2.0f)))*Mathf.Rad2Deg;
            widthPER = (touch.position.x / width) * currentAngle-(currentAngle / 2.0f);
          //  widthPER = (touch.position.y / height)*90-45;
            launchAngle = new Vector3(0, widthPER, 0);
            Launch.transform.eulerAngles = launchAngle;
           
            ballObjectInstance = Instantiate(ballObjectOrigin, launchPosition.position, Launch.transform.rotation);
            ballObjectInstance.GetComponent<Rigidbody>().AddRelativeForce(launchVector);
            m_Text.text = "Pos: " + touch.position + " deg: " + widthPER + "width: " + width + "height:" + height;
         else
        {
            m_Text.text = "No touch contacts";
        }
            */


        /////


        /*
    if (touch.position.x>width/2.0f) launchVector.x = 250f;
    if(touch.position.x<width / 2.0f) launchVector.x = -250f;
    if(touch.position.y>height / 2.0f) launchVector.y = 650f;
    if(touch.position.y<height / 2.0f) launchVector.y = 400f;
   */
        // Debug.Log("y is:" + touch.position.y);

        // Update the Text on the screen depending on current position of the touch each frame

    }





}

