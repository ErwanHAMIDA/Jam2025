using System.Collections;
using System.Collections.Generic;
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

    SANS_ACOOL          = 0b001000,
    ALCOOL              = 0b010000,
    NEUTRAL_ALCOOL      = 0b100000,

    PLAT                = 0b001000,
    SPARKLING           = 0b010000,
    NEUTRAL_SPARKLING   = 0b100000,

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
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private GameObject _dialogueHandler;
    [SerializeField] private GameObject _dialogPannel;
    [SerializeField] private GameObject _acceptButton;
    [SerializeField] private GameObject _declineButton;

    private AudioClip _payAudio;
    private AudioClip _walkAudio;
    private CharacterSpe _specifities;
    private Animator _characterAnimator;
    private GameManager _gameManager;

    private WhoIsIt _currentWhoIsIt;

    private int _offerPrice;

    private bool _openOffer;
    private bool _isWaitingForDrink;

    public AudioClip PayAudio { set { _payAudio = value; } }    
    public AudioClip WalkAudio { set { _walkAudio = value; } }    

    #region Memo (No Code)
    // favorite Ingredients
    // Hated Ingredients

    // arrive()
    // Accepte or Decline
    // Leave


    // Calculate Win %
    // Instantiate Gambling Rate
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    #endregion
    void Start()
    {
        float spawnWidth = Screen.width * 0.5f;

        transform.position = new Vector3( 0.0f - spawnWidth, 0.0f, 0.0f);
        _openOffer = true;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Arrival()
    {
        int r = UnityEngine.Random.Range( 0, 3 );

        _acceptButton.SetActive(true);
        _declineButton.SetActive(true);

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
        GameData.Instance.AddGold(-_offerPrice);

        _dialogueHandler.GetComponent<DialogueScript>().CloseDialogue();

        transform.GetChild(1).gameObject.SetActive(false);
        _isWaitingForDrink = true;
    }

    public void DeclineOffer()
    {
        StartCoroutine(LeaveAfterDecline());
    }

    IEnumerator LeaveAfterDecline()
    {
        _dialogueHandler.GetComponent<DialogueScript>().SetDialogueContent("Bon bah à la prochaine");
        _dialogueHandler.GetComponent<DialogueScript>().StartTempDialogue();

        gameObject.GetComponent<Animator>().SetBool("ServedNeutral", true);

        yield return new WaitForSeconds(3.5f);
        transform.GetChild(1).gameObject.SetActive(false);
        //_dialogueHandler.GetComponent<DialogueScript>().CloseDialogue();

        _gameManager.SpawnNewClient();

        Destroy(gameObject);
    }

    
    public void CharacterCreation(byte characterFlag)
    {
        _currentWhoIsIt = (WhoIsIt)UnityEngine.Random.Range(0, 4);

        _offerPrice = UnityEngine.Random.Range(20, 500);
        int statsCount = (int)characterType.COUNT / 3;

        _specifities.idealStats = new Dictionary<IngredientType, int>(statsCount) 
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
                    _specifities.idealStats[(IngredientType)i] = 4; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    _specifities.idealStats[(IngredientType)i] = 30; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    _specifities.idealStats[(IngredientType)i] = -15; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    _specifities.idealStats[(IngredientType)i] = 28; // 15 -> 100
                }
                offset += 4;

                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _sprites[(int)_currentWhoIsIt];

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
                    _specifities.idealStats[(IngredientType)i] = 50; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    _specifities.idealStats[(IngredientType)i] = 30; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    _specifities.idealStats[(IngredientType)i] = 40; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    _specifities.idealStats[(IngredientType)i] = -45; // 15 -> 100
                }
                offset += 4;
                int val = (int)_currentWhoIsIt;

                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _sprites[(int)_currentWhoIsIt];

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
                    _specifities.idealStats[(IngredientType)i] = -30; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    _specifities.idealStats[(IngredientType)i] = 5; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    _specifities.idealStats[(IngredientType)i] = -40; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    _specifities.idealStats[(IngredientType)i] = -10; // 15 -> 100
                }
                offset += 4;
                int val = (int)_currentWhoIsIt;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _sprites[(int)_currentWhoIsIt];

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
                    _specifities.idealStats[(IngredientType)i] = -42; // -50 -> 50
                }
                else if (Flags.HAS_FLAG(characterFlag, f2))
                {
                    //Sweet
                    _specifities.idealStats[(IngredientType)i] = 25; // -16 -> 14
                }
                else if (Flags.HAS_FLAG(characterFlag, f3))
                {
                    //alcohol
                    _specifities.idealStats[(IngredientType)i] = -30; // 15 -> 100
                }
                else if (Flags.HAS_FLAG(characterFlag, f4))
                {
                    //Sparkling

                    _specifities.idealStats[(IngredientType)i] = 40; // 15 -> 100
                }
                offset += 4;
                int val = (int)_currentWhoIsIt;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _sprites[(int)_currentWhoIsIt];

            }
        }
        
        Arrival();
    }

    public void ReceiveShaker(int priceToPay)
    {
        _isWaitingForDrink = false;

        if (priceToPay < _offerPrice - (_offerPrice * 0.25))
        {
            gameObject.GetComponent<Animator>().SetBool("ServedPASKONTANT", true);
            _dialogueHandler.GetComponent<DialogueScript>().SetDialogueContentWithState(DialogueOption.PasContant);
        }
        else if (priceToPay > _offerPrice + (_offerPrice * 0.25))
        {
            gameObject.GetComponent<Animator>().SetBool("ServedHappy", true);
            _dialogueHandler.GetComponent<DialogueScript>().SetDialogueContentWithState(DialogueOption.Content);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("ServedNeutral", true);
            _dialogueHandler.GetComponent<DialogueScript>().SetDialogueContentWithState(DialogueOption.MidTier);
        }

        _acceptButton.SetActive(false);
        _declineButton.SetActive(false);
        _dialogPannel.SetActive(true);
        _dialogueHandler.GetComponent<DialogueScript>().StartTempDialogue();
    }

    void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitAtBar") && _openOffer)
        {
            _openOffer = false;
            transform.GetChild(1).gameObject.SetActive(true);
            _dialogueHandler.SetActive(true);
            _dialogueHandler.GetComponent<DialogueScript>().SetDialogueContent("BONSOIR");
            _dialogueHandler.GetComponent<DialogueScript>().StartDialogue();
        }
    }

    #region Helpers
    public bool IsWaitingForDrink()
    {
        return _isWaitingForDrink;
    }

    public CharacterSpe GetCharactersSpecifications()
    {
        return _specifities;
    }
    #endregion
}
