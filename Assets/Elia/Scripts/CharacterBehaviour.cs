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

enum WhoIsIt
{
    SUCCUBE,
    CHIEN,
    PHOQUE,
    JOKER
    
}


public class CharacterBehaviour : MonoBehaviour
{

    CharacterSpe specifities;
    Animator characterAnimator;
    GameManager gm;

    WhoIsIt _currentWhoIsIt;

    int offerPrice;

    bool openOffer;

    bool isWaiting;
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

    public void AcceptOffer()
    {
        GameData.Gold -= offerPrice;
        isWaiting = true;
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void DeclineOffer()
    {
        StartCoroutine(LeaveAfterDecline());
    }

    IEnumerator LeaveAfterDecline()
    {
        gameObject.GetComponent<Animator>().SetBool("ServedNeutral", true);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        gm.SpawnNewClient();
    }

    public CharacterSpe GetCharactersSpecifications()
    {
        return specifities;
    }
    public void CharacterCreation(byte characterFlag)
    {
        _currentWhoIsIt = (WhoIsIt)UnityEngine.Random.Range(2, 4);

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

        //here
        if (_currentWhoIsIt == WhoIsIt.CHIEN)
        {
            Debug.Log("Chien");
            for (int i = 0; i < statsCount; i++)
            {
                byte f1 = (byte)Mathf.Pow(2, offset);
                byte f2 = (byte)Mathf.Pow(2, offset + 1);
                byte f3 = (byte)Mathf.Pow(2, offset + 2);
                byte f4 = (byte)Mathf.Pow(2, offset + 3);
                if (Flags.HAS_FLAG(characterFlag, f1))
                {
                    //temperature
                    specifities.idealStats[(IngredientType)i] = 4; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    specifities.idealStats[(IngredientType)i] = 30; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    specifities.idealStats[(IngredientType)i] = -15; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    specifities.idealStats[(IngredientType)i] = 28; // 15 -> 100
                }
                offset += 4;
            }
        }
        else if (_currentWhoIsIt == WhoIsIt.SUCCUBE)
        {
            Debug.Log("Suc");
            for (int i = 0; i < statsCount; i++)
            {
                byte f1 = (byte)Mathf.Pow(2, offset);
                byte f2 = (byte)Mathf.Pow(2, offset + 1);
                byte f3 = (byte)Mathf.Pow(2, offset + 2);
                byte f4 = (byte)Mathf.Pow(2, offset + 3);
                if (Flags.HAS_FLAG(characterFlag, f1))
                {
                    //temperature
                    specifities.idealStats[(IngredientType)i] = 50; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    specifities.idealStats[(IngredientType)i] = 30; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    specifities.idealStats[(IngredientType)i] = 40; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    specifities.idealStats[(IngredientType)i] = -45; // 15 -> 100
                }
                offset += 4;
            }
        }
        else if (_currentWhoIsIt == WhoIsIt.JOKER)
        {
            Debug.Log("JOKER");
            for (int i = 0; i < statsCount; i++)
            {
                byte f1 = (byte)Mathf.Pow(2, offset);
                byte f2 = (byte)Mathf.Pow(2, offset + 1);
                byte f3 = (byte)Mathf.Pow(2, offset + 2);
                byte f4 = (byte)Mathf.Pow(2, offset + 3);
                if (Flags.HAS_FLAG(characterFlag, f1))
                {
                    //temperature
                    specifities.idealStats[(IngredientType)i] = -30; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    specifities.idealStats[(IngredientType)i] = 5; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    specifities.idealStats[(IngredientType)i] = -40; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    specifities.idealStats[(IngredientType)i] = -10; // 15 -> 100
                }
                offset += 4;
            }
        }
        else if (_currentWhoIsIt == WhoIsIt.PHOQUE)
        {
            Debug.Log("PHOQUE");
            for (int i = 0; i < statsCount; i++)
            {
                byte f1 = (byte)Mathf.Pow(2, offset);
                byte f2 = (byte)Mathf.Pow(2, offset + 1);
                byte f3 = (byte)Mathf.Pow(2, offset + 2);
                byte f4 = (byte)Mathf.Pow(2, offset + 3);
                if (Flags.HAS_FLAG(characterFlag, f1))
                {
                    //temperature
                    specifities.idealStats[(IngredientType)i] = -42; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    specifities.idealStats[(IngredientType)i] = 25; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    specifities.idealStats[(IngredientType)i] = -30; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    specifities.idealStats[(IngredientType)i] = 40; // 15 -> 100
                }
                offset += 4;
            }
        }
        
        Arrival();
    }

    public void ReceiveShaker(int priceToPay)
    {
        isWaiting = false;
        if (priceToPay < offerPrice - (offerPrice * 0.25) )
            gameObject.GetComponent<Animator>().SetBool("ServedPASKONTANT", true);
        else if (priceToPay > offerPrice + (offerPrice * 0.25))
            gameObject.GetComponent<Animator>().SetBool("ServedHappy", true);
        else
            gameObject.GetComponent<Animator>().SetBool("ServedNeutral", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitAtBar") && openOffer)
        {
            openOffer = false;
            transform.GetChild(1).gameObject.SetActive(true);
        }
        
    }
}
