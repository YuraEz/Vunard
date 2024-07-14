using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public List<CardTypeSprites> cardTypes;
    [SerializeField] private GameObject cardPref;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private Transform cardSpawnPoint1;
    [SerializeField] private Transform cardSpawnPoint2;

    [Space]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerValue = 4f;
    private float timeCounter = 0f;


    [Space]
    private int cardTypeIndex;
    private int cardNumberIndex;
    private GameObject curCard;
    private GameObject curCard1;
    private GameObject curCard2;

    private void Awake()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.RemoveService(this);
    }

    public void OnStart()
    {
        SpawnCard();
        SpawnCard1();
        SpawnCard2();
    }

    private void Update()
    {
        if (!curCard)
        {
            timerText.text = "00:00";
            return;
        }

        timeCounter += Time.deltaTime;

        if (timeCounter >= 1f)
        {
            timerValue -= 1f;
            timeCounter = 0f;
        }

        timerText.text = "00:" + timerValue.ToString("00");

        if (timerValue <= 0f)
        {
            timerValue = 4f;
            print("Таймер истек");
            //SpawnCard();
            NextCard1();
        }

    }

    public void SpawnCard()
    {
        //if (curCard) Destroy(curCard);
        if (curCard) curCard.SetActive(false);

        cardTypeIndex = Random.Range(0, cardTypes.Count);
        cardNumberIndex = Random.Range(0, cardTypes[cardTypeIndex].CardSprites.Count);

        curCard = Instantiate(cardPref, cardSpawnPoint.position, Quaternion.identity);
        ChangeCardSprite();

        timerValue = 4f;
    }

    public void SpawnCard1()
    {
        if (curCard1) Destroy(curCard1);
        //if (curCard1) curCard1.SetActive(false);

        cardTypeIndex = Random.Range(0, cardTypes.Count);
        cardNumberIndex = Random.Range(0, cardTypes[cardTypeIndex].CardSprites.Count);

        curCard1 = Instantiate(cardPref, cardSpawnPoint1.position, Quaternion.identity);
        ChangeCardSprite(1);

        timerValue = 4f;
    }

    public void SpawnCard2()
    {
        //if (curCard2) Destroy(curCard2);

        cardTypeIndex = Random.Range(0, cardTypes.Count);
        cardNumberIndex = Random.Range(0, cardTypes[cardTypeIndex].CardSprites.Count);

        curCard2 = Instantiate(cardPref, cardSpawnPoint2.position, Quaternion.identity);
        ChangeCardSprite(2);

        timerValue = 4f;
    }

    public void NextCard()
    {
        timerValue = 4f;
        StartCoroutine(MoveCardToPosition(curCard1, cardSpawnPoint, 1f));
        curCard.SetActive(false);
    }

    public void NextCard1()
    {
                StartCoroutine(MoveCardToPosition1(curCard, cardSpawnPoint2, 1f));
        StartCoroutine(MoveCardToPosition(curCard1, cardSpawnPoint, 1f));
        //curCard.SetActive(false);

        curCard2.SetActive(false);
    }

    private IEnumerator MoveCardToPosition(GameObject card, Transform targetPosition, float duration)
    {
        Vector3 startPosition = card.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (card != null) card.transform.position = Vector3.Lerp(startPosition, targetPosition.position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (card != null) card.transform.position = targetPosition.position;
        //curCard = curCard1;
        curCard.SetActive(true);
        curCard.GetComponent<Animator>().enabled = false;
        curCard.GetComponent<SpriteRenderer>().sprite = curCard1.GetComponent<SpriteRenderer>().sprite;
        SpawnCard1();

    }

    private IEnumerator MoveCardToPosition1(GameObject card, Transform targetPosition, float duration)
    {
        Vector3 startPosition = card.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (card != null) card.transform.position = Vector3.Lerp(startPosition, targetPosition.position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        card.transform.position = cardSpawnPoint.position;
        //curCard = curCard1;
        curCard2.SetActive(true);
        curCard2.GetComponent<Animator>().enabled = false;
        curCard2.GetComponent<SpriteRenderer>().sprite = curCard.GetComponent<SpriteRenderer>().sprite;
        //SpawnCard1();
    }


    private void ChangeCardSprite(int index = 0)
    {
        if (index == 0) curCard.GetComponent<SpriteRenderer>().sprite = cardTypes[cardTypeIndex].CardSprites[cardNumberIndex];
        if (index == 1) curCard1.GetComponent<SpriteRenderer>().sprite = cardTypes[cardTypeIndex].CardSprites[cardNumberIndex];
        if (index == 2) curCard2.GetComponent<SpriteRenderer>().sprite = cardTypes[cardTypeIndex].CardSprites[cardNumberIndex];
    }
}
