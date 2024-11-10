using Unity.VisualScripting;
using UnityEngine;

public class DestructeurRoute : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    // detruit la route 5 secs après l'avoir touché
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Auto"))
        {
            Destroy(gameObject, 5f);
        }
    }
}

