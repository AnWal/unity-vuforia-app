﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 originalPosition;

    private Rigidbody rb;

    private GameObject arCamera;
    private GameObject camera;
    public Slider slider;

    public bool moving = false;
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

    private float startMovementTime;
    private float resetBallAfterSeconds = 7;

    float distance;
    bool dragging = false;

    public bool triggered = false;
    private int score = 0;
    public GameObject keeper;
    private GoalKeeper goalKeeper;

    // Use this for initialization
    void Start()
    {
        Debug.Log(transform.position);
        arCamera = GameObject.Find("ARCamera");
        camera = GameObject.Find("Camera");
        rb = transform.GetComponent<Rigidbody>();

        originalPosition = transform.position;
        goalKeeper = keeper.GetComponent<GoalKeeper>();


    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        startDragtime = Time.time;
    }

    private void OnMouseUp()
    {
        startMovementTime = Time.time;
        rb.useGravity = true;

        dragging = false;
        moving = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        endPosition = rayPoint;
        endPosition.y = originalPosition.y;

        float dragPower = calculateDragPower();
        rb.AddForce(new Vector3(1, 0, 0) * (endPosition.x - startPosition.x) * dragPower * speedFactor);
        rb.AddForce(new Vector3(0, 0, 1) * (endPosition.z - startPosition.z) * dragPower * speedFactor);
        rb.AddForce(new Vector3(0, 1, 0) * slider.value * upFactor);

        goalKeeper.animate = true;
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
            Debug.Log("reseting after leaving area");

            // calculate new position
            resetPosition();

            moving = false;
        }
        else if (moving && (Time.time - startMovementTime ) > resetBallAfterSeconds)
        {
            Debug.Log("reseting after timeout");
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
        rb.useGravity = false;

        transform.position = newPosition;
        Debug.Log("new: " + newPosition);
        startPosition = newPosition;
        triggered = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        moving = false;
        startMovementTime = 0.0F;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GoalLine" && !triggered)
        {
            Debug.Log("GOAL!!!");
            triggered = true;
            score++;
            StartCoroutine(WaitAndReset());
        } 
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, Screen.width / 5, Screen.height / 6), "SCORE: " + score); 
        if (GUI.Button(new Rect(Screen.width - (Screen.width / 6), 10, Screen.width / 6, Screen.height / 6), "RESTART"))
        {
            triggered = false;
            score = 0;
            resetPosition();
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(2);
        resetPosition();
    }
}
