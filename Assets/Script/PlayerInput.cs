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
            ToggleScaleAtIndex(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleScaleAtIndex(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleScaleAtIndex(2);
        }
    }
    
    private void ToggleScaleAtIndex(int colorIndex)
    {
        if (!LevelManager.Instance.ContainsColorAtIndex(colorIndex))
        {
            Debug.Log("No color registered for this key at color index " + colorIndex);
            return;
        }

        ECubeColor color = LevelManager.Instance.GetColorAtIndex(colorIndex);
        CubeManager.Instance.ToggleScale(color);
    }
}
