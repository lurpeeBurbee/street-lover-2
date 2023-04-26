using UnityEngine;

public class PlantWaterable : MonoBehaviour, IWaterable, ICollectable
{
    [SerializeField] GameObject vegetation;
    void Start()
    {
        vegetation.SetActive(false);
    }
    public void PlantAppear()
    {
        vegetation.SetActive(true);
    }
    public void WholePlantDisappear()
    {
        gameObject.SetActive(false);
    }
    public void WaterHit()
    {
        PlantAppear();
    }
    public void Collect()
    {
        WholePlantDisappear();
    }
}
