using System.Collections.Generic;
using UnityEngine;

public class ScaleByScreenSize : MonoBehaviour
{
    [Header("Reference Camera (design-time)")]
    public float referenceOrthoSize = 5f;
    public float referenceAspect = 16f / 9f;

    private Camera cam;

    private readonly List<Transform> children = new();
    private readonly List<Vector3> referenceViewportPositions = new();
    private readonly List<float> referenceDepths = new();

    void Start()
    {
        cam = Camera.main;

        float refHeight = 2f * referenceOrthoSize;
        float refWidth = refHeight * referenceAspect;

        foreach (Transform child in transform)
        {
            children.Add(child);

            Vector3 refViewport = WorldToReferenceViewport(child.position);
            referenceViewportPositions.Add(refViewport);
            referenceDepths.Add(refViewport.z);
        }

        float currentCamHeight = 2f * cam.orthographicSize;
        float currentCamWidth = currentCamHeight * cam.aspect;

        float scaleX = currentCamWidth / (2f * referenceOrthoSize * referenceAspect);
        float scaleY = currentCamHeight / (2f * referenceOrthoSize);

        for (int i = 0; i < children.Count; i++)
        {
            Vector3 vp = referenceViewportPositions[i];
            children[i].position = cam.ViewportToWorldPoint(
                new Vector3(vp.x, vp.y, referenceDepths[i])
            );

            children[i].localScale = new Vector3(scaleX, scaleY, 1f);
        }
    }

    void LateUpdate()
    {
        

    }

    Vector3 WorldToReferenceViewport(Vector3 worldPos)
    {
        float refHeight = 2f * referenceOrthoSize;
        float refWidth = refHeight * referenceAspect;

        float vx = (worldPos.x / refWidth) + 0.5f;
        float vy = (worldPos.y / refHeight) + 0.5f;

        return new Vector3(vx, vy, worldPos.z - cam.transform.position.z);
    }
}