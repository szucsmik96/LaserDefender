using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width =10f;
    public float height =5f;
    private bool movingRight = false;
    public float speed = 2f;
    private float xmax, xmin;
	public float spawnDelay = 0.5f;

    // Use this for initialization
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 LeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 RightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = RightEdge.x;
        xmin = LeftEdge.x;
		SpawnUntilFull ();
    }

	void SpawnEnemies(){
		foreach (Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();
		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
		
    void Update()
    {
        if (movingRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if((transform.position.x + (0.5f*width))>xmax) 
        {
            movingRight = false;
        } else if ((transform.position.x - (0.5f * width)) < xmin){
            movingRight = true;
        }
		if (AllMembersDead()) {
			Debug.Log ("Empty Formation");
			SpawnUntilFull();
		}
    }
	Transform NextFreePosition(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}

	bool AllMembersDead(){
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
		
	}
}
