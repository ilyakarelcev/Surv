using System.Collections.Generic;
using UnityEngine;

public class EnemyBakedMesh : MonoBehaviour
{

    private SkinnedMeshRenderer _skinRenderer;
    private Mesh _mesh;
    

    void Start()
    {
        _skinRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        GameObject meshObject = new GameObject();
        meshObject.transform.parent = transform.parent;
        meshObject.transform.localPosition = Vector3.zero;
        meshObject.transform.localRotation = _skinRenderer.transform.localRotation;

        if (!BakedMeshManager.Instance.Meshes.ContainsKey(gameObject.name))
        {
            transform.parent = null;
            transform.position = new Vector3(0f,-3f,0f);
            _mesh = new Mesh();
            BakedMeshManager.Instance.Meshes.Add(gameObject.name, _mesh);
        }
        else {
            Destroy(gameObject);
        }

        meshObject.AddComponent<MeshFilter>().mesh = BakedMeshManager.Instance.Meshes[gameObject.name];
        meshObject.AddComponent<MeshRenderer>().material = _skinRenderer.material;

    }

    void FixedUpdate()
    {
        _skinRenderer.BakeMesh(_mesh);
    }

}
