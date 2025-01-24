using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pourButton;
    public GameObject substance;
    public GameObject bubbleMask;

    private TextMeshProUGUI scoreText;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void Next()
    {
        score += 100;

        // Activate button
        pourButton.GetComponent<Button>().interactable = true;

        // Reset pouring state
        pourButton.GetComponent<PourButton>().donePouring = false;
        
        // Reset Substance
        var substanceScale = substance.transform.localScale;
        substanceScale.y = 0;
        substance.transform.localScale = substanceScale;

        // Reset bubble mask & remove collider:
        var bubbleScale = bubbleMask.transform.localScale;
        bubbleScale.y = 0;
        bubbleMask.transform.localScale = bubbleScale;
        var bubbleCollider = bubbleMask.GetComponent<BoxCollider2D>();
        Destroy(bubbleCollider);


    }
}
