using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoombaMove : MonoBehaviour {

    public int EnemySpeed;
    public int XMoveDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;

        if (hit.distance < 0.9f)
        {
            Flip();
            if (hit.collider.tag == "Player")
            {
                Destroy(hit.collider.gameObject);
            }
        }
	}

    void Flip()
    {
        if (XMoveDirection >0)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }
    }
}
