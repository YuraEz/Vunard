using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class CircleManager : MonoBehaviour
{
    public int partsAmount;
    public int startLayersAmount;
    public int maxLayersAmount;

    public float multiplierScale = 0.3f;
    public float increaseDe = 2f;
    public float padding = 0.1f;
    public float startScale = 0.1f;
    public float partsRotation = 30;
    public GameObject partPref;
    public Dictionary<int, Part[]> Parts;

    public float rotateSpeed = 30;
    public float rotateAbility = 30;
    public float rotateStopDelay = 10f;

    public float updatedScoreValue = 25f;

    public bool direction = true;

    public static CircleManager instance;

    private UIManager uiManager;
    public ScoreManager scoreManager;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        uiManager = ServiceLocator.GetService<UIManager>();
        instance = this;


        Parts = new Dictionary<int, Part[]>();
        UpdateCircle();

        InvokeRepeating("UpdateCircle", 0f, 120f); 
    }

    private bool rotateClockwise = true; // Флаг для направления вращения


    private void UpdateCircle()
    {
        foreach (var layerParts in Parts.Values)
        {
            foreach (Part part in layerParts)
            {
                Destroy(part.gameObject);
            }
        }


        Parts = new Dictionary<int, Part[]>();
        for (int i = 0; i < maxLayersAmount; i++)
        {
            if (!Parts.ContainsKey(i))
            {
                Parts.Add(i, new Part[partsAmount]);
            }
            for (int j = 0; j < partsAmount; j++)
            {
                GameObject part = Instantiate(partPref, transform.position, Quaternion.identity, transform);
                part.transform.eulerAngles = new Vector3(0, 0, j * partsRotation);
                Vector3 newScale = new Vector3(i * multiplierScale, i * multiplierScale, i * multiplierScale);

                if (i > 0)
                {
                    part.transform.localScale = (partPref.transform.localScale * increaseDe) + (newScale);
                }
                else
                {
                    part.transform.localScale = partPref.transform.localScale * startScale;
                }

                SpriteRenderer spriteRenderer = part.GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.sortingOrder = -i;
                Part partObj = part.GetComponent<Part>();
                partObj.SetOutlineIndex(-i * 2);
                Parts[i][j] = partObj;
                partObj.layerIndex = i;
                partObj.arrayIndex = j;
                partObj.gameObject.SetActive(i <= startLayersAmount);
            }
        }
    }



    private void Update()
    {
        // Вычисляем текущий угол вращения
        float rotationDirection = rotateClockwise ? -1 : -1;
        if (direction)
        {
             rotationDirection = rotateClockwise ? 1 : -1;
        }
      
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Time.deltaTime * rotateSpeed * rotationDirection);
    }

    public void AddPart(Part collidedPart, Color color)
    {
        if (Parts.Count <= (collidedPart.layerIndex + 1))
        {
            //lose
            scoreManager.Finish(false);
            uiManager.ChangeScreen("lose");      
            return;
        }

        Part part = Parts[collidedPart.layerIndex + 1][collidedPart.arrayIndex];
        part.gameObject.SetActive(true);
        part.SetColor(color);
    }


    public void ability1()
    {
        rotateSpeed = 5;
        Invoke("StartRotate", rotateStopDelay);
    }

    void StartRotate()
    {
        rotateSpeed = rotateAbility;
    }
}
