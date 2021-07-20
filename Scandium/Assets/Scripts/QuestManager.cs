using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private GameManager manager;
    private TalkManager talkManager;
    [SerializeField] private GameObject qBook;
    [SerializeField] private GameObject qClose;
    [SerializeField] private Text qText;
    [SerializeField] private Text Title;
    private bool isOpen = false;
    private Dictionary<int, string[]> q;

    private void Awake()
    {
        Debug.Log(GameManager.proCount + ", " + GameManager.qCount + ", " + GameManager.saveCount+", "+GameManager.sceneNum);
        manager = FindObjectOfType<GameManager>();
        talkManager = FindObjectOfType<TalkManager>();
        q = new Dictionary<int, string[]>();
        DataGrt();
    }

    void DataGrt()
    {
        q.Add(0, new string[2] { "둘러보기", "내 가장 최근의 기억은 분명 어떤 <color=#0000ffff>버스 안</color>이었다. 그리고 의식이 돌아왔을 때는 처음보는 이곳에 있었다. 한 번 여기를 둘러보자." });
        q.Add(1, new string[2] { "나가기", "여기서 가져갈만 한 것은 없어보인다. 이제 여기를 나가자." });
        q.Add(2, new string[2] { "출구찾기", "뭔가 되게 무서운 장소에 온 것 같다. 빨리 출구를 찾아보자." });
        q.Add(3, new string[2] { "위쪽으로 가기", "방금 <color=#ff0000ff>어떤 사람(?)</color>의 소리가 들렸다. 위쪽에서 소리가 난거같다. 한 번 가보자." });
        q.Add(4, new string[2] {"도망치기","정체모를 상처 투성이의 위험해보이는 무언가를 봤다. 아까 있었던 장소로 빨리 돌아가야겠다." });
        q.Add(5, new string[2] { "밧줄찾기", "이 방의 문에는 잠글 수 있는 장치가 없다. 아까 이 방에 있었던 밧줄을 잘 이용하면 문을 막을 수는 있을것 같다." });
        q.Add(6, new string[2] { "생존하기", "괴물같은 녀석이 문을 부수고 들어왔다. 녀석을 잘 피해다니고 다시 밖으로 나가자.(<color=#0A4B00>15초</color>동안 생존)" });
        q.Add(7, new string[2] { "도망가기", "괴물같은 녀석을 어떻게든 피해서 밖으로 빠져나왔다. 이제 빨리 다른 나갈 곳을 찾아야한다." });
        q.Add(8, new string[2] {"빠져나가기","아까같은 위험한 것들이 이곳에 많이 있다. 이들에게 들키지않고 이곳을 잘 빠져나가야한다."});
        q.Add(9, new string[2] {"둘러보기2","이 곳에는 뭔가 쓸만한게 있어보인다. 쓸만한 것을 찾아보고 다음 구역으로 넘어가자."});
        q.Add(10, new string[2] {"빠져나가기2", "아까같은 위험한 것들이 이곳에 많이 있다. 이번에도 역시 끝까지 생존해서 이곳을 빠져나가자." });
        q.Add(11, new string[2]{"휴식","두 번이나 죽을 뻔한 고비를 넘겼다. 일단 여기서 쉬고, 이곳을 조금 조사하고 다시 출발하자."});
        q.Add(12, new string[2] {"대화하기", "누군가가 있다. 나와 같은 <color=#0000ffff>피해자</color>일까? 한 번 말을 걸어보자" });
        q.Add(13, new string[2] {"처치","아까 만났던 괴물들과는 다르게 생긴 놈이 위협을 한다. 녀석을 처치하자"});
        q.Add(14, new string[2]{ "<color=#ff0000ff>ଟଢଣ੫ ੬</color>", "<color=#ff0000ff>੭੮  ╗╘੯*^ꀷ⌡ꌼᛇᛈᛉ%ర ఱల&១ ២ ៣ઑᙍᙎ ᙏᕍ ᕎᕏ@ᕄᗄᗅᗆ  ᕅᒫ ᒬ ᒭᘆ ᘇ ᘈઙચછ જ ઝઞ</color>" });
        q.Add(15, new string[2]{"종료","아까의 잡졸들과는 비교가 안될 정도의 녀석을 겨우 죽였다. 조금 휴식을 하고 다시 출발하자."});
        q.Add(16, new string[2]{"돌아가기","막다른 길에 왔다. 다시 돌아가서 길을 다시 찾아야한다."});
    }

    public void ClickBook()
    {
        qBook.SetActive(true);
        isOpen = true;
        Title.text = q[GameManager.qCount][0];
        qText.text = q[GameManager.qCount][1];
    }

    public void ClickClo()
    {
        qBook.SetActive(false);
        isOpen = false;
    }

    //PC버전 전용 코드
   /* private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isOpen = !isOpen;
            qBook.SetActive(isOpen);
            Title.text = q[GameManager.qCount][0];
            qText.text = q[GameManager.qCount][1];
        }
    }*/
}
