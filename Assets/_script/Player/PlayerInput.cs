using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("======| Global Input Action Asset |======")]
    [Header("")]
    [SerializeField] private InputActionAsset _inputActions;

    [Header("======| Actions Ref |======")]
    [Header("")]
    [SerializeField] private InputActionReference _clickActionReference;
    [SerializeField] private InputActionReference _pauseActionReference;

    [Header("======| PauseMenu Ref |======")]
    [Header("")]
    [SerializeField] private PauseMenuManager _pauseMenuManager;

    private DragController _dragController;

    private bool _isOnPause = false;

    private void Start()
    {
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

        _dragController = GetComponent<DragController>();
    }
    private void OnEnable()
    {
        _inputActions.FindActionMap("Gameplay").Enable();
        _inputActions.FindActionMap("UI").Enable();
    }

    private void OnDisable()
    {
        _inputActions.FindActionMap("Gameplay").Disable();
        _inputActions.FindActionMap("UI").Disable();
    }

    private void Pause_canceled(InputAction.CallbackContext obj)
    {
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
    }

    private void Pause_started(InputAction.CallbackContext obj)
    {
        _pauseMenuManager.Pause();
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
        if (_dragController == null) return;

        //_dragController.CheckDrag();
        //if (_dragController.IsDrag())
        //    _behavior.Test();
    }

    private void OnDestroy()
    {
        _clickActionReference.action.Disable();
        _pauseActionReference.action.Disable();
    }
}
