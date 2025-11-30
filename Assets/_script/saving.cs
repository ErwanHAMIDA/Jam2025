using System.IO;
using System.Threading.Tasks;
using UnityEngine;

//public class TestSave : MonoBehaviour
//{
//    private string SavePath;
//    private GameState gameState;

//    void Start()
//    {
//        SavePath = Application.persistentDataPath + "/save.json";
//        gameState = new GameState();
//        gameState.AddValueAndScene("Kiwistiti Shop", 999);
//    }
//    public void save()
//    {
//        string json = JsonUtility.ToJson(gameState.valuesAndScene, true);
//        File.WriteAllText(SavePath, json);
//    }

//    public GameState LoadData()
//    {

//        if (File.Exists(SavePath))
//        {
//            string json = File.ReadAllText(SavePath);
//            return JsonUtility.FromJson<GameState>(json);

//        }

//        return null;
//    }

//}