using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Rocket : MonoBehaviour
{
    public ParticleSystem flame;
    public Color color;
  //  public GameObject spawnPart;
    [SerializeField] private AudioClip clip;


    private ScoreManager scoreManager;
    private GamingManager gamingManager;
    private JoystickController joystickController;
   // private Joystick joystick;

    private Rigidbody rb;

    public bool isFly = false;

    public bool isGolden = false;


    private void Start()
    {
        scoreManager = ServiceLocator.GetService<ScoreManager>();
        gamingManager = ServiceLocator.GetService<GamingManager>();
     //   joystickController = ServiceLocator.GetService<JoystickController>();
      //  joystick = joystickController.moveJoystick;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isFly) return;
   //     // Получаем движение от джойстика
   //   //  Vector3 movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
   //
   //     // Если есть движение
   // //    if (movement.sqrMagnitude > 0)
   //     {
   //         // Нормализуем вектор движения
   //         movement.Normalize();
   //
   //         // Вычисляем новое положение
   //         Vector3 newPosition = transform.position + movement * joystickController.moveSpeed * Time.deltaTime;
   //
   //         // Обновляем позицию объекта
   //         transform.position = newPosition;
   //
   //         // Вычисляем направление взгляда
   //         // Quaternion lookRotation = Quaternion.LookRotation(movement);
   //
   //         // Плавно вращаем объект в направлении движения
   //         // joystickController.transformRig.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * joystickController.rotationSpeed);
   //
   //         // Вычисляем направление взгляда по оси Z
   //         float angle = joystick.Horizontal * joystickController.rotationSpeed * 900.0f;
   //
   //         // Получаем текущий угол по оси Z
   //         float currentAngle = transform.eulerAngles.z;
   //
   //         // Обновляем угол
   //         float newAngle = currentAngle + angle * Time.deltaTime;
   //
   //         // Создаем вращение вокруг оси Z
   //         Quaternion lookRotation = Quaternion.Euler(0, 0, -newAngle);
   //
   //         // Плавно вращаем объект в направлении движения, только по оси Z
   //         //joystickController.transformRig.rotation = lookRotation;
   //         joystickController.transformRig.rotation = Quaternion.Lerp(joystickController.transformRig.rotation, lookRotation, Time.deltaTime * joystickController.rotationSpeed);
   //     }

    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            print("Коснулся");
        }
        // if (collision.collider.gameObject.tag == "win")
        // {
        //     ServiceLocator.GetService<UIManager>().ChangeScreen("win");
        //     scoreManager.Finish(true);
        // }
        if (collision.TryGetComponent(out Part part))
        {

            Destroy(gameObject);
            if (color == part.curColor || isGolden)
            {
                if (part.partLife - 1 > 0)
                {
                    part.partBroke.SetActive(true);
                    part.partLife -= 1;
                }
                else if (part.partLife - 1 == 0)
                {
                    scoreManager.ChangeValue(25);
                    if (part.hasAbility)
                    {
                        //Если способка
                        print("Use ability");
                        gamingManager.AddAbility();
                        // CircleManager.instance.UseAbility(transform.position);
                    }
                    part.gameObject.SetActive(false);
                    PlayerPrefs.SetInt("Goal1", PlayerPrefs.GetInt("Goal1", 0) + 1);
                    //goalamount1
                }
                if (clip) ServiceLocator.GetService<AudioManager>().PlaySound(clip);
            }
            else
            {
                gamingManager.ChangeLives(-1);
            }
            if (clip) ServiceLocator.GetService<AudioManager>().PlaySound(clip);
        }
       
    }



    public void SetColor(Color color)
    {
        this.color = color;
        //GetComponent<SpriteRenderer>().color = color;
    }
}
