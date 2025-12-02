using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private string _menuScene;

    [SerializeField] private Button[] _allButtons;
    [SerializeField] private Vector3[] _buttonPositions;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _pauseCanvas;

    [Range(0.1f, 2.0f)]
    [SerializeField] private float _delay = 1.0f;
    [Range(0.1f, 2.0f)]
    [SerializeField] private float _transitionDuration = 1.0f;

    private bool _isOnPause = false;

    private Coroutine _currentCoroutine;

    public void Pause()
    {
        _isOnPause = !_isOnPause;

        if (_isOnPause)
            EnablePauseMenu();
        else
            DisablePauseMenu();
    }

    private void EnablePauseMenu()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);

        StartCoroutine(AnimateStartMenu(_delay));
    }

    private void DisablePauseMenu()
    {
        StartCoroutine(AnimateEndMenu(_delay));
    }

    IEnumerator AnimateStartMenu(float delay)
    {
        if (_allButtons.Length != _buttonPositions.Length) throw new ArgumentOutOfRangeException(
            "Not the same quantity between buttons number and positions number", nameof(_allButtons.Length) + " / " + nameof(_buttonPositions.Length));

        _pauseCanvas.SetActive(true);
        Time.timeScale = 0.0f;
        for (int i = 0; i < _allButtons.Length; i++)
        {
            _allButtons[i].enabled = true;
            StartCoroutine(MoveAndFadeIn(_allButtons[i], _buttonPositions[i], _transitionDuration));

            yield return new WaitForSecondsRealtime(delay);
        }
    }

    IEnumerator AnimateEndMenu(float delay)
    {
        if (_allButtons.Length != _buttonPositions.Length) throw new ArgumentOutOfRangeException(
            "Not the same quantity between buttons number and positions number", nameof(_allButtons.Length) + " / " + nameof(_buttonPositions.Length));

        for (int i = _allButtons.Length - 1; i >= 0; i--)
        {
            _allButtons[i].enabled = false;
            StartCoroutine(MoveAndFadeOut(_allButtons[i], _transitionDuration));

            yield return new WaitForSecondsRealtime(delay);
        }

        _pauseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private IEnumerator MoveAndFadeIn(Button button, Vector2 targetPos, float duration)
    {
        RectTransform rect = button.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        Vector2 startPos = rect.anchoredPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float time = Mathf.Clamp01(elapsed / duration);

            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, time);
            canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, time);

            yield return null;
        }

        rect.anchoredPosition = targetPos;
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator MoveAndFadeOut(Button button, float duration)
    {
        RectTransform rect = button.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        Vector2 startPos = rect.anchoredPosition;
        Vector2 targetPos = new Vector2(0.0f, 0.0f);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float time = Mathf.Clamp01(elapsed / duration);

            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, time);
            canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, time);

            yield return null;
        }

        rect.anchoredPosition = targetPos;
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void AnimateButtonSpawn(int index)
    {
        RectTransform rect = _allButtons[index].GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void ExitLevel()
    {
        if (string.IsNullOrEmpty(_menuScene))
        {
            Debug.LogError($"[SceneLoader] Aucun nom de scène défini sur {gameObject.name}. Assure-toi d'avoir assigné une scène dans l'inspecteur.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(_menuScene))
        {
            Debug.LogError($"[SceneLoader] La scène \"{_menuScene}\" ne peut pas être chargée. Vérifie qu'elle est bien ajoutée dans File > Build Settings.");
            return;
        }

        Time.timeScale = 1.0f;
        SceneManager.LoadScene(_menuScene);
    }

    public void QuitMenu()
    {
        StartCoroutine(AnimateEndMenu(_delay));
    }

    public void ResetLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeSelectedButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }
}