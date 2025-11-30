using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


public class GameManager : MonoBehaviour
{
    [Header("======| Global Input Action Asset |======")]
    [Header("")]
    [SerializeField] private InputActionAsset _inputActions;

    [Header("======| Actions Ref |======")]
    [Header("")]
    [SerializeField] private InputActionReference _clickActionReference;
    [SerializeField] private InputActionReference _pauseActionReference;

    [Header("======| Pause Menu Ref |======")]
    [Header("")]
    [SerializeField] private PauseMenuManager _pauseMenuManager;

    [Header("======| Other |======")]
    [Header("")]
    [SerializeField] GameObject charcterPrefab;
    [SerializeField] GameObject Shaker;
    [SerializeField] GameObject GoldTextCount;
    [SerializeField] GameObject PhysicEnvironmentHolder;
    [SerializeField] Camera WorldCam;
    [SerializeField] AudioClip bellRing;
    [SerializeField] AudioClip walkAudio;
    [SerializeField] AudioClip payAudio;

    GameObject currentCharacter;

    PlayerBehavior _behavior;
    DragController _dragController;

    bool _isPauseMenu = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameData.Gold = 100;
        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        CharacterBehaviour characterBehaviour = newCharacter.GetComponent<CharacterBehaviour>();
        characterBehaviour.CharacterCreation(0b010001); // 0b010000 + 0b000001
        characterBehaviour.WalkAudio = walkAudio;
        characterBehaviour.PayAudio = payAudio;
        currentCharacter = newCharacter;
        GetComponent<Gambling>().GenerateInformations(newCharacter.GetComponent<CharacterBehaviour>().GetCharactersSpecifications());
        
        #region InputActionFolks
        _clickActionReference.action.Enable();
        _pauseActionReference.action.Enable();

        _clickActionReference.action.started += Click_started;
        _clickActionReference.action.performed += Click_performed;
        _clickActionReference.action.canceled += Click_canceled;

        _pauseActionReference.action.started += Pause_started;
        _pauseActionReference.action.performed += Pause_performed;
        _pauseActionReference.action.canceled += Pause_canceled;
        #endregion

        _behavior = GetComponent<PlayerBehavior>();
        _dragController = GetComponent<DragController>();

    }

    private void Click_canceled(InputAction.CallbackContext obj)
    {
        //if (_dragController.IsDrag())
        //    _dragController?.Drop();
    }

    private void Click_performed(InputAction.CallbackContext obj)
    {
        //_dragController?.SetMousePosition();
    }

    private void Click_started(InputAction.CallbackContext obj)
    {
        //if (_dragController == null) return;

        //_dragController.CheckDrag();
        //if (_dragController.IsDrag())
        //    _behavior.Test();
    }

    private void Pause_canceled(InputAction.CallbackContext obj)
    {
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
    }

    private void Pause_started(InputAction.CallbackContext obj)
    {
        _isPauseMenu = !_isPauseMenu;
        if (_isPauseMenu)
            _pauseMenuManager.EnablePauseMenu();
        else
            _pauseMenuManager.DisablePauseMenu();
    }

    private void OnDestroy()
    {
        _clickActionReference.action.Disable();
        _pauseActionReference.action.Disable();
        SpawnNewClient();
    }

    public void SpawnNewClient()
    {
        GameObject newCharacter = Instantiate(charcterPrefab);
        charcterPrefab.gameObject.SetActive(true);
        CharacterBehaviour characterBehaviour = newCharacter.GetComponent<CharacterBehaviour>();
        characterBehaviour.CharacterCreation(0b010001); // 0b010000 + 0b000001
        characterBehaviour.WalkAudio = walkAudio;
        characterBehaviour.PayAudio = payAudio;
        //characterBehaviour.Sprites = _sprites;
        currentCharacter = newCharacter;
        GetComponent<Gambling>().GenerateInformations(newCharacter.GetComponent<CharacterBehaviour>().GetCharactersSpecifications());
    }

    public void ValidCocktail() 
    {
        if (currentCharacter.GetComponent<CharacterBehaviour>().IsWaitingForDrink() == false)
            return;

        if (Shaker.GetComponent<Shaker>().CanBeServed() == false)
            return;

        SFXManager.Instance.PlaySFXClip(bellRing, transform, 1);
        StartCoroutine(ManageCocktail());

    }

    IEnumerator ManageCocktail()
    {
        int price = 0;
        Dictionary<IngredientType, int> ShakerStats = Shaker.GetComponent<Shaker>().GetCocktailStats();
        price = (int)GetComponent<Gambling>().CalculatePayment(ShakerStats);
        currentCharacter.GetComponent<CharacterBehaviour>().ReceiveShaker(price);
        GameData.Gold += price;

        yield return new WaitForSeconds(3.0f);
        SpawnNewClient();
    }

    // Update is called once per frame
    void Update()
    {
        // TEMP
        //if (Input.GetKeyDown(KeyCode.E))
        //    SpawnNewClient();

        GoldTextCount.GetComponent<TMP_Text>().text = GameData.Gold.ToString();
    }

    
}
