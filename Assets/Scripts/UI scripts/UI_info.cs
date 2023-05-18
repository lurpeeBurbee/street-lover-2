using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class UI_info : MonoBehaviour
{
    VisualElement infoObject;
    [SerializeField] UIDocument UIDocument;

    [SerializeField] float infoVisibilityTime, infoVisibilityMaxTime, infoVisibilityTimeMultiplier;
    public int elementIndexNumber;
    IEnumerator ShowInfo() // Show info in UI 
    {

        while (infoVisibilityTime < infoVisibilityMaxTime)
        {
            infoObject.style.display = DisplayStyle.Flex;

            yield return new WaitForSeconds(0.1f);
            infoVisibilityTime += infoVisibilityTimeMultiplier * Time.deltaTime;
        }
        infoObject.style.display = DisplayStyle.None;
        infoVisibilityTime = 0f;
        yield break;

    }
    void Start()
    {
        UIDocument.GetComponent<UIDocument>();
        //  IMGUIContainer infoObject = GetComponent<UIDocument>().<IMGUIContainer>("BookContainer");    
        infoObject = UIDocument.rootVisualElement.ElementAt(elementIndexNumber);
        foreach (VisualElement element in infoObject.Children())
        {
            if (element.name == "InfoContainer")
            {
                element.style.display = DisplayStyle.None;
            }
        }



    }
    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            if (infoObject != null)
            {
                StartCoroutine(ShowInfo());

            }
        }
    }


}
