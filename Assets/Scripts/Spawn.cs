using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    GameObject patientPrefab;
    public int numPatients;
    public int maxNumPatients;

    void Start() {
        Invoke("SpawnPatient", 5.0f);
    }

    void SpawnPatient() {
        numPatients++;
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
        if (numPatients < maxNumPatients) {
            Invoke("SpawnPatient", Random.Range(10.0f,20.0f));
        }
    }
}
