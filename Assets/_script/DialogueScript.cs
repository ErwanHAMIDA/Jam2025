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

    public GameObject TB;
    public bool IsStopped = false; // false = caché par défaut
    public DialogueOption WishDialogueBox;
    public TMP_Text tmptxt;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        TB.SetActive(false); // caché par défaut
    }

    void Update()
    {
        // Si IsStopped devient true, on lance la séquence une seule fois
        if (IsStopped)
        {
            // reset pour éviter de relancer en boucle
            StartCoroutine(ShowDialogueSequence());
            IsStopped = false;
        }
    }

    private IEnumerator ShowDialogueSequence()
    {
        TB.SetActive(true);
        m_Animator.SetTrigger("Trhide");

        switch (WishDialogueBox)
        {
            case DialogueOption.PasContant:
                tmptxt.text = "Berk ! C'est vraiment mauvais";
                break;
            case DialogueOption.MidTier:
                tmptxt.text = "Ça pourrait être mieux, je reviendrai quand je serai bourrée.";
                break;
            case DialogueOption.Content:
                tmptxt.text = "Meilleur que si j'étais bourrée.";
                break;
        }

        yield return new WaitForSeconds(1f);

        m_Animator.SetTrigger("Trshow");

        yield return new WaitForSeconds(2f); 

        TB.SetActive(false);
    }
}
