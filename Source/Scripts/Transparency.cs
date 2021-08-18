using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Transparency : MonoBehaviour
{
    // Player hand or any body part
    public Transform Player;
    public String TransparencyTag;
    private GameObject CurrentMaterial;
    private float DistanceCalculate;
    private Color BackupColor;
    private Shader BackupShader;
    Ray CastRay;
    RaycastHit CastHit;

    // Update is called once per frame
    void Update()
    {
        CastRay = new Ray(Camera.main.transform.position, Player.position - Camera.main.transform.position);
        /* Camera will calculate player between object distance .
        if you want to show float value, you can set public
        */
        DistanceCalculate = Vector3.Distance(Player.position, Camera.main.transform.position);

        TransparencyLogic();
    }

    public void TransparencyLogic()
    {
        /*
        Camera will detect detect object if object contains correct tag.
        */
        if (Physics.Raycast(CastRay, out CastHit, DistanceCalculate))
        {
            if (CastHit.collider != null)
            {
                if (CastHit.collider.CompareTag(TransparencyTag))
                {
                    if (CurrentMaterial != null)
                    {
                        if (CurrentMaterial != CastHit.collider.gameObject)
                        {
                            EnableTransparency(true);
                            CurrentMaterial = CastHit.collider.gameObject;
                            EnableTransparency(false);
                        }
                    }
                    else
                    {
                        CurrentMaterial = CastHit.collider.gameObject;
                        EnableTransparency(false);
                    }
                }
                else
                {
                    if (CurrentMaterial != null)
                    {
                        EnableTransparency(true);
                        CurrentMaterial = null;
                    }
                }
            }
        }
    }
    public void EnableTransparency(bool boolOperation)
    {
        // The number of children the parent Transform has.
        /*
        It will detect auto object tag and child object. After It will set transparency
        */
        if (CurrentMaterial.transform.parent != null && CurrentMaterial.transform.parent.CompareTag(TransparencyTag))
        {
            for (int i = 0; i < CurrentMaterial.transform.parent.childCount; i++)
            {
                MeshRenderer buildingMesh = CurrentMaterial.transform.parent.GetChild(i).GetComponent<MeshRenderer>() ?? null;

                if (buildingMesh != null)
                {
                    SetTransparency(boolOperation, buildingMesh);
                }
            }
        }
        else
        {
            MeshRenderer buildingMesh = CurrentMaterial.GetComponent<MeshRenderer>() ?? null;
            if (buildingMesh != null)
            {
                SetTransparency(boolOperation, buildingMesh);
            }
        }
    }
    public void SetTransparency(bool objFunctions, MeshRenderer matRenderer)
    {
        // if you walk away, Wall will back to normal
        if (objFunctions)
        {
            matRenderer.material.shader = BackupShader;
            matRenderer.material.color = BackupColor;
        }
        // if player hide in wall or etc.
        else
        {
            // backup shader and color
            BackupShader = matRenderer.material.shader;
            BackupColor = matRenderer.material.color;
            // set wall shader and color
            matRenderer.material.shader = Shader.Find("Transparent/Diffuse");
            Color tempColor = matRenderer.material.color;
            tempColor.a = 0.3F;
            matRenderer.material.color = tempColor;
        }
    }
}