using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

struct Flags 
{
    static public byte SET_FLAG(byte flagToCheck, byte other)
    {
        return ((flagToCheck) |= (other));
    }
    static public byte UNSET_FLAG(byte flagToCheck, byte other)
    {
        return ((flagToCheck) &= (byte)~(other));
    }
    static public bool HAS_FLAG(byte flagToCheck, byte other)
    {
        return (((flagToCheck) & (other)) != 0);
    }
}

enum characterType
{
    ICE                 = 0b000001,
    FIRE                = 0b000010,
    NEUTRAL_TEMPERATURE = 0b000100,

    SWEET               = 0b001000,
    PAS_SWEET           = 0b010000,
    NEUTRAL_SWEET       = 0b100000,

    SANS_ACOOL = 0b001000,
    ALCOOL = 0b010000,
    NEUTRAL_ALCOOL= 0b100000,

    PLAT = 0b001000,
    SPARKLING= 0b010000,
    NEUTRAL_SPARKLING = 0b100000,

    COUNT = 12,
}


public class CharacterBehaviour : MonoBehaviour
{
    GameObject DialogueHandler;
    CharacterSpe specifities;
    Animator characterAnimator;
    GameManager gm;

    int offerPrice;

    bool openOffer;

    bool isWaitingForDrink;
    // favorite Ingredients
    // Hated Ingredients

    // arrive()
    // Accepte or Decline
    // Leave


    // Calculate Win %
    // Instantiate Gambling Rate
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int w = Screen.width;
        transform.position = new Vector3( 0.0f - (w * 0.5f), 0.0f,0.0f);
        openOffer = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        DialogueHandler = transform.GetChild(0).gameObject;
    }

    void Arrival()
    {
        int r = UnityEngine.Random.Range( 0, 3 );
        switch (r)
        {
            case 0:
                gameObject.GetComponent<Animator>().SetBool("ArriveRotate", true);
                break;
            case 1:
                gameObject.GetComponent<Animator>().SetBool("ArriveUpDown", true);
                break;
            case 2:
                gameObject.GetComponent<Animator>().SetBool("ArriveScale", true);
                break;
        }
    }

    public bool IsWaitingForDrink()
    {
        return isWaitingForDrink;
    }

    public void AcceptOffer()
    {
        DialogueHandler.GetComponent<DialogueScript>().CloseDialogue();
        GameData.Gold -= offerPrice;
        isWaitingForDrink = true;
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void DeclineOffer()
    {
        DialogueHandler.GetComponent<DialogueScript>().CloseDialogue();
        StartCoroutine(LeaveAfterDecline());
    }

    IEnumerator LeaveAfterDecline()
    {
        DialogueHandler.GetComponent<DialogueScript>().SetDialogueContent("Bon bah à la prochaine");
        DialogueHandler.GetComponent<DialogueScript>().StartTempDialogue();
        gameObject.GetComponent<Animator>().SetBool("ServedNeutral", true);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(3.5f);
        gm.SpawnNewClient();
    }

    public CharacterSpe GetCharactersSpecifications()
    {
        return specifities;
    }
    public void CharacterCreation(byte characterFlag)
    {
        offerPrice = UnityEngine.Random.Range(20, 500);
        int statsCount = (int)characterType.COUNT / 3;
        specifities.idealStats = new Dictionary<IngredientType, int>(statsCount) 
        { 
            { IngredientType.ALCOHOL, 0 },
            { IngredientType.SPARKLING, 0 },
            { IngredientType.SWEET, 0 },
            { IngredientType.TEMP, 0 },
        };

        byte offset = 0;

        for (int i = 0; i < statsCount; i++)
        {
            byte f1 = (byte)Mathf.Pow(2, offset);
            byte f2 = (byte)Mathf.Pow(2, offset + 1);
            byte f3 = (byte)Mathf.Pow(2, offset + 2);
            byte f4 = (byte)Mathf.Pow(2, offset + 3);
            if (Flags.HAS_FLAG(characterFlag, f1))
            {
                specifities.idealStats[(IngredientType)i] =  UnityEngine.Random.Range(-50,-16); // -50 -> -15
            }
            else if (Flags.HAS_FLAG(characterFlag, f2))
            {
                specifities.idealStats[(IngredientType)i] = UnityEngine.Random.Range(-16, 15); // -16 -> 14
            }
            else if (Flags.HAS_FLAG(characterFlag, f3))
            {
                specifities.idealStats[(IngredientType)i] = UnityEngine.Random.Range(15, 51); // 15 -> 100
            }
            else if (Flags.HAS_FLAG(characterFlag, f4))
            {
                specifities.idealStats[(IngredientType)i] = UnityEngine.Random.Range(-20, 45); // 15 -> 100
            }
            offset += 4;
        }
        Arrival();
    }

    public void ReceiveShaker(int priceToPay)
    {
        isWaitingForDrink = false;
        if (priceToPay < offerPrice - (offerPrice * 0.25))
        {
            gameObject.GetComponent<Animator>().SetBool("ServedPASKONTANT", true);
            DialogueHandler.GetComponent<DialogueScript>().SetDialogueContentWithState(DialogueOption.PasContant);
        }
        else if (priceToPay > offerPrice + (offerPrice * 0.25))
        {
            gameObject.GetComponent<Animator>().SetBool("ServedHappy", true);
            DialogueHandler.GetComponent<DialogueScript>().SetDialogueContentWithState(DialogueOption.Content);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("ServedNeutral", true);
            DialogueHandler.GetComponent<DialogueScript>().SetDialogueContentWithState(DialogueOption.MidTier);
        }
        DialogueHandler.GetComponent<DialogueScript>().StartTempDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitAtBar") && openOffer)
        {
            openOffer = false;
            transform.GetChild(1).gameObject.SetActive(true);
            DialogueHandler.SetActive(true);
            DialogueHandler.GetComponent<DialogueScript>().SetDialogueContent("BONSOIR");
            DialogueHandler.GetComponent<DialogueScript>().StartDialogue();
        }
        
    }
}
