using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSelector : MonoBehaviour
{
    public List<GameObject> models;

    private void Awake()
    {
        int rand = Random.Range(0, models.Count);
        models[rand].SetActive(true);
    }
}
