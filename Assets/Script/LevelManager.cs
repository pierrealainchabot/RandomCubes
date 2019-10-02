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

    public bool ContainsColorAtIndex(int coloIndex)
    {
        return coloIndex >= 0 && coloIndex < _cubesOrderList.Count;
    }

    public ECubeColor GetColorAtIndex(int colorIndex)
    {
        return _cubesOrderList[colorIndex];
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
        for (int i = 0; i < _cubesOrderList.Count; i++)
        {
            ECubeColor color = _cubesOrderList[i];
            TestCube cube = CubeManager.Instance.GetCubeByItsColor(color);
            if (cube != null)
            {
                SetCubePositionByItsIndex(cube, i);
            }
        }
    }
    
    public void PlaceCube(TestCube cube)
    {
        int index = FindOrAddColorIndex(cube.CubeColor);
        SetCubePositionByItsIndex(cube, index);
    }

    private void SetCubePositionByItsIndex(TestCube cube, int colorIndex)
    {
        Vector3 cubePosition = GetScenePositionForIndex(colorIndex);
        cube.transform.position = cubePosition;
    }

    private int FindOrAddColorIndex(ECubeColor cubeColor)
    {
        int index = _cubesOrderList.FindIndex(color => color.Equals(cubeColor));
        if (index == -1)
        {
            // Color has no order, add it at the end of the list
            index = _cubesOrderList.Count;
            _cubesOrderList.Add(cubeColor);
        }

        return index;
    }

    private Vector3 GetScenePositionForIndex(int colorIndex)
    {
        return new Vector3(colorIndex * 2, 0, 0);
    }

    public override string ToString()
    {
        return "CubeOrderList : " + string.Join(",", _cubesOrderList);
    }
}
