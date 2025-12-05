using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public HealthSystem enemyHealth;
    public Slider healthSlider;
    public Transform targetToLook; // Usually the camera

    void Start()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnHealthChanged += UpdateBar;
            UpdateBar(enemyHealth.currentHealth, enemyHealth.maxHealth);
        }

        if (targetToLook == null)
            targetToLook = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Always look at camera
        transform.LookAt(transform.position + targetToLook.rotation * Vector3.forward,
                         targetToLook.rotation * Vector3.up);
    }

    void UpdateBar(float current, float max)
    {
        healthSlider.value = current / max;
    }
}
