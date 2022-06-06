using UnityEngine;

public class ItemDataBased : MonoBehaviour
{
    public Item[] allItems;
    
    public static ItemDataBased instance;

    private void Awake ()
    {
        if (instance!=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ItemDataBased dans la scene");
            return;
        }

        instance = this;
    }
}
