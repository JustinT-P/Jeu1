using UnityEngine;

public class RouteAleatoire : MonoBehaviour
{
    [SerializeField]
    GameObject[] batiments;
    [SerializeField]
    GameObject route;
    [SerializeField]
    GameObject cone;
    [SerializeField]
    GameObject borne;

    //technique inspiré de https://www.youtube.com/watch?v=xFhScBZdXxg&list=PLvcJYjdXa962PHXFjQ5ugP59Ayia-rxM3&index=5
    public Vector3 prochaineRoute;

    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            GenererRoute();
        }
            
    }


    void Update()
    {
        
    }
   
    void GenererRoute()
    {
        GameObject temp = Instantiate(route, prochaineRoute, Quaternion.identity);
        prochaineRoute = temp.transform.GetChild(3).position;
        //tout ce qui est en haut de ce commentaire est inspire du video en mentionné en haut, à partir de maintenant, tout est authentique 
        GenererBatiments(temp.transform.position);
        GenererCones(temp.transform.position);
    }

    void GenererBatiments(Vector3 position)
    {
        GameObject batimentGauche = batiments[Random.Range(0, batiments.Length)];
        GameObject batimentDroite = batiments[Random.Range(0, batiments.Length)];

        Vector3 gauchePosition = position + new Vector3(-15, 0, 10);
        Quaternion gaucheRotation = Quaternion.Euler(0, 90, 0);


        Vector3 droitePosition = position + new Vector3(15, 0, 10);
        Quaternion droiteRotation = Quaternion.Euler(0, 270, 0);


        Instantiate(batimentDroite, droitePosition, droiteRotation);
        Instantiate(batimentGauche, gauchePosition, gaucheRotation);


    }

    void GenererCones(Vector3 position)
    {
        
        Vector3 positionCone = position + new Vector3(Random.Range(-5,6), 0, Random.Range(-5, 6));
        Instantiate(cone, positionCone, Quaternion.identity);

        Vector3 positionBorne = position + new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6));

        //https://docs.unity3d.com/ScriptReference/Vector3.Distance.html docc officielle de Unity sur Vector3.Distance
        while (Vector3.Distance(positionBorne, positionCone) < 2)
        {
            positionBorne = position + new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6));
        }

        
        Instantiate(borne, positionBorne, Quaternion.identity);
    }

}


