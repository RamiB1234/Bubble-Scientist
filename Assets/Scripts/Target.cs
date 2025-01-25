using UnityEngine;

public class Target : MonoBehaviour
{
    public AudioSource correctSFX;

    public bool targetReached = false;
    private GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        correctSFX.Play();
        targetReached = true;
        StartCoroutine(GameManager.GetComponent<GameManager>().Next());
    }
}
