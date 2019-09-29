using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public static CubeManager Instance { get; private set; }

    private Dictionary<ECubeColor, TestCube> _cubeDictionary = new Dictionary<ECubeColor, TestCube>();

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

    public void RegisterCube(TestCube cube)
    {
        _cubeDictionary.Add(cube.CubeColor, cube);
    }

    public TestCube GetCubeByItsColor(ECubeColor color)
    {
        if (!_cubeDictionary.ContainsKey(color))
        {
            return null;
        }

        return _cubeDictionary[color];
    }

    public void ToggleScale(ECubeColor color) 
    {
        if (_cubeDictionary.ContainsKey(color)) 
        {
            _cubeDictionary[color].ToggleScale();
            ResetAllScaledCubesNotOfColor(color);
        }
    }

    private void ResetAllScaledCubesNotOfColor(ECubeColor color)
    {
        _cubeDictionary.Values.ToList().ForEach(cube =>
        {
            if (!cube.CubeColor.Equals(color))
            {
                cube.ScaleToOriginalSize();
            }
        });
    }
}
