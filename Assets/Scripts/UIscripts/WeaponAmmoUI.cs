using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponNameText;
    [SerializeField] TextMeshProUGUI currentBulletCountText;
    [SerializeField] TextMeshProUGUI totalBulletcountText;

    [SerializeField] WeaponComponent weaponComponent;

    /// <summary>
    /// set up events for on weapon equipped to handle the weapon component we grab
    /// </summary>

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    private void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentBulletCountText.text = weaponComponent.weaponStats.bulletsInClip.ToString();
        totalBulletcountText.text = weaponComponent.weaponStats.totalBullets.ToString();
    }
}