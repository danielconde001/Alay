using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fungus;

public class NPCInteractable : Interactable
{
    private Animator animator;

    public UnityEvent OnTalk;

    public string directionLook = "";
    public string blockName = "";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(directionLook))
        {
            Debug.LogError("no direction for " + gameObject.name);
        }
        else if (directionLook == "UP")
        {
            LookUp();
        }
        else if (directionLook == "DOWN")
        {
            LookDown();
        }
        else if (directionLook == "LEFT")
        {
            LookLeft();
        }
        else if (directionLook == "RIGHT")
        {
            LookRight();
        }
    }

    protected override void Update()
    {
        base.Update();

        #region Debug
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow)) LookUp();
        if (Input.GetKeyDown(KeyCode.DownArrow)) LookDown();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) LookLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) LookRight();
        */
        #endregion
    }

    public override void Interact()
    {
        base.Interact();

        FindObjectOfType<Flowchart>().ExecuteBlock(blockName);

        //OnTalk.Invoke();
    }

    public void LookUp()
    {
        animator.SetTrigger("UP");
    }

    public void LookDown()
    {
        animator.SetTrigger("DOWN");
    }

    public void LookRight()
    {
        animator.SetTrigger("RIGHT");        
    }

    public void LookLeft()
    {
        animator.SetTrigger("LEFT");
    }
}
