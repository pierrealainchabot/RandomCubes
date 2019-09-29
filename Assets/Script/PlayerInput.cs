using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.Instance.ShuffleCubes();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LevelManager.Instance.ToggleScaleAtIndex(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LevelManager.Instance.ToggleScaleAtIndex(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LevelManager.Instance.ToggleScaleAtIndex(2);
        }
    }
}
