using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {
    private Vector3 startPosition;
    private Vector3 endPosition;

    private GameObject arCamera;
    private GameObject camera;

    private bool moving = false;
    private float movingSpeed = 0.15F;
    private float progressMoving = 0.0F;
    private float intensity = 1F;
    private float progress;

    float distance;
    bool dragging = false;

    // Use this for initialization
    void Start () {
        Debug.Log(transform.position);
        arCamera = GameObject.Find("ARCamera");
        camera = GameObject.Find("Camera");
    }
	
	// Update is called once per frame
	void Update () {
        /*if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = Vector3.Lerp(this.transform.position, rayPoint, Time.deltaTime);
        }*/

        //Debug.Log("ar " + arCamera.transform.position);
        //Debug.Log("cam " + camera.transform.position);
        //Debug.Log(transform.position);
        if (moving)
        {
            processMovement();
        }
    }

    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);

        startPosition = rayPoint;
        //dragging = true;
    }

    void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);

        endPosition = rayPoint;
        //dragging = false;

        moving = true;
    }

    private void processMovement()
    {
        
        progress = progressMoving * movingSpeed * intensity;
        transform.position = Vector3.Lerp(startPosition, endPosition, progress);

        Debug.Log(progress);
        progressMoving += 0.1F;

        if (progress >= 1.0F)
        {
            //transform.parent = otherTarget;
            //swap(ref currentTarget, ref otherTarget);

            moving = false;
            progressMoving = 0.0F;
            progress = 0.0F;
        }
    }

    /*void OnMouseDown()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                Debug.Log("fwf" + touch.position);
                Debug.Log("wqfpq" + transform.position);
                Debug.Log("wwwq" + Camera.main.ScreenToWorldPoint(touch.position));
                Debug.Log("cx" + Camera.main.WorldToScreenPoint(transform.position));
                startPosition = touch.position;
                Debug.Log("touched");
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("a" + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("b " + hit.point);
            }/

            startPosition = Input.mousePosition;
            Debug.Log("mouse down");
        }
        //Debug.Log(startPosition);
    }

    void OnMouseUp()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Ended) //check for the first touch
            {
                Debug.Log("22fwf" + touch.position);
                Debug.Log("22wqfpq" + transform.position);
                Debug.Log("22wwwq" + Camera.main.ScreenToWorldPoint(touch.position));
                Debug.Log("22cx" + Camera.main.WorldToScreenPoint(transform.position));
                endPosition = touch.position;
                moving = true;
                Debug.Log("touch release");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //endPosition = Input.mousePosition;
            //endPosition = 
            //endPosition.y = startPosition.y;
            //endPosition.z = startPosition.z;
            
            Debug.Log("xx " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("c " + hit.point);
            }
            moving = true;

            Debug.Log("mouse release");
        }

        //this.GetComponent<Rigidbody>().velocity += this.transform.forward * 1;
        

        //Debug.Log(endPosition);
    }*/

    /*void OnMouseUp()
    {
        this.GetComponent<Rigidbody>().velocity += this.transform.forward * 5;
    }*/
}
