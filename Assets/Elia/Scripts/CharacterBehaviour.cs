using System;
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

    COUNT = 6,
}


public class CharacterBehaviour : MonoBehaviour
{

    CharacterSpe specifities;
    Animator characterAnimator;

    bool openOffer;
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
        characterAnimator = gameObject.GetComponent<Animator>();
        openOffer = true;
    }

    void Arrival()
    {
        int r = UnityEngine.Random.Range( 0, 3 );
        Debug.Log("Arriving");
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
        // CONSUME GOLD
        // WAIT FOR COCKTAIL
    }

    public void DeclineOffer()
    {
        characterAnimator.SetBool("ServedNeutral", true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public CharacterSpe GetCharactersSpecifications()
    {
        return specifities;
    }
    public void CharacterCreation(byte characterFlag)
    {
        int statsCount = (int)characterType.COUNT / 3;
        specifities.idealStats = new Dictionary<string, int>(statsCount);

        byte offset = 0;

        for (int i = 0; i < statsCount; i++)
        {
            byte f1 = (byte)Mathf.Pow(2, offset);
            byte f2 = (byte)Mathf.Pow(2, offset + 1);
            byte f3 = (byte)Mathf.Pow(2, offset + 2);
            Debug.Log("Flags : " + f1 + ", " + f2 + ", " + f3);
            if (Flags.HAS_FLAG(characterFlag, f1))
            {
                UnityEngine.Random.Range(0,36); // 0 -> 35
                Debug.Log("Type with offset " + offset + " is 1");
            }
            else if (Flags.HAS_FLAG(characterFlag, f2))
            {
                UnityEngine.Random.Range(36, 65); // 36 -> 64
                Debug.Log("Type with offset " + offset + " is 2");
            }
            else if (Flags.HAS_FLAG(characterFlag, f3))
            {
                UnityEngine.Random.Range(65, 101); // 65 -> 100
                Debug.Log("Type with offset " + offset + " is 3");
            }
            offset += 3;
        }
        Arrival();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("WaitAtBar") && openOffer)
        {
            openOffer = false;
            transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log("WaitAtBar");
        }
        
    }
}
