using UnityEngine;

public class Target : MonoBehaviour
{
    public bool targetReached = false;
    private GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        targetReached = true;
        GameManager.GetComponent<GameManager>().score += 100;
    }
}
