using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardObj : MonoBehaviour
{
    private Player player;
    private ScoreManager scoreManager;
    private UIManager uiManager;
    private CardsManager cardsManager;

    private void Start()
    {
        player = ServiceLocator.GetService<Player>();
        scoreManager = ServiceLocator.GetService<ScoreManager>();
        uiManager = ServiceLocator.GetService<UIManager>();
        cardsManager = ServiceLocator.GetService<CardsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            SpriteRenderer otherSpriteRenderer = collision.GetComponent<SpriteRenderer>();
            player.UpdateSlider();

            player.SpawnCard();
            //cardsManager.SpawnCard();
            Destroy(gameObject);

            if (otherSpriteRenderer != null)
            {
                if (otherSpriteRenderer.sprite == gameObject.GetComponent<SpriteRenderer>().sprite)
                {
                    collision.GetComponent<Animator>().SetTrigger("Hide");
                    // Действия при совпадении спрайтов
                    cardsManager.NextCard();
                    //Invoke("wait", 0.3f);
                    scoreManager.ChangeValue(10);
                    player.BeatTrue();
                    PlayerPrefs.SetInt("goal1", PlayerPrefs.GetInt("goal1", 0) + 1);
                    PlayerPrefs.SetInt("goal2", PlayerPrefs.GetInt("goal2", 0) + 1);
                    PlayerPrefs.SetInt("goal3", PlayerPrefs.GetInt("goal3", 0) + 1);
                    PlayerPrefs.SetInt("goal4", PlayerPrefs.GetInt("goal4", 0) + 1);
                    //ServiceLocator.GetService<StoreLoader>().DecorAnim();
                   

                }
                else
                {
                    // Действия при отсутствии совпадения спрайтов
                    scoreManager.FinishGame();
                    player.BeatFalse();
                   //uiManager.ChangeScreen("lost");
                   player.LoseLife();
                }
            }

            
            //Destroy(collision.gameObject);
        }
    }


    void wait()
    {
        cardsManager.NextCard();
    }
}
