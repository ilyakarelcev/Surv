using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakedMeshManager : MonoBehaviour
{
    
    public static BakedMeshManager Instance;
    public Dictionary<string, Mesh> Meshes = new Dictionary<string, Mesh>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
        
    }

}
