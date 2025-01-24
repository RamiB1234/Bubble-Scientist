using UnityEngine;

public class Target : MonoBehaviour
{
    public bool targetReached = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Yes");
        targetReached = true;
    }
}
