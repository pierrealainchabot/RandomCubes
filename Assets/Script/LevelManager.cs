using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private List<ECubeColor> _cubesOrderList = new List<ECubeColor>();

    void Awake()
    {
        SingletonPattern();
    }
    
    private void SingletonPattern()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleScaleAtIndex(int index)
    {
        ECubeColor color = _cubesOrderList[index];
        CubeManager.Instance.ToggleScale(color);
    }
    
    public void ShuffleCubes() {
        ShuffleCubeOrderList();
        ReorganizeCubesInTheScene();
    }

    private void ShuffleCubeOrderList()
    {
        List<ECubeColor> newCubeOrder = new List<ECubeColor>();
        while (_cubesOrderList.Count > 0)
        {
            var randomIndex = Random.Range(0, _cubesOrderList.Count);
            newCubeOrder.Add(_cubesOrderList[randomIndex]);
            _cubesOrderList.RemoveAt(randomIndex);
        }
        
        _cubesOrderList = newCubeOrder;
        Debug.Log(ToString());
    }
    
    private void ReorganizeCubesInTheScene()
    {
        _cubesOrderList.ForEach(cubeColor =>
        {
            TestCube cube = CubeManager.Instance.GetCubeByItsColor(cubeColor);
            if (cube != null)
            {
                RefreshCubePosition(cube);
            }
        });
    }
    
    public void RefreshCubePosition(TestCube cube)
    {
        Vector3 position = GetScenePositionForColor(cube.CubeColor);
        cube.transform.position = position;
    }

    private Vector3 GetScenePositionForColor(ECubeColor cubeColor)
    {
        int index = _cubesOrderList.FindIndex(color => color.Equals(cubeColor));
        if (index == -1)
        {
            // Color has no order, add it at the end of the list
            index = _cubesOrderList.Count;
            _cubesOrderList.Add(cubeColor);
        }

        return GetScenePositionForIndex(index);
    }

    private Vector3 GetScenePositionForIndex(int index)
    {
        return new Vector3(index * 2, 0, 0);
    }

    public override string ToString()
    {
        return "CubeOrderList : " + string.Join(",", _cubesOrderList);
    }
}
