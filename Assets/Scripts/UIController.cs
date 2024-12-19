using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image shotgunCooldownBar;
    [SerializeField] private Image momentumBar;

    // References to player or relevant controllers to get data
    private ShotgunController shotgunController;
    private PlayerMomentum playerMomentum; // hypothetically where momentum is tracked

    void Start()
    {
        // Assuming these controllers are on the player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            shotgunController = player.GetComponent<ShotgunController>();
            playerMomentum = player.GetComponent<PlayerMomentum>(); // Or however you track momentum
        }
    }

    void Update()
    {
        UpdateShotgunCooldownUI();
        UpdateMomentumUI();
    }

    void UpdateShotgunCooldownUI()
    {
        if (shotgunController != null && shotgunCooldownBar != null)
        {
          
            float cooldownFraction = shotgunController.GetCooldownProgress(); 
          
            shotgunCooldownBar.fillAmount = cooldownFraction; 
        }
    }

    void UpdateMomentumUI()
    {
        if (playerMomentum != null && momentumBar != null)
        {
            // Suppose playerMovement returns a momentum value between 0 and maxMomentum
            float momentumValue = playerMomentum.GetMomentumValue();
            float maxMomentum = playerMomentum.GetMaxMomentum();

            float fraction = momentumValue / maxMomentum;
            fraction = Mathf.Clamp01(fraction); // ensure between 0 and 1
            momentumBar.fillAmount = fraction;
        }
    }
}
