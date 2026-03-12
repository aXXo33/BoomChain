using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(Rigidbody2D))][RequireComponent(typeof(CircleCollider2D))]
public class MagnetStar : MonoBehaviour
{
    [SerializeField] private float radius=3,force=8,activationDelay=0.5f,duration=2;
    private bool activated,landed;private float aT,eT;private int targetTier=-1;
    private void OnCollisionEnter2D(Collision2D c){if(!landed){landed=true;targetTier=FindBest();}}
    private void Update(){if(!landed)return;if(!activated){aT+=Time.deltaTime;if(aT>=activationDelay)activated=true;return;}eT+=Time.deltaTime;if(eT>=duration){Destroy(gameObject);return;}if(targetTier<0)return;foreach(var c in Physics2D.OverlapCircleAll(transform.position,radius)){if(c.gameObject==gameObject)continue;var m=c.GetComponent<MergeObject>();if(m!=null&&m.tierIndex==targetTier){var rb=c.GetComponent<Rigidbody2D>();if(rb)rb.AddForce(((Vector2)(transform.position-c.transform.position)).normalized*force*Time.deltaTime,ForceMode2D.Force);}}transform.Rotate(0,0,180*Time.deltaTime);}
    private int FindBest(){var d=new Dictionary<int,int>();foreach(var c in Physics2D.OverlapCircleAll(transform.position,radius)){var m=c.GetComponent<MergeObject>();if(m!=null){if(!d.ContainsKey(m.tierIndex))d[m.tierIndex]=0;d[m.tierIndex]++;}}int b=-1,bc=0;foreach(var k in d)if(k.Value>bc){bc=k.Value;b=k.Key;}return b;}
}