using UnityEngine;

public class PlantWaterable : MonoBehaviour, IWaterable, ICollectable
{
    [SerializeField] GameObject vegetation;

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
