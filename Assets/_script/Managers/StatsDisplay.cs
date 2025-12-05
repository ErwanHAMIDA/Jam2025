using System.Collections.Generic;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    private GameObject Stat1;
    private GameObject Stat2;
    private GameObject Stat3;
    private GameObject Stat4;

    void Start()
    {
        Stat1 = transform.GetChild(0).gameObject;
        Stat2 = transform.GetChild(1).gameObject;
        Stat3 = transform.GetChild(2).gameObject;
        Stat4 = transform.GetChild(3).gameObject;
    }

    public void ResetStat()
    {
        Stat1.transform.GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
        Stat2.transform.GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
        Stat3.transform.GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
        Stat4.transform.GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
    }

    public void AddStats(Dictionary<IngredientType,int> stats)
    {
        foreach (KeyValuePair<IngredientType, int> iterator in stats)
        {
            float newStat = 0;
            switch (iterator.Key)
            {
                case IngredientType.TEMP:
                    newStat = Stat1.transform.GetChild(0).GetChild(0).transform.localPosition.x + (iterator.Value);
                    newStat = Mathf.Clamp(newStat, -50, 50);
                    Stat1.transform.GetChild(0).GetChild(0).transform.SetLocalPositionAndRotation(new Vector3(newStat, 0, 0), Quaternion.identity);
                    break;
                case IngredientType.SWEET:
                    newStat = Stat2.transform.GetChild(0).GetChild(0).transform.localPosition.x + (iterator.Value);
                    newStat = Mathf.Clamp(newStat, -50, 50);
                    Stat2.transform.GetChild(0).GetChild(0).transform.SetLocalPositionAndRotation(new Vector3(newStat, 0, 0), Quaternion.identity);
                    break;
                case IngredientType.ALCOHOL:
                    newStat = Stat3.transform.GetChild(0).GetChild(0).transform.localPosition.x + (iterator.Value);
                    newStat = Mathf.Clamp(newStat, -50, 50);
                    Stat3.transform.GetChild(0).GetChild(0).transform.SetLocalPositionAndRotation(new Vector3(newStat, 0, 0), Quaternion.identity);
                    break;
                case IngredientType.SPARKLING:
                    newStat = Stat4.transform.GetChild(0).GetChild(0).transform.localPosition.x + (iterator.Value);
                    newStat = Mathf.Clamp(newStat, -50, 50);
                    Stat4.transform.GetChild(0).GetChild(0).transform.SetLocalPositionAndRotation(new Vector3(newStat, 0, 0), Quaternion.identity);
                    break;
            }
        }
    }
}
