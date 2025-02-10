using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UItoolkitControl : MonoBehaviour
{

    [SerializeField] UIDocument UiDocument;
    int iterationnumber = 0;

    [SerializeField] float infoVisibilityTime, infoVisibilityMaxTime, infoVisibilityMultiplier;
    IEnumerator ShowInfoScreen(VisualElement element)
    {
        while (infoVisibilityTime < infoVisibilityMaxTime)
        {
           element.ElementAt(0).style.display = DisplayStyle.Flex;
            yield return new WaitForSeconds(0.1f);
            infoVisibilityTime += infoVisibilityMultiplier * Time.deltaTime;
        }
        element.ElementAt(0).style.display = DisplayStyle.None;
        infoVisibilityTime = 0;
        yield break;
    }
   public void BarrelControl()
    {
 VisualElement rootElement = UiDocument.rootVisualElement.ElementAt(0);
        foreach (VisualElement childElement in rootElement.Children())
        {
            if (childElement.name == "header")
            {
                Debug.Log(childElement.name);
                foreach (VisualElement childElement2 in childElement.Children()) {
                    Debug.Log(childElement2.name);
                   if(childElement2.name == "Items")
                    {
                        foreach(VisualElement childElement3 in childElement2.Children())
                        {
                            if(childElement3.GetType() == typeof(IMGUIContainer))
                            {

                                childElement2.ElementAt(iterationnumber).style.display = DisplayStyle.Flex;
                                iterationnumber++;
                                break;
                            }
                            
                        }
                    }
                }

            }
            if(childElement.name == "main" && childElement.ElementAt(0).style.display != DisplayStyle.Flex)
                StartCoroutine(ShowInfoScreen(childElement));
        
        }
    }




}
