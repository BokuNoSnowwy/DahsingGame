using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = new Vector3(startPoint.x, startPoint.y, 0f);
        points[1] = new Vector3(endPoint.x, endPoint.y, 0f);;
        
        lr.SetPositions(points);
    }

    public void EndLine()
    {
        lr.positionCount = 0;
    }
}
