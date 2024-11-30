using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Scriptable Objects/New Enemy Wave")]
public class EnemyWaveSO : ScriptableObject
{
    public List<GameObject> enemyWave = new List<GameObject>();
}
