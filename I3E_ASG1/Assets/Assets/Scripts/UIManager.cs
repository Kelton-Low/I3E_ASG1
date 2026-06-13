using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject DiePanel;
    [SerializeField] private TMP_Text DieScore;
    [SerializeField] private TMP_Text HintText;
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] private Image scoreBar;
    [SerializeField] private GameObject congratulationsText;


    [Header("Variables")] 

    private float lerpTimer = 0;
    [SerializeField] private float chipSpeed = 1f;

    public void ToggleMenuPanel()
    {
        //sets the menu
        MenuPanel.SetActive(!MenuPanel.activeSelf);
        Cursor.visible = MenuPanel.activeSelf;
        Cursor.lockState = MenuPanel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void UpdateScore(float score, float maxScore)
    {
        //Changes the score and the xp bar so that the player can say their xp bar is low
        ScoreText.text = $"Score: {score}/{maxScore}";
        scoreBar.fillAmount = Mathf.Clamp(score / maxScore, 0, 1);
        if(score >= maxScore)
        {
            congratulationsText.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void ShowDiePanel(float score)
    {
        //Lets the player die
        DiePanel.SetActive(true);
        DieScore.text = $"Score: {score}";
        Cursor.visible = DiePanel.activeSelf;
        Cursor.lockState = DiePanel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }
    public void ShowHintingText(string hintString)
    {
        //Show hints to the player
        HintText.text = hintString;
    }
    public void UpdateHealthUI(float health, float maxHealth)
    {
        //Change the health bar when taking damage
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }

}
