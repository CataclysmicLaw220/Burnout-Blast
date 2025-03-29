using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 6;
    private int currentHealth;

    public HealthBar healthBar; // Reference to the HealthBar script

    void Start() {
        currentHealth = maxHealth;
        if (healthBar != null) {
            healthBar.UpdateHealthBar(currentHealth);
        }
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        Debug.Log("Player Health: " + currentHealth);

        if (healthBar != null) {
            healthBar.UpdateHealthBar(currentHealth);
        }

        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Player Died! Returning to Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }
}
