using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject player;
    public Transform enemy;
    public float movementSpeed = 1f;

    private int rangeMin = 0;
    private int rangeMax = 3;
    private int actionRange = 4;
    private int movementRange = 4;
    private int movementNum = 0;
    private int actionNum = 0;
    private float spacesX = 0f;
    private float spacesZ = 0f;

    Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        spacesX = transform.position.x;
        spacesZ = transform.position.z;
        StartCoroutine(AIMovement());
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed*Time.deltaTime);
        
    }

    IEnumerator AIMovement()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            

            actionNum = Random.Range(rangeMin, actionRange);

            if (actionNum == 0) // move towards player
            {
                targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            }
            else // random movement 
            {
                movementNum = Random.Range(rangeMin, movementRange);

                if (movementNum == 0) // left
                {
                    spacesX += -(Random.Range(rangeMin, rangeMax)); // gets random # of spaces to move
                }
                else if (movementNum == 1) // right
                {
                    spacesX += Random.Range(rangeMin, rangeMax); // gets random # of spaces to move
                }
                else if (movementNum == 2) // up
                {
                    spacesZ += -(Random.Range(rangeMin, rangeMax)); // gets random # of spaces to move
                }
                else // down
                {
                    spacesZ += Random.Range(rangeMin, rangeMax); // gets random # of spaces to move
                }

                targetPosition = new Vector3(Mathf.Round(spacesX), transform.position.y, Mathf.Round(spacesZ));
            }
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        targetPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        
        /*if (enemy.eulerAngles.y < 0) {
            //enemy.eulerAngles.y += 360f
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        else
        {
            //enemy.eulerAngles.y -= 180f;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        } 
        */
        spacesX = Mathf.Round(transform.position.x);
        spacesZ = Mathf.Round(transform.position.z);
    }
}
