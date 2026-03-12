using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))][RequireComponent(typeof(CircleCollider2D))]
public class GravityBomb : MonoBehaviour
{
    [SerializeField] private float radius=1.5f,force=5f;[SerializeField] private GameObject vfx;[SerializeField] private AudioClip sfx;
    private bool exploded;
    private void OnCollisionEnter2D(Collision2D c){if(!exploded&&(c.gameObject.GetComponent<MergeObject>()||c.gameObject.CompareTag("Wall")))Explode();}
    private void Explode(){exploded=true;foreach(var h in Physics2D.OverlapCircleAll(transform.position,radius)){if(h.gameObject==gameObject)continue;var m=h.GetComponent<MergeObject>();if(m){ScoreManager.Instance?.AddScore(m.baseScore);m.ReturnSelf();}else{var rb=h.GetComponent<Rigidbody2D>();if(rb&&!rb.isKinematic)rb.AddForce(((Vector2)(h.transform.position-transform.position)).normalized*force,ForceMode2D.Impulse);}}if(vfx)Destroy(Instantiate(vfx,transform.position,Quaternion.identity),2f);if(sfx)AudioSource.PlayClipAtPoint(sfx,transform.position);CameraShake.Instance?.Shake(0.3f,0.25f);Destroy(gameObject);}
}