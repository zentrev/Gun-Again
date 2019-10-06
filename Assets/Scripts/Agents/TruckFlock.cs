using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckFlock : MonoBehaviour
{
    [SerializeField] float randomAngleMax = 0.1f;
    [SerializeField] float speed = 30;
    [SerializeField] float decellRate = 0.5f;
    [SerializeField] BoxCollider collisionCollider = null;
    private Vector3 forward = Vector3.right;
    private Vector3 newForward = Vector3.right;
    private float randomness = 1.0f;
    private float timer = 0.5f;

    private int timerId = -1;

    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.Instance.AgentSpeed;
        collisionCollider = GetComponent<BoxCollider>();
        timerId = TimeLease.NewTime(gameObject.GetInstanceID().ToString(), timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLease.CheckTime(timerId))
        {
            GetNewRandom();
            newForward = new Vector3(1 + randomness, 0, randomness);
        }
        forward = Vector3.Lerp(forward, newForward, timer * Time.deltaTime);
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(gameObject.transform.forward, newForward, timer * Time.deltaTime));
        gameObject.transform.position += forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            forward = new Vector3(1, 0, -forward.z * 2);
        }
    }

    void GetNewRandom()
    {
        randomness = Random.Range(-randomAngleMax, randomAngleMax);
        if (Mathf.Abs(forward.z) + Mathf.Abs(randomness) > 1.0f)
        {
            randomness = -randomness;
        }
    }

    public void Kill()
    {
        StartCoroutine("KillCore");
    }

    IEnumerator KillCore()
    {
        while(speed > 0)
        {
            speed = Mathf.Lerp(speed, 0.0f, decellRate * Time.deltaTime);
            if (speed < 0.1f) speed = 0;
            yield return null;
        }
        TimeLease.RemoveTime(timerId);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Die()
    {
        gameObject.GetComponent<EnemyDeath>().enabled = true;
        Destroy(gameObject);
    }

}
