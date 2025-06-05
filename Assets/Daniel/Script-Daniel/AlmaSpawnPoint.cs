using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class AlmaSpawnPoint : MonoBehaviour
{

    [HideInInspector] public List<Transform> spawnPoints = new List<Transform>();
    private void Awake()
    {
        //recorre los hijos de alma
        foreach (Transform child in transform)
        {
            if (child.name.Contains("SpawnPoint"))
            {
                spawnPoints.Add(child);
            }
        }
    }
    
    public Vector3 GetSpawnPoint()
    {
        //si na hay spawn points 
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No hay puntos de spawn en: " + gameObject.name);
        }
        //retorna uno de los spawn point 
       int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index].position;
    }
}
