using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bird : MonoBehaviour
{


    // 鳥のプレハブを格納する配列
    string ovjTag = "Bomb";

    public GameObject[] BombPrefabs;
    public GameObject Boomb;
    public AudioClip atkSE;
    public AudioClip recSE;
    public AudioClip damSE;

    AudioSource audioSource;

    // 連鎖を消す最小数
    [SerializeField]
    private float removeBombMinCount = 3;

    // 連鎖判定用の距離
    [SerializeField]
    private float bombDistance = 1.6f;

    // クリックされた鳥を格納
    private GameObject firstBomb;
    private GameObject lastBomb;
    private string currentName;
    List<GameObject> removableBombList = new List<GameObject>();

    public GameObject lineObj;
    List<GameObject> lineBombList = new List<GameObject>();

    int Score, MaxCombo;
    public int defScore;
    public GameObject score;
    public GameObject combo;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI comboText;
    ScoreManager ScoreManage;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        score = GameObject.Find("ScoreText");
        combo = GameObject.Find("ComboText");
        scoreText = score.GetComponent<TextMeshProUGUI>();
        comboText = combo.GetComponent<TextMeshProUGUI>();
        ScoreManage = GetComponent<ScoreManager>();
        GetResult();
    }

    void Start()
    {
        TouchManager.Began += (info) =>
        {
            // クリック地点でヒットしているオブジェクトを取得
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint),
                Vector2.zero);
            if (hit.collider)
            {
                GameObject hitObj = hit.collider.gameObject;
                // ヒットしたオブジェクトのtagを判別し初期化
                if (hitObj.tag == ovjTag)
                {
                    firstBomb = hitObj;
                    lastBomb = hitObj;
                    currentName = hitObj.name;
                    removableBombList = new List<GameObject>();
                    PushToBombList(hitObj);
                }
            }
        };
        TouchManager.Moved += (info) =>
        {
            if (!firstBomb)
            {
                return;
            }
            // クリック地点でヒットしているオブジェクトを取得
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint),
                Vector2.zero);
            if (hit.collider)
            {
                GameObject hitObj = hit.collider.gameObject;

                // ヒットしたオブジェクトのtagが鳥、尚且名前が一緒、
                // 尚且最後にhitしたオブジェクトと違う、尚且リストに格納されていない
                if (hitObj.tag == ovjTag && hitObj.name == currentName
                && hitObj != lastBomb && 0 > removableBombList.IndexOf(hitObj))
                {
                    // 距離を見る
                    float distance = Vector2.Distance(hitObj.transform.position,
                        lastBomb.transform.position);
                    if (distance > bombDistance)
                    {
                        return;
                    }
                    PushToLineList(hitObj, lastBomb);
                    lastBomb = hitObj;
                    PushToBombList(hitObj);
                }
                if (hitObj.tag == ovjTag && hitObj.name == currentName && removableBombList.Count > 1
                && hitObj == removableBombList[removableBombList.Count-2] && 0 < removableBombList.IndexOf(hitObj))
                {
                    PopToLineList();
                    PopToBombList(lastBomb);
                    lastBomb = hitObj;
                }
            }
        };
        TouchManager.Ended += (info) =>
        {
             // リストの格納数を取り出し最小数と比較する
            int removeCount = removableBombList.Count;
            PushScore(removeCount);
            ScoreManage.SetLen(removeCount);
            if (removeCount >= removeBombMinCount)
            {
                switch (firstBomb.name)
                {
                    case "Bomb0":
                        float atk = 10 * removableBombList.Count;//Mathf.Pow(2, removableBombList.Count);
                        EnhpScript.hp -= atk;
                        audioSource.PlayOneShot(atkSE);
                        break;
                    case "Bomb1":
                        float rec = 10 * removableBombList.Count; //Mathf.Pow(2, removableBombList.Count);
                        MehpScript.hp += rec;
                        audioSource.PlayOneShot(recSE);
                        break;
                    case "Bomb2":
                        float dam = 10 * removableBombList.Count; //Mathf.Pow(2, removableBombList.Count);
                        MehpScript.hp -= dam;
                        audioSource.PlayOneShot(damSE);
                        break;
                }
                // 消す
                foreach (GameObject obj in removableBombList)
                {
                    Vector3 pos = obj.transform.position;
                    Destroy(obj);
                    Instantiate(Boomb,pos, Quaternion.identity);
                }
                // 補充
                StartCoroutine(DropBombs(removeCount));
            }

            foreach (GameObject obj in removableBombList)
            {
                ChangeColor(obj, 1.0f);
            }
            foreach (GameObject obj in lineBombList)
            {
                Destroy(obj);
            }
            removableBombList = new List<GameObject>();
            lineBombList = new List<GameObject>();
            firstBomb = null;
            lastBomb = null;
        };
        StartCoroutine(DropBombs(100));
        
    }
    private void Update()
    {
        GetResult();
        scoreText.text = Score.ToString("N0");
        comboText.text = MaxCombo.ToString();
    }
    private void PushToBombList(GameObject obj)
    {
        removableBombList.Add(obj);
        ChangeColor(obj, 0.5f);
    }
    private void PopToBombList(GameObject obj)
    {
        removableBombList.Remove(obj);
        ChangeColor(obj, 1.0f);
    }
    private void PushToLineList(GameObject lastObj, GameObject hitObj)
    {
        GameObject line = (GameObject)Instantiate(lineObj);
        LineRenderer renderer = line.GetComponent<LineRenderer>();
        //線の太さ
        renderer.startWidth = 0.1f;
        renderer.endWidth = 0.1f;
        //頂点の数
        renderer.positionCount = 2;
        //頂点を設定
        renderer.SetPosition(0, new Vector3(lastObj.transform.position.x,
            lastObj.transform.position.y, -1.0f));
        renderer.SetPosition(1, new Vector3(hitObj.transform.position.x,
            hitObj.transform.position.y, -1.0f));
        lineBombList.Add(line);
    }
    private void PopToLineList()
    {
        Destroy(lineBombList[lineBombList.Count-1]);
        lineBombList.RemoveAt(lineBombList.Count - 1);
    }
    private void ChangeColor(GameObject obj, float transparency)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r,
            renderer.color.g,
            renderer.color.b,
            transparency);
    }
    private void PushScore(int Length)
    {
        int pScore = 0;
        if (Length < 4) pScore = Length * defScore;
        else if (Length < 8) pScore = Length * defScore + Length * 200;
        else if (Length < 11) pScore = Length * defScore + 1500;
        else if (Length < 15) pScore = Length * defScore + 2000;
        else if (Length < 20) pScore = Length * defScore + 2500;
        else if (Length < 30) pScore = Length * defScore + 3000;
        else if (Length >= 30) pScore = Length * defScore + 3500;
        ScoreManage.SetScore(pScore);
    }
    private void GetResult()
    {
        Score = ScoreManage.GetScore();
        MaxCombo = ScoreManage.GetCombo();
    }

    IEnumerator DropBombs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // ランダムで出現位置を作成
            Vector2 pos = new Vector2(Random.Range(-4.20f, 4.20f), 20.16f);
            // ランダムで鳥を出現させてIDを格納
            int id = Random.Range(0, BombPrefabs.Length);
            // 鳥を発生させる
            GameObject bomb = (GameObject)Instantiate(BombPrefabs[id],
                pos,
                Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward));
            // 作成した鳥の名前を変更します
            bomb.name = ovjTag + id;
            // 0.05秒待って次の処理へ
            yield return new WaitForSeconds(0.05f);
        }
    }
}