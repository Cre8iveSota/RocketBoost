using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrust = 1.0f;
    [SerializeField] float thrustTurn = 100.0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;


    AudioSource audioSource;
    Rigidbody rb;
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
            StartThrusting();
        }
        else
        {
            StopThrustiong();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rotateleft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }
        else
        {
            StopRotating();
        }
    }


    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    private void StopThrustiong()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void rotateRight()
    {
        ApplyRotation(-thrustTurn);
        if (!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
    }

    private void Rotateleft()
    {
        ApplyRotation(thrustTurn);
        if (!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
    }
    private void StopRotating()
    {
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
    }


    private void ApplyRotation(float thrustThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * thrustThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
