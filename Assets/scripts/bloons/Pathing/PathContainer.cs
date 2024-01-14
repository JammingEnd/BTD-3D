using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PathContainer
{
    public List<PathNode> pathingDefs = new List<PathNode>();

    public float TrackLength;

    public void CalcTrackLenght()
    {
        float length = 0;
        foreach (var node in pathingDefs)
        {
            length += Vector3.Distance(node.transform.position, node.NextNode.transform.position);
        }
        TrackLength = length;
    }

    
}
