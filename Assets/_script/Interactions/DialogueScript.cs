using UnityEngine;
using TMPro;
using System.Collections;

public enum DialogueOption
{
    PasContant,
    MidTier,
    Content
}

public class DialogueScript : MonoBehaviour
{
    private Animator m_Animator;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _textBox;
    [SerializeField] private GameObject _textMesh;

    private DialogueOption _wishDialogueBox = DialogueOption.PasContant;
    private string _textToDisplay;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        _canvas.SetActive(false);
    }

    public void CloseDialogue()
    {
        StartCoroutine(ShowDialogueSequence());
    }

    public void StartDialogue()
    {
        _canvas.SetActive(true);

        _textMesh.GetComponent<TextMeshProUGUI>().SetText(_textToDisplay);
        _textBox.GetComponent<Animator>().SetBool("Open", true);
        _textMesh.GetComponent<Animator>().SetBool("Open", true);
    }

    public void StartTempDialogue()
    {
        _canvas.SetActive(true);

        _textMesh.GetComponent<TextMeshProUGUI>().SetText(_textToDisplay);
        _textBox.GetComponent<Animator>().SetBool("Open", true);
        _textMesh.GetComponent<Animator>().SetBool("Open", true);
        StartCoroutine(ShowDialogueSequence(1.5f));
    }

    public void SetDialogueContent(string content)
    {
        _textToDisplay = content;
    }

    public void SetDialogueContentWithState(DialogueOption clientState)
    {
        _wishDialogueBox = clientState;

        switch (_wishDialogueBox)
        {
            case DialogueOption.PasContant:
                _textToDisplay = "Berk ! C'est vraiment mauvais";
                break;
            case DialogueOption.MidTier:
                _textToDisplay = "Ça pourrait être mieux, je reviendrai quand je serai bourrée.";
                break;
            case DialogueOption.Content:
                _textToDisplay = "Meilleur que si j'étais bourrée.";
                break;
        }
    }

    private IEnumerator ShowDialogueSequence(float tempAnim = 0.0f)
    {
        yield return new WaitForSeconds(tempAnim);

        _textBox.GetComponent<Animator>().SetBool("Close", true);
        _textMesh.GetComponent<Animator>().SetBool("Close", true);

        _textBox.GetComponent<Animator>().SetBool("Open", false);
        _textMesh.GetComponent<Animator>().SetBool("Open", false);

        yield return new WaitForSeconds(1.5f); 

        _canvas.SetActive(false);
    }
}
