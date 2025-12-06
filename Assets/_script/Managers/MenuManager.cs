using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private GameObject _continueButton;

    private void Start()
    {
        if (SaveManager.Instance.IsSaveExist())
            _continueButton.GetComponent<Button>().interactable = true;
        else 
            _continueButton.GetComponent<Button>().interactable = false;
    }

    public void StartNewGame()
    {
        string inputText = GameObject.FindAnyObjectByType<TMP_InputField>().text;

        if (inputText == "") return;

        SaveManager.Instance.DeleteData();
        GameData.Instance.SetInputBarName(GameObject.FindAnyObjectByType<TMP_InputField>().text);
        SceneManager.LoadScene(_sceneName);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeSelectedButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
    }
}