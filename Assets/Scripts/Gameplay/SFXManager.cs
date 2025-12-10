using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource wilhelmScream;
    public AudioSource cashRegister;

    void OnEnable()
    {
        EventManager.onUpgradePurchased += PlayUpgradePurchaseSound;
    }
    void OnDisable()
    {
        EventManager.onUpgradePurchased -= PlayUpgradePurchaseSound;
    }

    private void PlayUpgradePurchaseSound(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.Gold)
        {
            if (cashRegister != null)
                cashRegister.Play();
        }
        else
        {
            if (wilhelmScream != null)
                wilhelmScream.Play();
        }
    }
}