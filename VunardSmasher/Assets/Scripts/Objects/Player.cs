using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Button BeatBtn;
    [SerializeField] private Button changeSuitBtn;
    [SerializeField] private Button lowerCardBtn;
    [SerializeField] private Button higherCardBtn;

    [Space]
    public List<CardTypeSprites> cardTypes;
    [SerializeField] private GameObject cardPref;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private float shotForce = 3f;

    [Space]
    [SerializeField] private List<Sprite> beatBtnSprites;

    [Space]
    [SerializeField] private List<GameObject> lives;
    [SerializeField] private int livesAmount;


    [Space]
    private int cardTypeIndex;
    private int cardNumberIndex;
    private GameObject curCard;
    private int beatIndex = 0;


    public Slider slider; // Ссылка на слайдер
    public float decreaseTime = 8.0f; // Время до достижения нуля


    private Coroutine decreaseCoroutine = null;
    void Start()
    {
        num();
    }

    void num()
    {
        slider.value = slider.maxValue;
        if (decreaseCoroutine != null)
        {
            StopCoroutine(decreaseCoroutine);
        }
        decreaseCoroutine = StartCoroutine(DecreaseSliderToZero());
    }

    private IEnumerator DecreaseSliderToZero()
    {
        float elapsedTime = 0;
        float startValue = slider.value;

        while (elapsedTime < decreaseTime)
        {
            slider.value = Mathf.Lerp(startValue, 0, elapsedTime / decreaseTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = 0;
        LoseLife();
    }

    public void UpdateSlider()
    {
        if (decreaseCoroutine != null)
        {
            StopCoroutine(decreaseCoroutine);
        }
        num();
    }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void OnStart()
    {
        SpawnCard();

        BeatBtn.onClick.AddListener(Beat);
        changeSuitBtn.onClick.AddListener(ChangeSuit);
        lowerCardBtn.onClick.AddListener(LowerCard);
        higherCardBtn.onClick.AddListener(HigherCard);
    }

    private void Beat()
    {
        if (!curCard) return;
        curCard.GetComponent<Animator>().SetTrigger("shoot");
        curCard.GetComponent<Rigidbody2D>().AddForce(Vector2.up.normalized * shotForce, ForceMode2D.Impulse);
        curCard = null;
    }

    private void ChangeSuit()
    {
        if (!curCard) return;
        cardTypeIndex = Random.Range(0, cardTypes.Count);
        ChangeCardSprite();
    }

    private void LowerCard()
    {
        if (!curCard) return;
        cardNumberIndex -= 1;
        if (cardNumberIndex < 0) cardNumberIndex = cardTypes[cardTypeIndex].CardSprites.Count - 1;
        ChangeCardSprite();
    }

    private void HigherCard()
    {
        if (!curCard) return;
        cardNumberIndex += 1;
        if (cardNumberIndex >= cardTypes[cardTypeIndex].CardSprites.Count) cardNumberIndex = 0;
        ChangeCardSprite();
    }

    public void SpawnCard()
    {
        cardTypeIndex = Random.Range(0, cardTypes.Count);
        cardNumberIndex = Random.Range(0, cardTypes[cardTypeIndex].CardSprites.Count);

        curCard = Instantiate(cardPref, cardSpawnPoint.position, Quaternion.identity);
        ChangeCardSprite();
    }

    private void ChangeCardSprite()
    {
        curCard.GetComponent<SpriteRenderer>().sprite = cardTypes[cardTypeIndex].CardSprites[cardNumberIndex];
    }

    public void BeatTrue()
    {
        beatIndex++;
        if (beatIndex > 4) beatIndex = 4;
        BeatBtn.GetComponent<Image>().sprite = beatBtnSprites[beatIndex];
    }

    public void BeatFalse()
    {
        beatIndex = 0;
        BeatBtn.GetComponent<Image>().sprite = beatBtnSprites[beatIndex];
    }

    public float after;

    public void LoseLife()
    {
        if (livesAmount > 0)
        {
            livesAmount--;

            // Отключаем последнюю жизнь в списке
            lives[livesAmount].GetComponent<Life>().animator.SetTrigger("lose");

            Destroy(lives[livesAmount], after);
            num();
            // Уменьшаем количество жизней
            

            // Проверяем, остались ли жизни
            if (livesAmount == 0)
            {

                StopCoroutine(decreaseCoroutine);
                // Если жизней больше нет, вызываем функцию проигрыша
                ServiceLocator.GetService<UIManager>().ChangeScreen("lost");
                print("Проиграл");
            }
        }
    }


}