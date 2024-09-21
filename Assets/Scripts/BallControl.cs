using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro kütüphanesini ekleyin


public class BallControl : MonoBehaviour
{
    public UnityEngine.UI.Button btn; 
    // TextMeshPro öğelerini tanımlayın
     public TextMeshProUGUI zaman, can , GameOver;
     //public UnityEngine.UI.Text Zaman, can;
    private Rigidbody rg;
    public float Hız = 1.5f;
    
    float zamanSayaci = 10;
    int canSayaci = 3;

    bool oyunDevam = true;
    bool oyunTamam = false;



    void Start()
    {
        can.text=canSayaci+"";
        
        // Rigidbody bileşenini al
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(oyunDevam&& !oyunTamam){
        // Zamanlayıcıyı güncelle
        zamanSayaci -= Time.deltaTime; //zaman sayaci= zamanmsayacı-time.deltaTime
        zaman.text = ((int)zamanSayaci).ToString(); // TextMeshPro'yu kullanarak zamanı güncelle
             //zaman.text=(int)zamanSayaci+""
        }else if(!oyunTamam){ 
            GameOver.text="Oyun Tamamlanamadı";
            btn.gameObject.SetActive(true);
        }
        if (zamanSayaci <= 0)
        {
            oyunDevam = false;
       
        }
        
    }

    void FixedUpdate()
    {
    if(oyunDevam && !oyunTamam){
        // Topun hareketini kontrol et
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
        rg.AddForce(kuvvet * Hız); // Topa kuvvet uygula
    }else{
        rg.velocity= Vector3.zero;
        rg.angularVelocity = Vector3.zero;
    }
    }

    void OnCollisionEnter(Collision cls)
    {
        Debug.Log(cls.gameObject.name);
        string objIsmi = cls.gameObject.name;

        if (objIsmi.Equals("Bitis"))
        {
          //  print("Oyun Tamamlandı");
          oyunTamam=true;
          GameOver.text = "Oyun tamamlandı,tebrikler";
          btn.gameObject.SetActive(true);
        }
        else if(objIsmi.Equals("Plane"))
        {
            canSayaci -= 1;
            can.text = canSayaci.ToString(); // TextMeshPro'yu kullanarak canı güncelle
            
            if (canSayaci == 0)
            {
                oyunDevam = false;
       
                print("Can bitti, oyun bitti!");
            }
        }
    }
}
