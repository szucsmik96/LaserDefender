using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed=15.0f;
    public float padding = 1;
    float xmin;
    float xmax;
    public GameObject Laser;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 500f;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }
	void Fire()
    {    
        GameObject beam = Instantiate(Laser, (transform.position + new Vector3(0,0.7f,0)), Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);      
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
            if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
            //gameObject.transform.position += new Vector3(-speed*Time.deltaTime, 0f, 0f);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            //gameObject.transform.position += new Vector3(speed*Time.deltaTime, 0f, 0f);
        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        ProjectileX missile = coll.gameObject.GetComponent<ProjectileX>();
        missile.Hit();
        if (missile)
        {
            health -= missile.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
