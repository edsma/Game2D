
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator sharedInstance;
    public LevelBlock firstBlock;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPoint;
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();
    int CountFor = 0;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        
        GenerateInitialBlock(2);
    }

    public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0,allTheLevelBlocks.Count);
        LevelBlock currentBlock;
        Vector3 spawmPosition = Vector3.zero;
        if (currentBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(firstBlock);
            currentBlock.transform.SetParent(this.transform, false);
            spawmPosition = levelStartPoint.position;
        }
        else
        {
            currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
            currentBlock.transform.SetParent(this.transform, false);
            spawmPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position; ;
        }
        Vector3 correction = new Vector3(spawmPosition.x - currentBlock.startPoint.position.x,
                                        spawmPosition.y - currentBlock.startPoint.position.y,
                                        0);
        currentBlock.transform.position = correction;
        currentBlocks.Add(currentBlock);
    }

    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldesBlock = currentBlocks[0];
        currentBlocks.Remove(oldesBlock);
        Destroy(oldesBlock.gameObject);
    }

    public void RemoveAllTheLevelBlock()
    {
        if(currentBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
            RemoveAllTheLevelBlock();
        }
    }

    public void GenerateInitialBlock(int quantity)
    {
        if(quantity != CountFor)
        {
            CountFor++;
            AddLevelBlock();
            GenerateInitialBlock(quantity);
        }
        else
        {
            CountFor = 0;
        }
    }
}
