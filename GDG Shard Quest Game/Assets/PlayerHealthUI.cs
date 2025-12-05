using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public HealthSystem playerHealth;
    public Slider healthSlider;

    void Start()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += UpdateHealthUI;
            UpdateHealthUI(playerHealth.currentHealth, playerHealth.maxHealth);
        }
    }

    void UpdateHealthUI(float current, float max)
    {
        healthSlider.value = current / max;
    }
}
