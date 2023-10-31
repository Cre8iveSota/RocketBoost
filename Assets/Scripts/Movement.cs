using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrust = 1.0f;
    [SerializeField] float thrustTurn = 100.0f;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            NewMethod(thrustTurn);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            NewMethod(-thrustTurn);
        }
    }

    private void NewMethod(float thrustThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * thrustThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
