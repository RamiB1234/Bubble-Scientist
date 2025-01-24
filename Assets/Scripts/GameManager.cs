using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pourButton;
    public GameObject substance;
    public GameObject bubbleMask;

    public int percentage = 50;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI percentageText;
    private GameObject target;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomPercentage();
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        percentageText = GameObject.FindWithTag("Percentage").GetComponent<TextMeshProUGUI>();
        target = GameObject.FindWithTag("Target");
        StartCoroutine(CheckLoseCondition());

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        percentageText.text = percentage+"%";
    }

    private void RandomPercentage()
    {
        // Generate a random number between 1 and 10, then multiply by 10 to get multiples of 10.
        percentage=  UnityEngine.Random.Range(1, 11) * 10;
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

        // Reset target state
        target.GetComponent<Target>().targetReached = false;

        // Reset bubble mask & remove collider:
        var bubbleScale = bubbleMask.transform.localScale;
        bubbleScale.y = 0;
        bubbleMask.transform.localScale = bubbleScale;
        var bubbleCollider = bubbleMask.GetComponent<BoxCollider2D>();
        Destroy(bubbleCollider);

        RandomPercentage();
    }

    public IEnumerator CheckLoseCondition()
    {
        while (true)
        {
            // Wait for a short period after pouring ends to give time for a possible win
            yield return new WaitForSeconds(1f);

            if (!target.GetComponent<Target>().targetReached &&
                bubbleMask.TryGetComponent(out BoxCollider2D collider2D))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
