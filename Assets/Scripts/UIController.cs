using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image shotgunCooldownBar;
    [SerializeField] private Image momentumBar;

    private ShotgunController shotgunController;
    private PlayerMomentum playerMomentum; 

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            shotgunController = player.GetComponent<ShotgunController>();
            playerMomentum = player.GetComponent<PlayerMomentum>(); 
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
            float momentumValue = playerMomentum.GetMomentumValue();
            float maxMomentum = playerMomentum.GetMaxMomentum();

            float fraction = momentumValue / maxMomentum;
            fraction = Mathf.Clamp01(fraction); 
            momentumBar.fillAmount = fraction;
        }
    }
}
