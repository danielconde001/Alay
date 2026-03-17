using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeLevelCharacters : MonoBehaviour
{
    public static SafeLevelCharacters Instance;

    public List<GameObject> characters;

    int index = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void UnlockNextChar()
    {
        if (index >= characters.Count) return;

        characters[index].SetActive(true);
        index++;
    }
}
