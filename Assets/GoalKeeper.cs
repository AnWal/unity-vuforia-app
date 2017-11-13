using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalKeeper : MonoBehaviour {

    private bool isKicked;  
    public GameObject soccerBall;
    private new Animation animation;
    public bool animate = true;

    public Toggle keeperMoving;
    public Slider speedSlider;

    private Vector3 startPosition;
    float dist = 5f;
    float speed = 2.0f;

    // Use this for initialization
    void Start () {
        animation = GetComponent<Animation>();
        animation["StandSave"].speed = 0.5f;
        animation.Play("StandSave");
        startPosition = transform.position;     
    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(soccerBall.transform.position, transform.position);

        if (keeperMoving.isOn)
        {
            speedSlider.interactable = true;
            speed = speedSlider.value;
            Vector3 temp = startPosition;
            temp.z += dist * Mathf.Sin(Time.time * speed);
            transform.position = temp;
            animate = false;
        } else {
            transform.position = startPosition;
            speedSlider.interactable = false;
        }


        if (distance <=8f && animate)
        {
            transform.position = startPosition;
            int rand = Random.Range(1, 4);
            if (rand == 1)
            {
                animation.Play("StandSave");
                animate = false;
            }
            else if (rand == 2)
            {
                animation["LeftSave"].speed = 2f;
                animation.Play("LeftSave");
                animate = false;
            } else if(rand == 3)
            {
                animation["RightSave"].speed = 2f;
                animation.Play("RightSave");
                animate = false;
            }
        }
	}

}
