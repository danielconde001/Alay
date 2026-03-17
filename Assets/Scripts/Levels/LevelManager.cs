using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("LevelManager");
                instance = obj.AddComponent<LevelManager>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private int levelsToSpawn;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    [SerializeField] private List<LevelBehaviour> levels = new List<LevelBehaviour>();

    [SerializeField] private List<LevelBehaviour> spawnedLevels = new List<LevelBehaviour>();
    public List<LevelBehaviour> SpawnedLevels
    {
        get => spawnedLevels;
    }

    public List<GameObject> InstantiatedLevels = new List<GameObject>();
    public List<GameObject> GetInstantiatedLevels()
    {
        return InstantiatedLevels;
    }

    private List<DoorPlacement> doorSequences = new List<DoorPlacement>();

    private GameObject currentLevel;
    public GameObject CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    public GameObject safeLevel;
    public int safeLevelEveryXTimes;

    private void Awake()
    {
        instance = this;
        SpawnLevels();
    }

    private void Start()
    {
        InstantiatedLevels[0].GetComponent<LevelBehaviour>().Initiate();

        DoorBehaviour initDoor = InstantiatedLevels[0].GetComponent<LevelBehaviour>().GetRandomAvailableNonExitDoor();
        Transform initialSpawnPoint = initDoor.gameObject.transform;

        PlayerManager.Instance.GetPlayer().transform.position = initialSpawnPoint.position;
    }

    private void SpawnLevels()
    {
        List<LevelBehaviour> listOfLevels = new List<LevelBehaviour>();
        listOfLevels = levels;

        Vector2 currentPoint = Vector2.zero; // position of recently instantiated level

        for (int i = 0; i < levelsToSpawn; i++)
        {
            int n;
            DoorPlacement doorSeq;
            for (;;)
            {
                StartOfLoop:
                n = Random.Range(0, listOfLevels.Count);
                doorSeq = (DoorPlacement)Random.Range(0, (int)DoorPlacement.Count);

                if (doorSequences.Count > 0)
                {
                    // if doorSeq is NOT compatible with previously instantiated level
                    
                    if (doorSeq == DoorPlacement.LEFT && doorSequences[doorSequences.Count - 1] == DoorPlacement.RIGHT)
                        goto StartOfLoop;
                    else if (doorSeq == DoorPlacement.RIGHT && doorSequences[doorSequences.Count - 1] == DoorPlacement.LEFT)
                        goto StartOfLoop;
                    else if (doorSeq == DoorPlacement.UP && doorSequences[doorSequences.Count - 1] == DoorPlacement.DOWN)
                        goto StartOfLoop;
                    else if (doorSeq == DoorPlacement.DOWN && doorSequences[doorSequences.Count - 1] == DoorPlacement.UP)
                        goto StartOfLoop;
                    else
                    {
                        // if randomly chose level is compatible with previously instantiated level

                        if (listOfLevels[n].hasLeftDoor && doorSequences[doorSequences.Count - 1] == DoorPlacement.RIGHT)
                            goto EndOfLoop;
                        else if (listOfLevels[n].hasRightDoor && doorSequences[doorSequences.Count - 1] == DoorPlacement.LEFT)
                            goto EndOfLoop;
                        else if (listOfLevels[n].hasUpDoor && doorSequences[doorSequences.Count - 1] == DoorPlacement.DOWN)
                            goto EndOfLoop;
                        else if (listOfLevels[n].hasDownDoor && doorSequences[doorSequences.Count - 1] == DoorPlacement.UP)
                            goto EndOfLoop;
                        else 
                            goto StartOfLoop; // if randomly chose level is NOT compatible with previously instantiated level
                    }
                }

                EndOfLoop:
                if (listOfLevels[n].hasLeftDoor && doorSeq == DoorPlacement.LEFT) 
                    goto InstantiateLevel;
                else if (listOfLevels[n].hasRightDoor && doorSeq == DoorPlacement.RIGHT) 
                    goto InstantiateLevel;
                else if (listOfLevels[n].hasUpDoor && doorSeq == DoorPlacement.UP) 
                    goto InstantiateLevel;
                else if (listOfLevels[n].hasDownDoor && doorSeq == DoorPlacement.DOWN) 
                    goto InstantiateLevel;
            }

            InstantiateLevel:
            GameObject obj = Instantiate(listOfLevels[n].gameObject, currentPoint, Quaternion.identity);
            InstantiatedLevels.Add(obj);
            spawnedLevels.Add(listOfLevels[n]);
            doorSequences.Add(doorSeq);

            InstantiatedLevels[i].GetComponent<LevelBehaviour>().ExitDoor = InstantiatedLevels[i].GetComponent<LevelBehaviour>().GetExitDoorViaPlacement(doorSequences[i]);
            if (i > 0)
            {
                InstantiatedLevels[i].GetComponent<LevelBehaviour>().EntranceDoor = InstantiatedLevels[i].GetComponent<LevelBehaviour>().GetEntranceDoorViaPlacement(doorSequences[i - 1]);
            }
            else
            {
                InstantiatedLevels[i].GetComponent<LevelBehaviour>().EntranceDoor = null;
            }

            listOfLevels.RemoveAt(n);
            currentPoint = currentPoint + new Vector2(xOffset, 0);
        }

        safeLevel = Instantiate(safeLevel, new Vector2(0, yOffset), Quaternion.identity);
        currentLevel = InstantiatedLevels[0]; // First level
    }

    public void GoToNextLevel(DoorBehaviour p_doorToEnter, bool isSafeLevel = false)
    {
        if (isSafeLevel)
        {
            Camera.main.transform.position = new Vector3(0, yOffset, -10);
            PlayerManager.Instance.GetPlayer().transform.position = p_doorToEnter.EntryPoint.position;
        }
        else
        {
            int index = GetCurrentLevelIndex();
            InstantiatedLevels[index].GetComponent<LevelBehaviour>().Initiate();
            Camera.main.transform.position = new Vector3(index * xOffset, 0, -10);
            AstarPath.active.data.gridGraph.center.x = index * xOffset;
            AstarPath.active.data.gridGraph.Scan();
            PlayerManager.Instance.GetPlayer().transform.position = p_doorToEnter.EntryPoint.position;
        }
    }

    public void UnlockDoorToNextLevel()
    {
        int index = GetCurrentLevelIndex();

        DoorPlacement dPlacement = doorSequences[index];

        if (dPlacement == DoorPlacement.LEFT) 
        {
            InstantiatedLevels[index].GetComponent<LevelBehaviour>().LeftDoor.IsLocked = false;
        }
        else if (dPlacement == DoorPlacement.RIGHT) 
        {
            InstantiatedLevels[index].GetComponent<LevelBehaviour>().RightDoor.IsLocked = false;
        }
        else if (dPlacement == DoorPlacement.UP) 
        {
            InstantiatedLevels[index].GetComponent<LevelBehaviour>().UpDoor.IsLocked = false;
        }
        else if (dPlacement == DoorPlacement.DOWN) 
        {
            InstantiatedLevels[index].GetComponent<LevelBehaviour>().DownDoor.IsLocked = false;
        }         
    }

    public int GetCurrentLevelIndex()
    {
        for (int i = 0; i < InstantiatedLevels.Count; i++)
        {
            if (currentLevel == InstantiatedLevels[i])
            {
                return i;
            }
        }

        Debug.LogError("Could not find current level index.");
        return -1;
    }
}
