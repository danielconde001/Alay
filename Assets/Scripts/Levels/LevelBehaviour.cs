using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    [SerializeField] private DoorBehaviour leftDoor;
    [SerializeField] private DoorBehaviour rightDoor;
    [SerializeField] private DoorBehaviour upDoor;
    [SerializeField] private DoorBehaviour downDoor;

    public DoorBehaviour EntranceDoor;
    public DoorBehaviour ExitDoor;

    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private List<GameObject> enemies;

    public int enemiesToKill = 3;

    public DoorBehaviour LeftDoor
    {
        get => leftDoor;
    }
    public DoorBehaviour RightDoor
    {
        get => rightDoor;
    }
    public DoorBehaviour UpDoor
    {
        get => upDoor;
    }
    public DoorBehaviour DownDoor
    {
        get => downDoor;
    }

    public bool hasLeftDoor { get => leftDoor != null; }
    public bool hasRightDoor { get => rightDoor != null; }
    public bool hasUpDoor { get => upDoor != null; }
    public bool hasDownDoor { get => downDoor != null; }

    public void Initiate()
    {
        for (int i = 0; i < enemySpawnPoints.Count; i++)
        {
            EntityHealth entity = Instantiate(enemies[i], enemySpawnPoints[i].position, Quaternion.identity).GetComponent<EntityHealth>();
            entity.GetOnDeathEvent().AddListener(OnEnemyDeath);
        }
    }

    public void OnEnemyDeath()
    {
        enemiesToKill--;

        if (enemiesToKill <= 0)
        {
            LevelManager.Instance.UnlockDoorToNextLevel();

            if (ExitDoor == leftDoor)
            {
                leftDoor.OpenDoorSprite();
            }
            else if (ExitDoor == rightDoor)
            {
                rightDoor.OpenDoorSprite();
            }
            else if (ExitDoor == upDoor)
            {
                upDoor.OpenDoorSprite();
            }
            else if (ExitDoor == downDoor)
            {
                downDoor.OpenDoorSprite();
            }
        }
    }

    public DoorBehaviour GetRandomAvailableNonExitDoor()
    {
        List<DoorBehaviour> doors = new List<DoorBehaviour>();

        if (hasLeftDoor)
        {
            doors.Add(leftDoor);
        }
        if (hasRightDoor)
        {
            doors.Add(rightDoor);
        }
        if (hasUpDoor)
        {
            doors.Add(upDoor);
        }
        if (hasDownDoor)
        {
            doors.Add(downDoor);
        }

        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i] == ExitDoor)
            {
                doors.Remove(doors[i]);
            }
        }

        return doors[Random.Range(0, doors.Count)];
    }

    public DoorBehaviour GetExitDoorViaPlacement(DoorPlacement p_doorPlacement)
    {
        if (p_doorPlacement == DoorPlacement.LEFT)
        {
            //Debug.Log(leftDoor + ", " + gameObject.name);
            return leftDoor;
        }
        else if (p_doorPlacement == DoorPlacement.RIGHT)
        {
            //Debug.Log(rightDoor + ", " + gameObject.name);
            return rightDoor;
        }
        else if (p_doorPlacement == DoorPlacement.UP)
        {
            //Debug.Log(upDoor + ", " + gameObject.name);
            return upDoor;
        }
        else if (p_doorPlacement == DoorPlacement.DOWN)
        {
            //Debug.Log(downDoor + ", " + gameObject.name);
            return downDoor;
        }
        else
        {
            return null;
        }
    }

    public DoorBehaviour GetEntranceDoorViaPlacement(DoorPlacement p_doorPlacement)
    {
        if (p_doorPlacement == DoorPlacement.RIGHT)
        {
            //Debug.Log(leftDoor + ", " + gameObject.name);
            return leftDoor;
        }
        else if (p_doorPlacement == DoorPlacement.LEFT)
        {
            //Debug.Log(rightDoor + ", " + gameObject.name);
            return rightDoor;
        }
        else if (p_doorPlacement == DoorPlacement.DOWN)
        {
            //Debug.Log(upDoor + ", " + gameObject.name);
            return upDoor;
        }
        else if (p_doorPlacement == DoorPlacement.UP)
        {
            //Debug.Log(downDoor + ", " + gameObject.name);
            return downDoor;
        }
        else
        {
            return null;
        }
    }
}
