using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{                                                         //variable options:
    public GameObject[] asteroidPrefabs;
    public int numberOfAsteroidsNeeded = 10;

                                                           [Header("SpawnArea")]
    public int areaOverride = 0;
    public Vector3 spawnOriginPoint = Vector3.zero;
    public Vector2 areaXAxis = new Vector2(-1,1);
	public Vector2 areaYAxis = new Vector2(-1,1);
	public Vector2 areaZAxis = new Vector2(-1,1);


                                                            //function counters.
    private GameObject asteroidChosenForSpawn;
    private Vector3 pointChosenForSpawn;

                                                         //AstroidParent
    private GameObject asteroidsParent;
    void Start()
    {
        if (areaOverride != 0)
        {
            areaXAxis.x = -areaOverride; areaXAxis.y = areaOverride;
			areaYAxis.x = -areaOverride; areaYAxis.y = areaOverride;
			areaZAxis.x = -areaOverride; areaZAxis.y = areaOverride;
		}


        asteroidsParent = new GameObject("asteroid Parent");                   //a parent
        CreateAsteroidsInArea();
    }


	void CreateAsteroidsInArea()                    //the action code.
    {
        for (int currentAsteroidCount = 1  ;currentAsteroidCount <= numberOfAsteroidsNeeded  ;currentAsteroidCount++)
        {
            asteroidChosenForSpawn = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
            pointChosenForSpawn = new Vector3
                (Random.Range(areaXAxis.x , areaXAxis.y ),
                Random.Range(areaYAxis.x , areaYAxis.y ),
                Random.Range(areaZAxis.x , areaZAxis.y )
                ) + spawnOriginPoint;

			GameObject astroidSpawned =Instantiate(asteroidChosenForSpawn,pointChosenForSpawn,Random.rotation,asteroidsParent.transform);

            float astroidSizeRamge = Random.Range(0.5f, 4f);
            astroidSpawned.transform.localScale = Vector3.one * astroidSizeRamge;
            astroidSpawned.GetComponent<Rigidbody>().mass *= astroidSizeRamge;

			if (Random.Range(0, 3) == 1)
            {
                astroidSpawned.GetComponent<Rigidbody>().velocity = (Random.rotation.eulerAngles.normalized) *Random.Range(0.5f,5f);
            }
          
        }
    }

	private void OnDrawGizmosSelected()
	{
       // Gizmos.DrawLine(new Vector3(1, 1, 1), new Vector3(0, 10, 0));
	}

}
