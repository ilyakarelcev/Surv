using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MyRender : MonoBehaviour
{

    [SerializeField] private Vector2 _uv;
    [SerializeField] private int _textureSize;
    [SerializeField] private Texture2D _occlusionTexture;
    [SerializeField] private RenderTexture _renderTexture;
    [SerializeField] private Transform _cameras;

    [SerializeField] private Camera _finalCamera;
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private Camera[] _allCamera;
    [SerializeField] private Shader _shader;

    [SerializeField] private Material _materialForRender;
    [SerializeField] private Material _materialResult;

    [SerializeField] private MyRender[] _myRenders;

    public void SetBlack() {
        _meshRenderer.material = _materialForRender;
    }
    public void SetDefault()
    {
        _meshRenderer.material = _materialResult;
    }


    [ContextMenu("Render")]
    void Render()
    {
        _occlusionTexture = new Texture2D(_textureSize, _textureSize);
        _meshRenderer.sharedMaterial.SetTexture("_BaseMap", _occlusionTexture);
        StartCoroutine(RenderProcess());
    }

    private IEnumerator RenderProcess()
    {
        for (int x = 0; x < _textureSize; x++)
            for (int y = 0; y < _textureSize; y++)
                _occlusionTexture.SetPixel(x, y, Color.white * 0.2f);


        for (int x = 0; x < _textureSize; x++)
        {
            for (int y = 0; y < _textureSize; y++)
            {
                float u = (float)x / _textureSize;
                float v = (float)y / _textureSize;

                Vector3 normal;
                Vector3 position = UvTo3D(new Vector2(u, v), out normal);

                if (position == Vector3.zero) continue;

                _cameras.transform.position = position;
                _cameras.transform.rotation = Quaternion.LookRotation(normal);

                foreach (var item in _myRenders)
                {
                    item.SetBlack();
                }
                //_meshRenderer.material = _materialForRender;
                //RenderTexture.active = _renderTexture;
                for (int i = 0; i < _allCamera.Length; i++)
                {
                    _allCamera[i].SetReplacementShader(_shader, "");
                    _allCamera[i].Render();
                }
                //_meshRenderer.material = _materialResult;
                foreach (var item in _myRenders)
                {
                    item.SetDefault();
                }


                _occlusionTexture.SetPixel(x, y, GetColorFromRT());
            }

            _occlusionTexture.Apply();
            yield return null;

            byte[] bytes = _occlusionTexture.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/MyOcclusion/Saved_" + gameObject.name + ".png", bytes);

            // For testing purposes, also write to a file in the project folder
            // File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);


        }
        //Debug.Log("Done");
    }

    public Texture2D _resultTexture;

    [ContextMenu("GetColorFromRT")]
    private Color GetColorFromRT()
    {
        _resultTexture = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, true);
        Rect rectReadPicture = new Rect(0, 0, _renderTexture.width, _renderTexture.height);
        RenderTexture.active = _renderTexture;
        _resultTexture.ReadPixels(rectReadPicture, 0, 0, true);
        _resultTexture.Apply();
        RenderTexture.active = null; // added to avoid errors

        //Debug.Log(_renderTexture.mipmapCount);
        Color mipColor = _resultTexture.GetPixel(0, 0, _renderTexture.mipmapCount - 1);
        return mipColor;
    }


    void OnDrawGizmos()
    {
        //Vector3 position = UvTo3D(_uv);
        //Gizmos.DrawWireSphere(position, 0.02f);
    }


    Vector3 UvTo3D(Vector2 uv, out Vector3 normal)
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        int[] tris = mesh.triangles;
        Vector2[] uvs = mesh.uv;
        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Vector2 u1 = uvs[tris[i]]; // get the triangle UVs
            Vector2 u2 = uvs[tris[i + 1]];
            Vector2 u3 = uvs[tris[i + 2]];
            // calculate triangle area - if zero, skip it
            float a = Area(u1, u2, u3); if (a == 0) continue;
            // calculate barycentric coordinates of u1, u2 and u3
            // if anyone is negative, point is outside the triangle: skip it
            float a1 = Area(u2, u3, uv) / a; if (a1 < 0) continue;
            float a2 = Area(u3, u1, uv) / a; if (a2 < 0) continue;
            float a3 = Area(u1, u2, uv) / a; if (a3 < 0) continue;
            // point inside the triangle - find mesh position by interpolation...
            Vector3 p3D = a1 * verts[tris[i]] + a2 * verts[tris[i + 1]] + a3 * verts[tris[i + 2]];
            // and return it in world coordinates:

            normal =
                mesh.normals[tris[i]] +
                mesh.normals[tris[i + 1]] +
                mesh.normals[tris[i + 2]];
            normal = normal.normalized;

            normal = transform.TransformVector(normal);

            return transform.TransformPoint(p3D);
        }
        // point outside any uv triangle: return Vector3.zero
        normal = Vector3.up;
        return Vector3.zero;
    }

    // calculate signed triangle area using a kind of "2D cross product":
    float Area(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        Vector2 v1 = p1 - p3;
        Vector2 v2 = p2 - p3;
        return (v1.x * v2.y - v1.y * v2.x) / 2;
    }

}
