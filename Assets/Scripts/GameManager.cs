using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] static GameObject[] tiles;
    [SerializeField] static GameObject gameOverBlock;
    [SerializeField] Button newGameButton;
    [SerializeField] TextMeshProUGUI movesCountText;

    public static int movesCount;
    
    void Start()
    {
        newGameButton.onClick.AddListener(RandomGen);
        gameOverBlock = GameObject.Find("Block");
        GetAllTiles();
        RandomGen();
    }

    void Update()
    {
        movesCountText.text = ("Moves: " + movesCount);
    }

    public static void GameOver()
    {
        bool[] tileStates = new bool[tiles.Length];

        for (int i = 0; i < tiles.Length; i++)
        {
            TileBehaviour tileScript = tiles[i].GetComponent<TileBehaviour>();

            tileStates[i] = tileScript.on;
        }

        if (!tileStates.Contains(true) || !tileStates.Contains(false))
        {
            gameOverBlock.SetActive(true);
        }
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

        movesCount = 0;
        gameOverBlock.SetActive(false);
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
