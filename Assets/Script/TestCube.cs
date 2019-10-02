using UnityEngine;

public class TestCube : MonoBehaviour
{
    public ECubeColor CubeColor;
    
    private Vector3 _originalScale;
    private bool _isScaled = false;

    void Awake()
    {
        _originalScale = transform.localScale;
        
        CubeManager.Instance.RegisterCube(this);
        LevelManager.Instance.PlaceCube(this);
    }

    public void ToggleScale()
    {
        if (_isScaled)
        {
            ScaleToOriginalSize();
        }
        else
        {
            DoubleSize();
        }
    }

    private void DoubleSize()
    {
        transform.localScale = _originalScale * 2;
        _isScaled = true;
    }

    public void ScaleToOriginalSize()
    {
        transform.localScale = _originalScale;
        _isScaled = false;
    }

    public override string ToString()
    {
        return CubeColor.ToString();
    }
}
