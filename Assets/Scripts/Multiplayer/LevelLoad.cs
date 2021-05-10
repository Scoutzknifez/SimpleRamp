using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class LevelLoad : MonoBehaviour
{
    public static LevelLoad instance;

    public Material fallbackLevelMaterial;
    public MaterialMapping[] levelMaterials;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void LoadPiece(LevelPiece data)
    {
        List<Vertex> vertices = new List<Vertex>();
        foreach (Vector3 position in data.vertices)
        {
            Vertex newPoint = new Vertex();
            newPoint.position = position;
            vertices.Add(newPoint);
        }

        List<Face> faces = new List<Face>();
        foreach (ArrayPacker face in data.faces)
        {
            faces.Add(new Face(face.array));
        }

        List<SharedVertex> sharedVertices = new List<SharedVertex>();
        foreach (ArrayPacker shared in data.sharedVertices)
        {
            sharedVertices.Add(new SharedVertex(shared.array));
        }

        ProBuilderMesh pbMesh = ProBuilderMesh.Create(vertices, faces, sharedVertices);
        GameObject generatedObject = pbMesh.gameObject;

        generatedObject.transform.position = data.position;
        generatedObject.transform.rotation = data.rotation;

        MaterialMapping mapping = MaterialMapping.GetMaterialMappingFromName(levelMaterials, data.materialName);

        if (mapping != null)
        {
            generatedObject.GetComponent<Renderer>().sharedMaterial = mapping.material;
        }
        else
        {
            generatedObject.GetComponent<Renderer>().sharedMaterial = fallbackLevelMaterial;
        }

        generatedObject.AddComponent<MeshCollider>();

        generatedObject.transform.parent = transform;
    }
}

public class LevelPiece
{
    public Vector3 position;
    public Quaternion rotation;

    public Vector3[] vertices;
    public ArrayPacker[] faces;
    public ArrayPacker[] sharedVertices;

    public string materialName;

    public LevelPiece(Vector3 _pos, Quaternion _rot, Vector3[] _vertices, ArrayPacker[] _faces, ArrayPacker[] _sharedVertices)
    {
        position = _pos;
        rotation = _rot;
        vertices = _vertices;
        faces = _faces;
        sharedVertices = _sharedVertices;
    }
}

public class ArrayPacker
{
    public int length;
    public int[] array;

    public ArrayPacker(int[] _elements)
    {
        array = _elements;
        length = array.Length;
    }
}

[System.Serializable]
public class MaterialMapping
{
    public string materialName;
    public Material material;

    public static MaterialMapping GetMaterialMappingFromName(MaterialMapping[] map, string matName)
    {
        return Array.Find(map, mapping => mapping.materialName.Equals(matName));
    }

    public static MaterialMapping GetMaterialMappingFromMaterial(MaterialMapping[] map, Material material)
    {
        return Array.Find(map, mapping => mapping.material.Equals(material));
    }
}