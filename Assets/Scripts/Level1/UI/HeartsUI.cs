using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HeartsUI : MonoBehaviour
{
    public List<Image> heartList;
    public GameObject heartPrefab;
    // [SerializeField] public PlayerLife playerLife;
    [SerializeField] public PlayerMovement playerLife;
    public int indexActual;
    public Sprite fullHeart, emptyHeart;

    private void Awake()
    {
        playerLife.changeLife.AddListener(ChangeHearts);
    }

    private void ChangeHearts(int actualLife)
    {
        if (!heartList.Any())
        {
            CreateHearts(actualLife);
        }
        else
        {
            ChangeLife(actualLife);
        }
    }

    private void CreateHearts(int amountMaxLife)
    {

        for (int i = 0; i < amountMaxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);

            heartList.Add(heart.GetComponent<Image>());
        }
        indexActual = amountMaxLife - 1;
    }

    private void ChangeLife(int actualLife)
    {
        if (actualLife <= indexActual)
        {
            RemoveHearts(actualLife);
        }
        else
        {
            AddHearts(actualLife);
        }
    }

    private void RemoveHearts(int actualLife)
    {
        for (int i = indexActual; i >= actualLife; i--)
        {
            indexActual = i;
            heartList[indexActual].sprite = emptyHeart;
        }
    }

    private void AddHearts(int actualLife)
    {
        for (int i = indexActual; i < actualLife; i++)
        {
            indexActual = i;
            heartList[indexActual].sprite = fullHeart;
        }
    }

}
