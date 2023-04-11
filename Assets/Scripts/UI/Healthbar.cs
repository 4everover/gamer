using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    Slider slider;
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject fillArea;
    [SerializeField] Color hpColor; // 30903F
    [SerializeField] Color hpLowColor; // B45045
    [SerializeField] float hpPercentToBeLow = 0.25f;

    Health playerHealth;
    float playerMaxHP;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        playerHealth = FindObjectOfType<PlayerController>().GetComponent<Health>();
        playerMaxHP = playerHealth.GetMaxHealth();
        slider.maxValue = playerMaxHP;
        slider.value = playerHealth.GetHealth();
        healthText.text = "HP: " + playerHealth.GetHealth() + "/" + playerHealth.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerHealth.GetHealth();
        slider.maxValue = playerHealth.GetMaxHealth();
        healthText.text = "HP: " + playerHealth.GetHealth() + "/" + playerHealth.GetMaxHealth();

        if (slider.value <= slider.maxValue * hpPercentToBeLow)
        {
            fillArea.GetComponent<Image>().color = new Color(hpLowColor.r, hpLowColor.g, hpLowColor.b); ;
        }
        else
        {
            fillArea.GetComponent<Image>().color = new Color(hpColor.r, hpColor.g, hpColor.b);
        }
    }
}
