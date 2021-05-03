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
        ProBuilderMesh mesh = ShapeGenerator.GenerateCube(PivotLocation.Center, data.size);
        GameObject generatedObject = mesh.gameObject;

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
    public Vector3 size;
    public Quaternion rotation;

    public string materialName;

    public LevelPiece(Vector3 _pos, Vector3 _size, Quaternion _rot)
    {
        position = _pos;
        size = _size;
        rotation = _rot;
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