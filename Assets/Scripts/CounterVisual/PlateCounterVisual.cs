using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlateCounter plateCounter;

    public List<GameObject> plateList;
    private void Awake() {
        plateList = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        plateCounter.OnPlateSpawned += plateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemove += plateCounter_OnPlateRemove;
    }

    private void plateCounter_OnPlateRemove(object sender, EventArgs e)
    {
        GameObject lastPlate =  plateList[plateList.Count - 1];
        plateList.Remove(lastPlate);
        Destroy(lastPlate);
    }

    private void plateCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualSpawned = Instantiate(plateVisualPrefab, spawnPoint);

        float placeOffsetY = .1f;
        plateVisualSpawned.localPosition = new Vector3(0, placeOffsetY * plateList.Count, 0);

        plateList.Add(plateVisualSpawned.gameObject);
    }

  
}
