using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject ballPref;
    [SerializeField] private float shotForece;
    [SerializeField] private Button shotBtn;

    [ReadOnly, SerializeField] private Rocket curBall;

    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [SerializeField] private List<Color> colors;
    public List<Sprite> colorSprites;
    public Sprite GoldenRocket;
    [SerializeField] private int colorIndex;

    [SerializeField] private float shotDelay;
    private bool canShoot = false;
    void Start()
    {
        shotBtn.onClick.AddListener(ChangeColor);
      //  PlaceNewBall();
    }

    public void PlaceNewBall()
    {
        if (curBall)
        {
            Destroy(curBall.gameObject);
        }

        curBall = Instantiate(ballPref, point1.position, Quaternion.identity).GetComponent<Rocket>();
        curBall.GetComponent<Collider2D>().isTrigger = true;
        ChangeColor();
        CanShot();
    }

    public void SpawnGoldenRocket()
    {
        Vector2 dir = point2.position - point1.position;
        if (curBall != null)
        {
            curBall.GetComponent<SpriteRenderer>().sprite = GoldenRocket;
            curBall.isGolden = true;
            curBall.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shotForece, ForceMode2D.Impulse);
            curBall.flame.Play();
        }
        else
        {
            GameObject newBall = Instantiate(ballPref, point1.position, Quaternion.identity);

             newBall.GetComponent<SpriteRenderer>().sprite = GoldenRocket;
             newBall.GetComponent<Rocket>().isGolden = true;

            newBall.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shotForece, ForceMode2D.Impulse);
            newBall.GetComponent<Rocket>().flame.Play();
        }
        Invoke("PlaceNewBall", 2);
    }

    private void ChangeColor()
    {
        if (!curBall) return; 
        int index = Random.Range(0, colors.Count);
        curBall.SetColor(colors[index]);
        curBall.GetComponent<SpriteRenderer>().sprite = colorSprites[index];
    }

    private void Shot()
    {
         if (!curBall) return;
        print("Shottttt");

        
       // GameObject newBall = Instantiate(ballPref, point1.position, Quaternion.identity);
        Vector2 dir = point2.position - point1.position;
        curBall.SetColor(curBall.color);
        curBall.isFly = true;
        curBall.GetComponent<SpriteRenderer>().sprite = curBall.GetComponent<SpriteRenderer>().sprite;
        curBall.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shotForece, ForceMode2D.Impulse);
        curBall.flame.Play();
        // Destroy(curBall, 6);
        //  newBall.GetComponent<Rocket>().SetColor(curBall.color);
        // newBall.GetComponent<Rocket>().isFly = true;
        //  newBall.GetComponent<SpriteRenderer>().sprite = curBall.GetComponent<SpriteRenderer>().sprite;

        //  newBall.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shotForece, ForceMode2D.Impulse);
        //  Destroy(newBall, 5);

        //  PlaceNewBall();
        Invoke("PlaceNewBall", 2);
        shotBtn.interactable = false;
        Invoke("CanShot", shotDelay);
    }




    private float timeCounter = 0f;  // Счётчик времени
   // private bool rotateClockwise = true; // Флаг для направления вращения
    public TextMeshProUGUI timerText; // Ссылка на TextMesh Pro компонент для отображения времени
    private float timerValue = 3f; // Значение таймера

    private void Update()
    {
        if (!curBall)
        {
            timerText.text = "00:00";
            return;        
        }

        //transform.eulerAngles = transform.eulerAngles+ new Vector3(0, 0, Time.deltaTime * rotateSpeed);
        // Обновляем счётчик времени
        timeCounter += Time.deltaTime;

        // Уменьшаем значение таймера каждую секунду
        if (timeCounter >= 1f)
        {
            timerValue -= 1f;
            timeCounter = 0f;
        }

        // Обновляем текст таймера
        //timerText.text = timerValue.ToString("0");
        timerText.text = "00:" + timerValue.ToString("00");

        // Проверяем, если таймер достиг 0
        if (timerValue <= 0f)
        {
            // Сбрасываем значение таймера на 10
            timerValue = 3f;

            // Переключаем направление вращения
            //rotateClockwise = !rotateClockwise;
            if (canShoot) Shot();
            canShoot = false;
            //  canShoot = false;
            //  Invoke("CanShot", 3);
        }

    }

    private void CanShot()
    {
        canShoot = true;
        shotBtn.interactable = true;
        timerValue = 3f;
    }
}
