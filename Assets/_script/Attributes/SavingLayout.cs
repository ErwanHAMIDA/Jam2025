using TMPro;
using UnityEngine;

public class SavingLayout : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private GameObject _layoutPlayGame;
    SaveManager.SavingStruct _loadedDatas;

    int _chosenSlot;
    private void OnEnable()
    {
        SetSlotData();
    }

    public void SetSlotData()
    {
        _loadedDatas = _saveManager.LoadAllData();
        if (_loadedDatas == null) _loadedDatas = new SaveManager.SavingStruct();
        for (int i = 0; i < 4; ++i)
        {
            if (_loadedDatas.savingSlot[i] != null)
            {
                GameObject iSaveSlot = gameObject.transform.GetChild(i).gameObject;
                iSaveSlot.transform.GetChild(0).gameObject.SetActive(true); // DataLoaded
                iSaveSlot.transform.GetChild(1).gameObject.SetActive(false); // EMPTY
                iSaveSlot.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Name : " + _loadedDatas.savingSlot[i].Name);
                iSaveSlot.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Gold : " + _loadedDatas.savingSlot[i].Gold);
            }
        }
    }

    public void SetChosenSlot(int id)
    {
        _chosenSlot = id;
    }

    public void StartSaveSlot(int slotID)
    {
        _chosenSlot = slotID;

        if (_loadedDatas.savingSlot[slotID] == null)
        {
            _layoutPlayGame.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            GameData.Instance.CopyFrom(_loadedDatas.savingSlot[slotID]);
            _menuManager.ContinueGame();
        }
    }

    public void CreateNewGame()
    {
        _menuManager.CreateNewGame(_chosenSlot);
    }

    public void DeleteChosenSlot()
    {
        GameObject iSaveSlot = gameObject.transform.GetChild(_chosenSlot).gameObject;
        iSaveSlot.transform.GetChild(0).gameObject.SetActive(false); // DataLoaded
        iSaveSlot.transform.GetChild(1).gameObject.SetActive(true); // EMPTY
        _saveManager.DeleteSlotData(_chosenSlot);
        SetSlotData();
    }
}
