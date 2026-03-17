using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorPlacement
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    Count
}

[RequireComponent(typeof(Collider2D))]
public class DoorBehaviour : MonoBehaviour
{
    private LevelBehaviour assignedLevel;

    private bool isLocked = true;
    public bool IsLocked
    {
        get => isLocked;
        set => isLocked = value;
    }

    private SpriteRenderer spriteRenderer;

    [SerializeField] private DoorPlacement doorPlacement;
    public DoorPlacement DoorPlacement
    {
        get => doorPlacement;
    }

    [SerializeField] private Transform entryPoint;
    public Transform EntryPoint
    {
        get => entryPoint;
    }

    [SerializeField] private Sprite openDoorSprite;
    private Sprite closedDoorSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedDoorSprite = spriteRenderer.sprite;
        assignedLevel = GetComponentInParent<LevelBehaviour>();
    }

    public void OpenDoorSprite()
    {
        spriteRenderer.sprite = openDoorSprite;
    }

    public void ClosedDoorSprite()
    {
        spriteRenderer.sprite = closedDoorSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isLocked)
            {
                if (assignedLevel.EntranceDoor == this)
                {
                    //int index = LevelManager.Instance.GetCurrentLevelIndex();
                    //Debug.Log(LevelManager.Instance.CurrentLevel.name);
                    //LevelManager.Instance.CurrentLevel = LevelManager.Instance.InstantiatedLevels[index - 1];
                    //Debug.Log(LevelManager.Instance.CurrentLevel.name);
                    //LevelManager.Instance.GoToNextLevel(LevelManager.Instance.CurrentLevel.GetComponent<LevelBehaviour>().ExitDoor);
                }
                else if (assignedLevel.ExitDoor == this)
                {
                    int index = LevelManager.Instance.GetCurrentLevelIndex();

                    if ((index + 1) % LevelManager.Instance.safeLevelEveryXTimes == 0
                        && (index + 1) >= LevelManager.Instance.safeLevelEveryXTimes
                        && assignedLevel.gameObject != LevelManager.Instance.safeLevel)
                    {
                        DoorBehaviour doorToSafeLvl = null;
                        DoorBehaviour exitDoorFromSafeLvl = null;

                        if (assignedLevel.ExitDoor == assignedLevel.LeftDoor)
                        {
                            doorToSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().RightDoor;
                            exitDoorFromSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().LeftDoor;
                        }
                        else if (assignedLevel.ExitDoor == assignedLevel.RightDoor)
                        {
                            doorToSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().LeftDoor;
                            exitDoorFromSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().RightDoor;
                        }
                        else if (assignedLevel.ExitDoor == assignedLevel.UpDoor)
                        {
                            doorToSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().DownDoor;
                            exitDoorFromSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().UpDoor;
                        }
                        else if (assignedLevel.ExitDoor == assignedLevel.DownDoor)
                        {
                            doorToSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().UpDoor;
                            exitDoorFromSafeLvl = LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().DownDoor;
                        }

                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().EntranceDoor = doorToSafeLvl;
                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().ExitDoor = exitDoorFromSafeLvl;


                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().LeftDoor.ClosedDoorSprite();
                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().RightDoor.ClosedDoorSprite();
                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().UpDoor.ClosedDoorSprite();
                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().DownDoor.ClosedDoorSprite();

                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().ExitDoor.OpenDoorSprite();
                        LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().ExitDoor.IsLocked = false;

                        SafeLevelCharacters.Instance.UnlockNextChar();

                        LevelManager.Instance.GoToNextLevel(LevelManager.Instance.safeLevel.GetComponent<LevelBehaviour>().EntranceDoor, true);

                    }
                    else if (index == (LevelManager.Instance.InstantiatedLevels.Count-1))
                    {
                        FindObjectOfType<Fungus.Flowchart>().ExecuteBlock("End");
                    }

                    else
                    {
                        LevelManager.Instance.CurrentLevel = LevelManager.Instance.InstantiatedLevels[index + 1];
                        LevelManager.Instance.GoToNextLevel(LevelManager.Instance.CurrentLevel.GetComponent<LevelBehaviour>().EntranceDoor);
                    }
                }
            }
        }
    }
}
