using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallController : MonoBehaviour
{
    Text scoreUIP1;
    Text scoreUIP2;
    public int force;
    GameObject panelSelesai;
    Text txPemenang;
    int scoreP1;
    int scoreP2;
    public Rigidbody2D rigid;
    AudioSource audio;
    public AudioClip hitSound;
    // Start is called before the first frame update
    void Start()
    {
        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        scoreP1 = 0;
        scoreP2 = 0;
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2,0).normalized;
        rigid.AddForce(arah * force);
        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);
    }

    void TampilkanScore(){
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall(){
        transform.localPosition = new Vector2(0,0);
        rigid.velocity = new Vector2(0,0);
    }

    private void OnCollisionEnter2D(Collision2D coll){
        audio.PlayOneShot(hitSound);
        if(coll.gameObject.name == "TepiKanan"){
            scoreP1 += 1;
            TampilkanScore();
            if(scoreP1 == 5){
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Biru Pemenang!";
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(2,0).normalized;
            rigid.AddForce(arah * force);
        }
        if(coll.gameObject.name == "TepiKiri"){
            scoreP2 += 1;
            TampilkanScore();
             if(scoreP2 == 5){
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Hijau Pemenang!";
                Destroy(gameObject);
                return;
            }
            ResetBall();
            Vector2 arah = new Vector2(-2,0).normalized;
            rigid.AddForce(arah * force);
        }
        if(coll.gameObject.name == "Pemukul1" || coll.gameObject.name == "Pemukul2"){
            float sudut = (transform.position.y - coll.transform.position.y)*5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0,0);
            rigid.AddForce(arah * force * 2);
        }
    }
}
