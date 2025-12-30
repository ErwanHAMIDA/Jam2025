using System.Collections.Generic;
using UnityEngine;

public class LockChildrenToCameraView : MonoBehaviour
{
    private Camera cam;

    private readonly List<Transform> children = new();
    private readonly List<Vector3> viewportPositions = new();

    void Start()
    {
        cam = Camera.main;

        foreach (Transform child in transform)
        {
            children.Add(child);
            viewportPositions.Add(
                cam.WorldToViewportPoint(child.position)
            );
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < children.Count; i++)
        {
            Vector3 vp = viewportPositions[i];
            children[i].position = cam.ViewportToWorldPoint(vp);
        }
    }
}
