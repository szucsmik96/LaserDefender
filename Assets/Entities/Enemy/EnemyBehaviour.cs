using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public GameObject EnemyLaser;
    public float health = 300f;
    public float projectileSpeed = 3f;
    public float rate = 2f;
	public int scoreValue = 150;

	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.gameObject.GetComponent<Projectile>();
        if (missile)
        {
        missile.Hit();
            health -= missile.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
				scoreKeeper.Score (scoreValue);
            }
        }
    }
    void Backfire()
    {
        GameObject laser = Instantiate(EnemyLaser, (transform.position + new Vector3(0,-0.7f,0)), Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
    }
    void Update()
    {
        float prob = rate * Time.deltaTime;
        if (prob > Random.value)
        {
            Backfire();
        }
    }
}
