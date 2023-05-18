using UnityEngine;
using UnityEngine.UIElements;

public class UIControl : MonoBehaviour
{
    VisualElement UIElement;
    [SerializeField] UIDocument UIDocument;
    public int elementIndex;
    public string classname;

    void UIdisplayer()
    {
        // Get the root visual element of the UIDocument
        VisualElement rootElement = UIDocument.rootVisualElement.ElementAt(0);

        // Iterate through all child elements
        foreach (VisualElement childElement in rootElement.Children())
        {
            print(childElement.name);
            // Check the type of the child element
            foreach (VisualElement element in childElement.Children())
            {
                if (element.GetType() == typeof(GroupBox) && element.name == "GroupSpacer")
                {
                    foreach (VisualElement element2 in element.Children())
                    {

                        if (element2.GetType() == typeof(GroupBox) || element2.name == "CheckMarkBox")
                        {
                            foreach (VisualElement element3 in element2.Children())
                            {
                                foreach (VisualElement element4 in element3.Children())
                                    element4.style.display = DisplayStyle.None; // Hide all the checkmarks.
                            }
                        }
                    }
                }
                if (element.GetType() == typeof(IMGUIContainer))
                {
                    // Disable visibility on the IMGUIContainer element

                    // element.style.display = DisplayStyle.None; // Disables "Gifu"
                }
            }
            // Add more conditions for other types of elements as needed
        }
    }

    void Start()
    {
        UIdisplayer();


        /*
        UIDocument.GetComponent<UIDocument>();

        UIElement = UIDocument.rootVisualElement.Q<GroupBox>();

        Debug.Log(UIElement + " " + "Element name = " + UIElement.name);

        foreach (object element in UIElement.Children())
        {
            Debug.Log("Element name is: " + element.ToString());

            if (UIElement.ElementAt(elementIndex).ClassListContains(classname))
                if (UIElement.ElementAt(elementIndex).ClassListContains("checkmark"))
                {
                    Debug.Log("Found checkmark class!");

                    foreach (object element2 in UIElement.GetClasses().ElementAt(1))
                    {
                        Debug.Log("Elements in checkmark are : " + element2);
                        foreach (object element3 in UIElement.GetClasses())
                        {
                            Debug.Log("Checkmark images are : " + element3);
                        }
                    }
                }
        }*/


    }


    //if (UIElement.GetClasses(). {
    //    Debug.Log("Hiding ui-class elements");
    //   UIElement.style.display = DisplayStyle.None;
    //}



}
