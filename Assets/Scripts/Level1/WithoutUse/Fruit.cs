using UnityEngine;


public class Fruit : MonoBehaviour
{
    //[SerializeField] private GameObject efect;
    [SerializeField] private float amountScore;
    [SerializeField] private Score score;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            score.AddScore(amountScore);
            //Instantiate(efect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
