using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOre : MonoBehaviour
{
    private bool SpawnNewOre = true;
    public GameObject OreSpawnpoint;
    [SerializeField]
    private GameObject[] oreArr;

    void Update()
    {  
        if (SpawnNewOre) {
            StartCoroutine(Spawn());
            SpawnNewOre = false;
        }
    }

    private IEnumerator Spawn()
    {
        int respawnTime = Random.Range(3, 6);
        GameObject orePrefab = oreArr[Random.Range(0, 7)];

        GameObject ore = Instantiate(orePrefab, OreSpawnpoint.transform.position, Quaternion.identity);
        int type = Random.Range(0,3);
        ore.GetComponent<ItemAttributes>().handleType = HandleType.NONE;

        yield return new WaitForSeconds(respawnTime);

        yield return SpawnNewOre = true;
    }
}
