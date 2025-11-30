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

    [SerializeField] GameObject Canva;
    [SerializeField] GameObject TextBox;
    [SerializeField] GameObject TextMesh;

    DialogueOption WishDialogueBox = DialogueOption.PasContant;
    string TextToDisplay;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        Canva.SetActive(false); // caché par défaut
    }

    void Update()
    {

    }

    public void CloseDialogue()
    {
        StartCoroutine(ShowDialogueSequence());
    }

    public void StartDialogue()
    {
        // on lance la séquence une seule fois
        Canva.SetActive(true);
        TextMesh.GetComponent<TextMeshProUGUI>().SetText(TextToDisplay);
        TextBox.GetComponent<Animator>().SetBool("Open", true);
        TextMesh.GetComponent<Animator>().SetBool("Open", true);
    }

    public void StartTempDialogue()
    {
        // on lance la séquence une seule fois
        Canva.SetActive(true);
        TextMesh.GetComponent<TextMeshProUGUI>().SetText(TextToDisplay);
        TextBox.GetComponent<Animator>().SetBool("Open", true);
        TextMesh.GetComponent<Animator>().SetBool("Open", true);
        StartCoroutine(ShowDialogueSequence());
    }

    public void SetDialogueContent(string content)
    {
        TextToDisplay = content;
    }

    public void SetDialogueContentWithState(DialogueOption clientState)
    {
        WishDialogueBox = clientState;

        switch (WishDialogueBox)
        {
            case DialogueOption.PasContant:
                TextToDisplay = "Berk ! C'est vraiment mauvais";
                break;
            case DialogueOption.MidTier:
                TextToDisplay = "Ça pourrait être mieux, je reviendrai quand je serai bourrée.";
                break;
            case DialogueOption.Content:
                TextToDisplay = "Meilleur que si j'étais bourrée.";
                break;
        }
    }

    private IEnumerator ShowDialogueSequence()
    {
        yield return new WaitForSeconds(1.5f);

        TextBox.GetComponent<Animator>().SetBool("Close", true);
        TextMesh.GetComponent<Animator>().SetBool("Close", true);

        TextBox.GetComponent<Animator>().SetBool("Open", false);
        TextMesh.GetComponent<Animator>().SetBool("Open", false);

        yield return new WaitForSeconds(1.5f); 

        Canva.SetActive(false);
    }
}
