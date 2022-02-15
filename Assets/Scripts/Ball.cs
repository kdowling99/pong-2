using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speedMultiplier = 1.1f;
    public float offsetMultiplier = 5f;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    public AudioSource soundPlayer;
    public AudioClip paddleSound;
    public AudioClip wallSound;
    public AudioClip scoreSound;

    public GameObject mostRecentlyTouchedPaddle;
    private Vector3 paddleSizeIncrease = new Vector3(0, 0, 1);
    public TextMeshProUGUI scoreText;
    public Rigidbody rb;
    public Transform t;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        soundPlayer = GetComponent<AudioSource>();
        
        rb.velocity = new Vector3(20, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (t.position.x > 26)
        {
            playerOneScore++;
            soundPlayer.clip = scoreSound;
            soundPlayer.Play();
            scoreText.SetText($"{playerOneScore} - {playerTwoScore}");
            mostRecentlyTouchedPaddle = null;
            if (playerOneScore > 10)
            {
                t.SetPositionAndRotation(Vector3.zero, new Quaternion(0,0,0, 0));
                rb.velocity = new Vector3(0, 0, 0);
                scoreText.color = Color.yellow;
            }
            else
            {
                t.SetPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));
                rb.velocity = new Vector3(20, 0, 0);
            }
        } else if (t.position.x < -26)
        {
            playerTwoScore++;
            soundPlayer.clip = scoreSound;
            soundPlayer.Play();
            scoreText.SetText($"{playerOneScore} - {playerTwoScore}");
            mostRecentlyTouchedPaddle = null;
            if (playerTwoScore > 10)
            {
                t.SetPositionAndRotation(Vector3.zero, new Quaternion(0,0,0, 0));
                rb.velocity = new Vector3(0, 0, 0);
                scoreText.color = Color.yellow;
            }
            else
            {
                t.SetPositionAndRotation(Vector3.zero, new Quaternion(0, 0, 0, 0));
                rb.velocity = new Vector3(-20, 0, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Left Paddle")
        {
            Vector3 direction = new Vector3(1,0,((collision.gameObject.GetComponent<Transform>().position.z - transform.position.z) / -2.5f)); //max angle 45
            float initialMagnitude = rb.velocity.magnitude;
            rb.velocity = (direction / direction.magnitude) * initialMagnitude * speedMultiplier;
            soundPlayer.clip = paddleSound;
            soundPlayer.Play();
            mostRecentlyTouchedPaddle = collision.gameObject;
        } else if (collision.gameObject.name == "Right Paddle")
        {
            Vector3 direction = new Vector3(-1,0,((collision.gameObject.GetComponent<Transform>().position.z - transform.position.z) / -2.5f)); //max angle 45
            float initialMagnitude = rb.velocity.magnitude;
            rb.velocity = (direction / direction.magnitude) * initialMagnitude * speedMultiplier;
            soundPlayer.clip = paddleSound;
            soundPlayer.Play();
            mostRecentlyTouchedPaddle = collision.gameObject;
        } else if (collision.gameObject.name == "Upper Wall" || collision.gameObject.name == "Lower Wall")
        {
            soundPlayer.clip = wallSound;
            soundPlayer.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        mostRecentlyTouchedPaddle.transform.localScale += paddleSizeIncrease;
    }
}

