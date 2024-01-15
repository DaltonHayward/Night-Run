using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Text text;
    public float lifetime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Mathf.Abs(Random.rotation.eulerAngles.y);
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, direction, 0) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        if (timer > lifetime) Destroy(this.gameObject);
        else if (timer > fraction) text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        transform.localPosition = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifetime));
    }

    public void SetDamageText(float dmg) 
    {
        text.text = dmg.ToString();
    }

    public void ChangeColor(Color new_color) {
        text.color = new_color;
    }
}
