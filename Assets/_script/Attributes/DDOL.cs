using UnityEngine;

public class DDOL : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsByType<DDOL>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
