using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] Button newGameButton;
    
    void Start()
    {
        newGameButton.onClick.AddListener(RandomGen);
        GetAllTiles();
        RandomGen();
    }

    void GetAllTiles()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    void RandomGen()
    {
        bool[] boolValues = RandomBoolList();

        for (int i = 0; i < tiles.Length; i++)
        {
            TileBehaviour tileScript = tiles[i].GetComponent<TileBehaviour>();

            tileScript.on = boolValues[i];
        }
    }

    bool[] RandomBoolList()
    {
        bool[] boolValues = new bool[tiles.Length];
        bool invalid = true;
        bool containsAllFalse;
        
        while (invalid)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                boolValues[i] = RandomBool();
            }

            containsAllFalse = !boolValues.Contains(true);

            invalid = containsAllFalse ? true : false;
        }

        return boolValues;
    }

    bool RandomBool()
    {
        if (Random.value <= .5f)
        {
            return true;
        }
        return false;
    }
}
