using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerController playerController;
    public Sprite crosshairImage;

    Animator PlayerAnimator;
    WeaponComponent equippedWeapon;

    [SerializeField]
    GameObject weaponSocketLocation;
    [SerializeField]
    Transform gripIKSocketLocation;

    bool wasFiring = false;
    bool firingPressed = false;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");
    // Start is called before the first frame update
    void Start()
    {

        playerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();


        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();

        equippedWeapon.Initialize(this);
        gripIKSocketLocation = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
    }
    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            print("Stopping fire");
            StopFiring();
        }
    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0) return;
        PlayerAnimator.SetBool(isFiringHash, true);
        equippedWeapon.StartFiringWeapon();
    }

    public void StopFiring()
    {
        playerController.isFiring = false;
        PlayerAnimator.SetBool(isFiringHash, false);
        equippedWeapon.StopFiringWeapon();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        PlayerAnimator.SetBool(isReloadingHash, playerController.isReloading);
    }

    public void StartReloading()
    {

    }

}