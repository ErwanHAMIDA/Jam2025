using UnityEngine;

public class DDOL : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<DDOL>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
