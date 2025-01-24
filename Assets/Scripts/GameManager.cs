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

    public GameObject max;
    public GameObject min;

    public int percentage = 50;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI percentageText;
    private TextMeshProUGUI substanceText;
    private Image substanceColor;

    private GameObject target;
    private int score = 0;

    // Array of substance names
    private string[] substances = new string[]
    {
        "Zorium", "Veltraxium", "Lunorite", "Aetherium", "Kryzium", "Solenite", "Dracontium",
        "Neotexium", "Plasmonite", "Quintarion", "Xalorine", "Mytherium", "Thalvium", "Obscurium",
        "Pyranite", "Xenotral", "Flaridium", "Crysalon", "Syntheton", "Orbexium", "Vylantium",
        "Morbidium", "Phasium", "Helidonium", "Gravonix", "Optarion", "Ferelium", "Glacionite",
        "Radionyx", "Electranium", "Chronexium", "Vulcanite", "Aquarion", "Tremarix", "Seraphite",
        "Arkanium", "Zyntheral", "Ecliptium", "Blazium", "Voidite", "Prismatium", "Nexarite",
        "Fluxorium", "Solarion", "Omnidium", "Clastium", "Pulsarite", "Umbraxium", "Frozium",
        "Ionix", "Ignatrite", "Luminite", "Kalthorium", "Vyrantium", "Nexalon", "Ceridite",
        "Zenithium", "Crysium", "Phanthonium", "Stratonite", "Pyrithal", "Biocronium", "Glimphorite",
        "Quarium", "Tekronium", "Aurorite", "Alvionium", "Thermexium", "Fractonix", "Requium",
        "Spectrium", "Shynex", "Krythonite", "Cobaltrium", "Zyntharion", "Phraxium", "Protiumite",
        "Arbitrite", "Magnorite", "Silvium", "Cloronium", "Exolithium", "Pyronox", "Deltanite",
        "Eridion", "Cyclantrium", "Etheron", "Halonix", "Glowium", "Vesperium", "Arcitium",
        "Zephorium", "Lumathor", "Cryonex", "Astranite", "Velonix", "Nebulite", "Aurion",
        "Zyphorium", "Novalon"
    };

    private string currentSubstance; // Store the current substance name

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomPercentage();
        scoreText = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        percentageText = GameObject.FindWithTag("Percentage").GetComponent<TextMeshProUGUI>();
        substanceText = GameObject.FindWithTag("SubstanceName").GetComponent<TextMeshProUGUI>();
        substanceColor = GameObject.FindGameObjectWithTag("SubstanceColor").GetComponent<Image>();
        
        target = GameObject.FindWithTag("Target");

        SetRandomSubstance(); // Initialize the first substance

        StartCoroutine(CheckLoseCondition());

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        percentageText.text = percentage+"%";
        substanceText.text = "Substance: " + currentSubstance;
    }

    private void RandomPercentage()
    {
        int newPercentage;
        do
        {
            // Generate a random multiple of 5 between 5 and 50
            newPercentage = UnityEngine.Random.Range(1, 11) * 5; // 1 to 10 multiplied by 5
        }
        while (newPercentage == percentage); // Ensure it's not the same as the last value

        percentage = newPercentage; // Assign the new value
    }

    private void SetRandomSubstance()
    {
        // Pick a random substance
        int randomIndex = UnityEngine.Random.Range(0, substances.Length);
        currentSubstance = substances[randomIndex];

        // Generate a random color
        Color randomColor = new Color(
            UnityEngine.Random.Range(0f, 1f), // Red
            UnityEngine.Random.Range(0f, 1f), // Green
            UnityEngine.Random.Range(0f, 1f)  // Blue
        );

        // Update the SubstanceColor UI image
        substanceColor.color = randomColor;

        substance.GetComponent<SpriteRenderer>().color = randomColor;
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

        // Randomize target position within bounds
        float minY = min.transform.position.y;
        float maxY = max.transform.position.y;
        float randomY = UnityEngine.Random.Range(minY, maxY);

        Vector3 targetPosition = target.transform.position;
        targetPosition.y = randomY;
        target.transform.position = targetPosition;

        // Reset bubble mask & remove collider:
        var bubbleScale = bubbleMask.transform.localScale;
        bubbleScale.y = 0;
        bubbleMask.transform.localScale = bubbleScale;
        var bubbleCollider = bubbleMask.GetComponent<BoxCollider2D>();
        Destroy(bubbleCollider);

        RandomPercentage();
        SetRandomSubstance(); // Set a new random substance and color for the next level
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
