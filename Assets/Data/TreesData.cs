using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreesPrefabs", menuName = "Data/Trees Prefabs")]
public class TreesData : ScriptableObject
{
    public List<GameObject> treesPrefabs;
}