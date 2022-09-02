using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    [SerializeField] Material onState, offState;
    [SerializeField] Renderer rend;
    [SerializeField] float detectionRadius = .7f;
    [SerializeField] List<GameObject> neighbourTiles = new List<GameObject>();

    public bool on;

    void Start()
    {
        rend = GetComponent<Renderer>();
        GetNeighbourTiles();
    }

    void Update()
    {
        rend.material = on ? onState : offState;
    }

    void OnMouseDown()
    {
        foreach (GameObject obj in neighbourTiles)
        {
            TileBehaviour tileScript = obj.GetComponent<TileBehaviour>();

            tileScript.Switch();
        }

        GameManager.movesCount++;
        GameManager.GameOver();
    }

    void Switch()
    {
        on = !on;
    }

    void GetNeighbourTiles()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Tile"))
            {
                neighbourTiles.Add(hitCollider.gameObject);
            }
        }
    }
}
