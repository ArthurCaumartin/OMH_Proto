using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControler : MonoBehaviour
{
    [SerializeField] private Transform _weaponPivot;
    [SerializeField] private List<Weapon> _weaponList;
    private int _currentWeaponIndex = 0;

    private InputAction _primaryAttackInputAction;
    private InputAction _secondaryAttackInputAction;
    private bool _isPrimaryAttacking;
    private bool _isSecondaryAttacking;
    public bool IsPrimaryAttacking { get => _isPrimaryAttacking; }
    public bool IsSecondaryAttacking { get => _isSecondaryAttacking; }

    private void Start()
    {
        _primaryAttackInputAction = GetComponent<PlayerInput>().actions.FindAction("Attack");
        _secondaryAttackInputAction = GetComponent<PlayerInput>().actions.FindAction("SecondaryAttack");

        GetAllChildWeapon();
    }

    private void Update()
    {
        _isPrimaryAttacking = _primaryAttackInputAction.ReadValue<float>() > .5f;
        _isSecondaryAttacking = _secondaryAttackInputAction.ReadValue<float>() > .5f;
    }

    public bool IsPlayerShooting()
    {
        return _isPrimaryAttacking || _isSecondaryAttacking;
    }

    private void EnableWeapon(int index)
    {
        foreach (var item in _weaponList)
            item.gameObject.SetActive(false);

        _weaponList[index].gameObject.SetActive(true);
    }

    private void SwapWeapon(int swapDirection)
    {
        if (swapDirection == 0) return;

        _currentWeaponIndex = _currentWeaponIndex + (swapDirection > 0 ? 1 : -1);
        if (_currentWeaponIndex < 0) _currentWeaponIndex = 0;
        if (_currentWeaponIndex > _weaponList.Count - 1) _currentWeaponIndex = _weaponList.Count - 1;

        EnableWeapon(_currentWeaponIndex);
    }

    public void OnWeaponSwap(InputValue value)
    {
        //? scroll axis return 120 or 0 or -120 ???
        //? return angle delta
        SwapWeapon((int)value.Get<float>());
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        // if (!weaponToAdd || !_weaponList.Contains(weaponToAdd)) return;
        Weapon newWeapon = Instantiate(weaponToAdd, _weaponPivot);
        newWeapon.Initialize(this);
        _weaponList.Add(newWeapon);
        EnableWeapon(_weaponList.Count - 1);
        // print("weapon grab and select");
    }

    public void RemoveWeapon(Weapon weaponToRemove)
    {
        _weaponList.Remove(weaponToRemove);
        EnableWeapon(0);
    }

    private void GetAllChildWeapon()
    {
        _weaponList.Clear();
        foreach (var item in GetComponentsInChildren<Weapon>())
        {
            item.Initialize(this);
            _weaponList.Add(item);
        }
        EnableWeapon(0);
    }
}
