using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandller : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;

    int currentSceneIndex;
    int nextSceneIndex;
    bool isTransitioning = false;
    bool collisionDiisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDiisabled = !collisionDiisabled; // toggle collision
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDiisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("this this is friendly");
                break;
            case "Finish":
                Debug.Log("Congrats, yo, you finished!");
                StartSuccessSeaquence();
                break;
            default:
                Debug.Log("Sorry, you brew up");
                StartCrashSeaquence();
                break;
        }
    }

    void StartSuccessSeaquence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartCrashSeaquence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
