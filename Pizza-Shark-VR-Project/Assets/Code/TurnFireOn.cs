using UnityEngine;

public class TurnFireOn : MonoBehaviour
{
    public GameObject redFire;
    public GameObject orangeFire;
    public GameObject yellowFire;
    public int on;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void TurnOn()
    {
       on = 1;
    }

    void SetFire(GameObject fire, int cond)
    {
        ParticleSystem fireParticles = fire.GetComponent<ParticleSystem>();
        if(cond == 0){
            fireParticles.Pause();
        }
        else
        {
            fireParticles.Play();  
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetFire(redFire,on);
        SetFire(orangeFire,on);
        SetFire(yellowFire,on);
    }
}
