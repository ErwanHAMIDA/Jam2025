using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void StartGame()
    {
        if (string.IsNullOrEmpty(_sceneName))
        {
            Debug.LogError($"[SceneLoader] Aucun nom de scène défini sur {gameObject.name}. Assure-toi d'avoir assigné une scène dans l'inspecteur.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(_sceneName))
        {
            Debug.LogError($"[SceneLoader] La scène \"{_sceneName}\" ne peut pas être chargée. Vérifie qu'elle est bien ajoutée dans File > Build Settings.");
            return;
        }

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