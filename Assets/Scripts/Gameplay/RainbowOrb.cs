using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))][RequireComponent(typeof(CircleCollider2D))]
public class RainbowOrb : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;[SerializeField] private GameObject vfx;[SerializeField] private AudioClip sfx;
    private bool merged;
    private void Update(){if(sr)sr.color=Color.HSVToRGB(Mathf.Repeat(Time.time*2,1),0.8f,1f);}
    private void OnCollisionEnter2D(Collision2D c){if(merged)return;var o=c.gameObject.GetComponent<MergeObject>();if(o!=null&&o.canMerge){merged=true;var pos=(Vector2)(transform.position+o.transform.position)/2f;ComboSystem.Instance?.RegisterMerge();ScoreManager.Instance?.AddScore(o.baseScore);if(o.tierIndex+1<12)MergeManager.Instance?.SpawnNextTier(o.tierIndex+1,pos);if(vfx)Destroy(Instantiate(vfx,pos,Quaternion.identity),2f);o.ReturnSelf();Destroy(gameObject);}}
}