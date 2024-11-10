using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MouvementAuto : MonoBehaviour
{
    [SerializeField]
    float vitesse = 100f;
    [SerializeField]
    float vitesseHorizontale = 5f;
    [SerializeField]
    GameObject camera;
    [SerializeField]
    Image[] coeurs;
    [SerializeField]
    Sprite coeurVide;
    [SerializeField]
    Slider slider;
    bool coroutineOn = false;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    Animator animator;

    Coroutine coroutine;

    [SerializeField]
    TextMeshProUGUI score;

    int points = 0;
    int nbvies = 3;

    void Start()
    {
        DebutCoroutine();
    }


    void Update()
    {

        if (nbvies > 0)
        {
            if (!coroutineOn)
            {
                DebutCoroutine();
            }

            if (Input.GetKey(KeyCode.Space) && slider.value > 0f)
            {
                transform.Translate(Vector3.forward * vitesse * 0.5f * Time.deltaTime);
                slider.value -= Time.deltaTime * 0.25f;
                animator.SetTrigger("Espace");
            }
            else
            {
                transform.Translate(Vector3.forward * vitesse * Time.deltaTime);
            }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.rotation = Quaternion.Euler(0, 345, 0);
                    transform.Translate(Vector3.left * vitesseHorizontale * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.rotation = Quaternion.Euler(0, 15, 0);
                    transform.Translate(Vector3.right * vitesseHorizontale * Time.deltaTime);

                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                camera.transform.position = transform.position + new Vector3(-transform.position.x, 2.5f, -5f);


        }
        else
        {
            SceneManager.LoadScene("Depart");
        } 
            
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cone"))
        {
            points += 100;
            score.text = "Score : " + points;
            if (vitesse > 25)
            {
                vitesse *= 1.02f;
            }
            else
            {
                vitesse *= 1.05f;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Borne"))
        {
            nbvies--;
            coeurs[nbvies].sprite = coeurVide;
            Destroy(collision.gameObject);
            audioSource.Play();
        }
    }

    private IEnumerator TempsSlowMo()
    {
        while (slider.value < 1)
        {
            //20 secs pour max 
            slider.value += Time.deltaTime * 0.05f; 
            yield return null;
        }
        coroutineOn = false;
    }

    void DebutCoroutine()
    {
        coroutineOn = true;
        coroutine = StartCoroutine("TempsSlowMo");
        
    }




}
