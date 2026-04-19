using UnityEngine;
using ZhengHua;

namespace KalinKonta.Stationery
{
    public class Stationery : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float totalHealth;
        [SerializeField] private readonly int cost = 1;

        public float TotalHealth
        {
            set => totalHealth = value;
            get => totalHealth;
        }

        public float Health
        {
            set => health = Mathf.Clamp(value, 0, totalHealth);
            get => health;
        }

        public int Cost
        {
            get => cost;
        }

        protected virtual void Start()
        {
            health = totalHealth;
        }

        public void Damage(float value)
        {
            health -= value;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<ProjectileObject>())
            {
                Damage(3); // TODO: Intergrating damage from where
            }
            else
            {
                StationerySpawner.Instance?.PlayCollisionSfx();
            }
        }
    }
}

