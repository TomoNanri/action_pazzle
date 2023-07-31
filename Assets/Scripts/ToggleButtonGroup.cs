using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleButtonGroup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOnRock(bool state)
    {
        if (state)
        {
            GameManager.current.selectedItem = (int)Item.Rock;
        }
    }

    public void ToggleOnLeftJump(bool state)
    {
        if (state)
        {
            GameManager.current.selectedItem = (int)Item.LeftJump;
        }
    }
    public void ToggleOnRightJump(bool state)
    {
        if (state)
        {
            GameManager.current.selectedItem = (int)Item.RightJump;
        }
    }
}
