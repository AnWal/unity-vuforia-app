using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour {
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 originalPosition;

    private Rigidbody rb;

    private GameObject arCamera;
    private GameObject camera;
    public Slider slider;

    private bool moving = false;
    private float movingSpeed = 0.15F;
    private float progressMoving = 0.0F;
    private float intensity = 1F;
    private float progress;
    private float progressTotal;
    private float upFactor = 300F;
    private float speedFactor = 100F;
    private float startDragtime;

    private float maxX = 8.5F;
    private float maxZ = 5F;

    float distance;
    bool dragging = false;

    // Use this for initialization
    void Start () {
        Debug.Log(transform.position);
        arCamera = GameObject.Find("ARCamera");
        camera = GameObject.Find("Camera");
        rb = transform.GetComponent<Rigidbody>();

        originalPosition = transform.position;
        
    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        startDragtime = Time.time;
    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
        
        dragging = false;
        moving = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        endPosition = rayPoint;
        endPosition.y = originalPosition.y;
        
        float dragPower = calculateDragPower();
        rb.AddForce(new Vector3(1, 0, 0) * (endPosition.x - startPosition.x)  * dragPower * speedFactor);
        rb.AddForce(new Vector3(0, 0, 1) * (endPosition.z - startPosition.z)  * dragPower * speedFactor);
        rb.AddForce(new Vector3(0, 1, 0) * slider.value * upFactor);
    }

    private float calculateDragPower()
    {
        float timeDiff = Time.time - startDragtime;

        Debug.Log("timeDiff: " + timeDiff);
        
        // TODO bessere Berechnung
        if (timeDiff <= 2)
        {
            return 1F;
        }
        else
        {
            return 0.5F;
        }
    }

    private void Update()
    {

        if (transform.position.y <= -10)
        {
            // TODO animationclip für spawning

            // reset motion
            rb.velocity = new Vector3(0,0,0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            rb.useGravity = false;

            // calculate new position
            resetPosition();

            moving = false;
        }
    }

    private void resetPosition()
    {
        Debug.Log("orig: " + originalPosition);

        Vector3 newPosition = new Vector3(UnityEngine.Random.Range(-maxX, maxX), 0.7F, UnityEngine.Random.Range(-maxZ, maxZ));
        newPosition.y = originalPosition.y;

        transform.position = newPosition;
        Debug.Log("new: " + newPosition);
        startPosition = newPosition;
    }
}
