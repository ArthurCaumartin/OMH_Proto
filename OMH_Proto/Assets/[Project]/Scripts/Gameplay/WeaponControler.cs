using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponControler : MonoBehaviour
{
    [SerializeField] private Transform _weaponMeshPivotTarget;
    [SerializeField] private Transform _weaponParent;
    [SerializeField] private PlayerAim _playerAim;
    [SerializeField] private List<Weapon> _weaponList;
    [SerializeField] private WeaponTimerShot _uiWeapon;

    [SerializeField] private GameEvent _secondaryAttackEvent;

    private int _currentWeaponIndex = 0;
    private List<WeaponIdentifier> _weaponIdentifiers = new List<WeaponIdentifier>();

    private InputAction _primaryAttackInputAction;
    private InputAction _secondaryAttackInputAction;
    private bool _isPrimaryAttacking;
    private bool _isSecondaryAttacking;

    public bool IsPrimaryAttacking { get => _isPrimaryAttacking; }
    public bool IsSecondaryAttacking { get => _isSecondaryAttacking; }
    [Space]
    [Header("Wwise Events")]
    [SerializeField] private AK.Wwise.Event _fugitiveShoot;
    [SerializeField] private AK.Wwise.Event _sobekShoot;
    [SerializeField] private AK.Wwise.Event _gatlingShoot;
    [SerializeField] private AK.Wwise.Event _stopGatling;

    [Header("Wwise RTPC")]
    [SerializeField] private AK.Wwise.RTPC _weaponRTPC;

    [Header("Weapon Config")]
    [SerializeField] private WeaponIdentifier _weaponIdentifier;
    [Space]
    private Transform _currentWeaponMesh;
    private PlayerAnimation _playerAnimation;


    [ContextMenu("Test Weapon Identifiers")]
    private void TestWeaponIdentifiers()
    {
        Debug.Log("----- Weapon Identifiers Test -----");

        for (int i = 0; i < _weaponList.Count; i++)
        {
            string weaponName = _weaponList[i].name;
            string weaponType = _weaponIdentifiers[i] != null ? _weaponIdentifiers[i].weaponType.ToString() : "NULL";

            Debug.Log($"Index {i} | Weapon: {weaponName} | Type: {weaponType}");
        }
    }
    private void Start()
    {
        _primaryAttackInputAction = GetComponent<PlayerInput>().actions.FindAction("Attack");
        _secondaryAttackInputAction = GetComponent<PlayerInput>().actions.FindAction("SecondaryAttack");

        _playerAnimation = GetComponent<PlayerAnimation>();

        // Debug.Assert(_uiWeapon != null, "UI Weapon is not set in WeaponControler on Player");

        GetAllChildWeapon();
    }

    private void Update()
    {
        _isPrimaryAttacking = _primaryAttackInputAction.ReadValue<float>() > .5f;
        _isSecondaryAttacking = _secondaryAttackInputAction.ReadValue<float>() > .5f;

        if (_isSecondaryAttacking) _secondaryAttackEvent.Raise();

        _playerAnimation.IsPlayerShooting = IsShooting();
        AligneWeaponMesh();
    }

    private void AligneWeaponMesh()
    {
        if (!_currentWeaponMesh) return;

        _currentWeaponMesh.position = _weaponMeshPivotTarget.position;

        if (_isPrimaryAttacking | _isSecondaryAttacking)
            _currentWeaponMesh.forward = _playerAim.WorldMouseDirection;
        else
            _currentWeaponMesh.forward = -_weaponMeshPivotTarget.forward;
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
        _uiWeapon?.InitializeWeapon(_weaponList[index]);
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

             #region Posting sounds
    public void FireWeapon()
    {
        if (_weaponIdentifiers.Count <= _currentWeaponIndex)
        {
            Debug.LogWarning($"{name} : WeaponIdentifier introuvable.");
            return;
        }

        WeaponIdentifier currentIdentifier = _weaponList[_currentWeaponIndex].GetComponent<WeaponIdentifier>();

        if (currentIdentifier == null)
        {
            Debug.LogWarning($"{name} : WeaponIdentifier null pour l'arme courante.");
            return;
        }

        SetWeaponRTPC(currentIdentifier.weaponType);
        PostWeaponSound(currentIdentifier.weaponType);
    }
    public void StopWeaponSound() //spécifique pour la gatling
    {
        if (_weaponList.Count <= _currentWeaponIndex)
        {
            Debug.LogWarning($"{name} : WeaponList vide ou index invalide.");
            return;
        }

        WeaponIdentifier currentIdentifier = _weaponList[_currentWeaponIndex].GetComponent<WeaponIdentifier>();

        if (currentIdentifier == null)
        {
            Debug.LogWarning($"{name} : WeaponIdentifier null pour l'arme courante.");
            return;
        }

        StopWeaponEvent(currentIdentifier.weaponType);
    }
    private void StopWeaponEvent(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Gatling:
                _stopGatling.Post(gameObject);
                break;
        }
    }

    private void SetWeaponRTPC(WeaponType type)
    {
        float rtpcValue = type switch
        {
            WeaponType.Fugitive => 0f,
            WeaponType.Sobek => 1f,
            WeaponType.Gatling => 2f,
            _ => -1f
        };

        if (rtpcValue >= 0)
        {
            _weaponRTPC.SetGlobalValue(rtpcValue);
        }
        else
        {
            Debug.LogWarning($"{name} : WeaponType inconnu ({type}) pour RTPC.");
        }
    }

    private void PostWeaponSound(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Fugitive:
                _fugitiveShoot.Post(gameObject);
                break;
            case WeaponType.Sobek:
                _sobekShoot.Post(gameObject);
                break;
            case WeaponType.Gatling:
                _gatlingShoot.Post(gameObject);
                break;
            default:
                Debug.LogWarning($"{name} : Aucun son assigné pour {type}");
                break;
        }
    }
                #endregion
    public void AddWeapon(Weapon weaponToAdd)
    {
        Weapon newWeapon = Instantiate(weaponToAdd, _weaponParent);
        newWeapon.Initialize(this);

        _weaponList.Add(newWeapon);
        _weaponIdentifiers.Add(newWeapon.GetComponent<WeaponIdentifier>());

        EnableWeapon(_weaponList.Count - 1);
    }

    public void RemoveWeapon(Weapon weaponToRemove)
    {

        int index = _weaponList.IndexOf(weaponToRemove);
        if (index >= 0)
        {
            _weaponList.RemoveAt(index);
            _weaponIdentifiers.RemoveAt(index);
        }

        EnableWeapon(0);
    }

    private void GetAllChildWeapon()
    {
        _weaponList.Clear();
        _weaponIdentifiers.Clear();

        foreach (var item in GetComponentsInChildren<Weapon>())
        {
            item.Initialize(this);

            _weaponList.Add(item);
            _weaponIdentifiers.Add(item.GetComponent<WeaponIdentifier>());
        }

        EnableWeapon(0);
    }
}
