using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class WeaponControler : MonoBehaviour
{
    [SerializeField] private Transform _weaponMeshPivotTarget;
    [SerializeField] private Transform _weaponParent;
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private WeaponTimerShot _uiWeapon;

    [SerializeField] private GameEvent _secondaryAttackEvent;
    
    private int _currentWeaponIndex = 0;

    private InputAction _primaryAttackInputAction;
    private InputAction _secondaryAttackInputAction;
    private bool _isPrimaryAttacking;
    private bool _isSecondaryAttacking;
    public bool IsPrimaryAttacking { get => _isPrimaryAttacking; }
    public bool IsSecondaryAttacking { get => _isSecondaryAttacking; }
    private Transform _currentWeaponMesh;
    private PlayerAnimation _playerAnimation;

    private void Start()
    {
        _primaryAttackInputAction = GetComponent<PlayerInput>().actions.FindAction("Attack");
        _secondaryAttackInputAction = GetComponent<PlayerInput>().actions.FindAction("SecondaryAttack");

        _playerAnimation = GetComponent<PlayerAnimation>();
        
        Debug.Assert(_uiWeapon != null, "UI Weapon is not set in WeaponControler on Player");

        GetAllChildWeapon();
    }

    private void Update()
    {
        _isPrimaryAttacking = _primaryAttackInputAction.ReadValue<float>() > .5f;
        _isSecondaryAttacking = _secondaryAttackInputAction.ReadValue<float>() > .5f;

        if(_isSecondaryAttacking) _secondaryAttackEvent.Raise();
        
        _playerAnimation.IsPlayerShooting = IsShooting();

        if (_currentWeaponMesh)
        {
            _currentWeaponMesh.position = _weaponMeshPivotTarget.position;
            _currentWeaponMesh.forward = -_weaponMeshPivotTarget.forward;
        }
    }

    public bool IsShooting()
    {
        return _isPrimaryAttacking || _isSecondaryAttacking;
    }

    private void EnableWeapon(int index)
    {
        foreach (var item in _weaponList)
            item.gameObject.SetActive(false);

        _weaponList[index].gameObject.SetActive(true);
        _uiWeapon.InitializeWeapon(_weaponList[index]);
        _currentWeaponMesh = _weaponList[index].MeshTransform;

        _playerAnimation.SetWeaponAnimation(_weaponList[index].AnimationState);
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

    public void AddGatling(GameObject tempObject)
    {
        Weapon tempWeapon = tempObject.GetComponent<Weapon>();
        AddWeapon(tempWeapon);
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        Weapon newWeapon = Instantiate(weaponToAdd, _weaponParent);

        newWeapon.Initialize(this);

        _weaponList.Add(newWeapon);
        EnableWeapon(_weaponList.Count - 1);
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
