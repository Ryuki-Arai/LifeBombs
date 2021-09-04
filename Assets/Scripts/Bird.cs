using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // 鳥のプレハブを格納する配列
    string ovjTag = "Bomb";

    public GameObject[] BirdPrefabs;
    public AudioClip atkSE;
    public AudioClip recSE;
    public AudioClip damSE;

    AudioSource audioSource;

    // 連鎖を消す最小数
    [SerializeField]
    private float removeBirdMinCount = 3;

    // 連鎖判定用の距離
    [SerializeField]
    private float birdDistance = 1.6f;

    // クリックされた鳥を格納
    private GameObject firstBird;
    private GameObject lastBird;
    private string currentName;
    List<GameObject> removableBirdList = new List<GameObject>();

    public GameObject lineObj;
    List<GameObject> lineBirdList = new List<GameObject>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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
                    firstBird = hitObj;
                    lastBird = hitObj;
                    currentName = hitObj.name;
                    removableBirdList = new List<GameObject>();
                    PushToBirdList(hitObj);
                }
            }
        };
        TouchManager.Moved += (info) =>
        {
            if (!firstBird)
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
                && hitObj != lastBird && 0 > removableBirdList.IndexOf(hitObj))
                {
                    // 距離を見る
                    float distance = Vector2.Distance(hitObj.transform.position,
                        lastBird.transform.position);
                    if (distance > birdDistance)
                    {
                        return;
                    }
                    PushToLineList(hitObj, lastBird);
                    lastBird = hitObj;
                    PushToBirdList(hitObj);
                }
                if (hitObj.tag == ovjTag && hitObj.name == currentName
                && hitObj != lastBird && 0 < removableBirdList.IndexOf(hitObj))
                {
                    PopToLineList();
                    PopToBirdList(lastBird);
                    lastBird = hitObj;
                }
            }
        };
        TouchManager.Ended += (info) =>
        {
             // リストの格納数を取り出し最小数と比較する
            int removeCount = removableBirdList.Count;
            if (removeCount >= removeBirdMinCount)
            {
                switch (firstBird.name)
                {
                    case "Bomb0":
                        float atk = 10 * removableBirdList.Count;//Mathf.Pow(2, removableBirdList.Count);
                        EnhpScript.hp -= atk;
                        audioSource.PlayOneShot(atkSE);
                        break;
                    case "Bomb1":
                        float rec = 10 * removableBirdList.Count; //Mathf.Pow(2, removableBirdList.Count);
                        MehpScript.hp += rec;
                        audioSource.PlayOneShot(recSE);
                        break;
                    case "Bomb2":
                        float dam = 10 * removableBirdList.Count; //Mathf.Pow(2, removableBirdList.Count);
                        MehpScript.hp -= dam;
                        audioSource.PlayOneShot(damSE);
                        break;
                }
                // 消す
                foreach (GameObject obj in removableBirdList)
                {
                    Destroy(obj);
                }
                // 補充
                StartCoroutine(DropBirds(removeCount));
            }

            foreach (GameObject obj in removableBirdList)
            {
                ChangeColor(obj, 1.0f);
            }
            foreach (GameObject obj in lineBirdList)
            {
                Destroy(obj);
            }
            removableBirdList = new List<GameObject>();
            lineBirdList = new List<GameObject>();
            firstBird = null;
            lastBird = null;
        };
        StartCoroutine(DropBirds(100));
    }
    private void PushToBirdList(GameObject obj)
    {
        removableBirdList.Add(obj);
        ChangeColor(obj, 0.5f);
    }
    private void PopToBirdList(GameObject obj)
    {
        removableBirdList.Remove(obj);
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
        lineBirdList.Add(line);
    }
    private void PopToLineList()
    {
        Destroy(lineBirdList[lineBirdList.Count-1]);
        lineBirdList.RemoveAt(lineBirdList.Count - 1);
    }
    private void ChangeColor(GameObject obj, float transparency)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r,
            renderer.color.g,
            renderer.color.b,
            transparency);
    }

    IEnumerator DropBirds(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // ランダムで出現位置を作成
            Vector2 pos = new Vector2(Random.Range(-4.20f, 4.20f), 20.16f);
            // ランダムで鳥を出現させてIDを格納
            int id = Random.Range(0, BirdPrefabs.Length);
            // 鳥を発生させる
            GameObject bird = (GameObject)Instantiate(BirdPrefabs[id],
                pos,
                Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward));
            // 作成した鳥の名前を変更します
            bird.name = ovjTag + id;
            // 0.05秒待って次の処理へ
            yield return new WaitForSeconds(0.05f);
        }
    }
}