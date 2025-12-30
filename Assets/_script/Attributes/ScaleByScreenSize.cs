using System.Collections.Generic;
using UnityEngine;

public class ScaleByScreenSize : MonoBehaviour
{
    private Camera cam;

    [Header("Reference Camera Settings")]
    public float referenceOrthoSize = 5f;
    public float referenceAspect = 16f / 9f;

    private float refCamWidth;
    private float refCamHeight;

    private readonly List<Transform> children = new();
    private readonly List<Vector3> viewportPositions = new();
    private readonly List<Vector3> originalScales = new();

    private float lastAspect;
    private float lastOrthoSize;

    void Start()
    {
        cam = Camera.main;

        refCamHeight = 2f * referenceOrthoSize;
        refCamWidth = refCamHeight * referenceAspect;

        foreach (Transform child in transform)
        {
            children.Add(child);

            viewportPositions.Add(
                cam.WorldToViewportPoint(child.position)
            );

            originalScales.Add(child.localScale);
        }

        lastAspect = cam.aspect;
        lastOrthoSize = cam.orthographicSize;

        ApplyScaling();
    }

    void Update()
    {
        if (Mathf.Approximately(cam.aspect, lastAspect) &&
            Mathf.Approximately(cam.orthographicSize, lastOrthoSize))
            return;

        lastAspect = cam.aspect;
        lastOrthoSize = cam.orthographicSize;

        ApplyScaling();
    }

    void ApplyScaling()
    {
        float currentCamHeight = 2f * cam.orthographicSize;
        float currentCamWidth = currentCamHeight * cam.aspect;

        float scaleX = currentCamWidth / refCamWidth;
        float scaleY = currentCamHeight / refCamHeight;

        for (int i = 0; i < children.Count; i++)
        {
            Vector3 vp = viewportPositions[i];
            Vector3 worldPos = cam.ViewportToWorldPoint(
                new Vector3(
                    vp.x,
                    vp.y,
                    children[i].position.z - cam.transform.position.z
                )
            );
            children[i].position = worldPos;

            children[i].localScale = Vector3.Scale(
                originalScales[i],
                new Vector3(scaleX, scaleY, 1f)
            );
        }
    }
}