using UnityEngine;

public class HealthBar : MonoBehaviour {
    public SpriteRenderer healthBarRenderer;
    public Sprite[] healthSprites;

    public void UpdateHealthBar(int currentHealth) {
        if (healthBarRenderer == null) {
            Debug.LogError("HealthBar SpriteRenderer is missing!");
            return;
        }

        if (currentHealth < 0 || currentHealth >= healthSprites.Length) {
            Debug.LogError("Invalid health value: " + currentHealth);
            return;
        }

        healthBarRenderer.sprite = healthSprites[currentHealth];
    }
}
