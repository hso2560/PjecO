using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkManager : MonoBehaviour
{
    private Dictionary<int, string[]> talkData;
    private Dictionary<int, Sprite> portraitData;
    [SerializeField] private Sprite[] portraitArr;
    [SerializeField] GameObject openMemo;
    [SerializeField] SoundManager sound;
    private QuestSpotManager qsm;
    public bool isMemo = false;
    private GameManager manager;
    [SerializeField] GameManager2 mng2;
    private PlayerMove player;
    public int pickup;
    UIEffect uiMng;
    CardMng cardMng;
    public string variText = "";
    public GameObject EndPanel;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        manager = FindObjectOfType<GameManager>();
        qsm = FindObjectOfType<QuestSpotManager>();
        cardMng = FindObjectOfType<CardMng>();
        player= FindObjectOfType<PlayerMove>();
        uiMng = FindObjectOfType<UIEffect>();

        if (GameManager.proCount != 7)
            GenerateData();
        else
            Invoke("GenerateData", 0.3f);

        pickup = Random.Range(1, 8);
    }

    public  void GenerateData()
    {
        #region 초상화 데이터
        if (GameManager.proCount == 1 || GameManager.proCount == 2 || GameManager.proCount == 8||GameManager.proCount==9||GameManager.proCount==12)
        {
            portraitData.Add(60 + 0, portraitArr[0]);
            portraitData.Add(60 + 1, portraitArr[1]);
            portraitData.Add(65 + 0, portraitArr[0]);
            portraitData.Add(65 + 1, portraitArr[1]);
            portraitData.Add(260 + 0, portraitArr[0]);
            portraitData.Add(260 + 1, portraitArr[1]);
            if (GameManager.proCount == 9)
            {
                portraitData.Add(300 + 0, portraitArr[0]);
                portraitData.Add(300 + 1, portraitArr[1]);
                portraitData.Add(300 + 2, portraitArr[2]);
            }
            portraitData.Add(340 + 0, portraitArr[0]);
            portraitData.Add(340 + 1, portraitArr[1]);
        }
        #endregion

        talkData.Add(10, new string[] { "(나갈 수 있는 <color=#0000ffff>문</color>인것 같다.)" });
        talkData.Add(20, new string[] { "(짧은 내용의 글이 적혀있는 <color=#0000ffff>메모</color>이다.)", "(한 번 읽어보자.)" });
        talkData.Add(30, new string[] { "(쓸만해 보이는 로프이다.)", "(하지만 굳이 챙길 필요는 없을 것 같다.)" });
        talkData.Add(40, new string[] { "(평범한 책상이다.)"});
        talkData.Add(50, new string[] { "(침대다.)", "(내가 깨어났을 때, <color=#0000ffff>여기에</color> 누워있었다.)" });
        talkData.Add(80, new string[] { "(깨진 액자이다.)", "(<color=#ff0000ff>사진 속 사람들</color>은 누구이고 이게 왜 여기에 있을까?)" });
        //id가 90인 것은 이미 있다.
        talkData.Add(60, new string[] { "으..아....:1", "?!:0", "(무슨 소리가 들렸는데..):0", "흐윽....:1", "(<color=#0000ffff>위쪽</color>에서 소리가 난다.):0", "(한 번 가보자):0" });
        talkData.Add(70, new string[] { "(분명 이 너덜너덜한 문 뒤쪽에서 소리가 났다.)", "거기 누구..있나..요?", "?!!!", "그 순간, 나는 믿기지 않는 형태의 무언가를 봤다." });
        talkData.Add(11, new string[] { "(이제 여기를 <color=#000080ff>나가볼까?</color>)" });
        talkData.Add(65, new string[] {"???:0","!!!:0","엥?..:0","(분명...아까 이 방에 로프가 있었..는데..):0","....:0","왜지...:0",
            "이 때 문에서 쿵쿵거리는 소리가 났다.:0","(아!... 벌써 여기까지!..):0","(문을 막을 다른 방법은 없으려나?):0","(아니.. 애초에 저것이 문을 스스로 열 수 있을까?):0","그런 희망같지도 않은 희망도 잠시, 문이 부서졌다..:0",
        "(문이 부서졌어?!..):0","사..알...:1","...:0","리어...:1","(이..일단 여기를 필사적으로 저 녀석을 피해서 빠져나가야 돼..):0","이곳에서 15초 동안 살아남아야합니다.:0"});
        talkData.Add(100, new string[] {"(여기에 문이 있다!)","그리고 나는 문을 살짝 열어서 문 너머를 보았다.","!!!!","거기서 본 것은... 아까 같은 괴물같은 녀석들이었다.",
        "(흑.. 아까 같은게 또 있어?!)","(그것도 <color=#ff0000ff>한 마리가 아닌것 같아..</color>)","(일단 여기로 가야지 출구를 찾을 수 있을것 같은데...)",
        "(아까 그 괴물같은 경우에는 시력이 엄청 나쁜것 같았어. 그래서 멀리 있을 때는 나를 찾지 못했고)","(만약 저 괴물들도 똑같은 종류라면......)","(<color=#008000ff>이곳을 지나가볼만 하겠어!</color>)","이 때의 나는 인지하지는 못했지만 그 상황에서도 매우 냉정했었다."});
        talkData.Add(101, new string[] {"(이제 모든 준비가 끝났다.)"});
        talkData.Add(110, new string[] {"?","(여기에도 무슨 글이 써져있는 종이가 있네)","'그러게 너는 왜 Ⅹ∇∑ ∬…¿ξ★■◐?'라고 적혀있었다.","(중간에 내용이 지워져있네)","(누구에게 말하는 걸까?)"});
        talkData.Add(115, new string[]{"(또 있네?)", "'처럼 보일거야. 물론 <color=#ff0000ff>부작용</color>은 있어. 근데 아마 너도 한 번은 느꼈을 것 같은데? 후후...'라고 적혀있다.", "(이번에는 앞의 내용이 조금 잘려있네)","(그건 그렇고 부작용이라니..)",
         "(도대체 무슨 이야기를 하는거지?)","(궁금하긴 하지만 일단 지금은 내 안전이 최우선이야)"});
        talkData.Add(1000, new string[] {"이곳은 첫번째 저장 구간입니다. 지금까지의 플레이 기록을 저장하시겠습니까?"});  //게임 저장
        talkData.Add(120, new string[]{"(이번엔 또 무슨 내용이지?)","'잘도 여기까지 왔네? 대단해 대단해.. 나는 아주 감탄했다고! 여기까지 온 보상으로 내가 희망을 조금 심어줄게'",
        "'기억.. 되찾고 싶지? ㅋㅋㅋ 그러면 나한테 와봐. 근처에 있으니까. 근데 괜찮겠어? 너가 저지른 일을 기억해내도?'라고 적혀있다","(기억?? 그거야 되찾고 싶지.. 아주 답답했는데.. 근데 이거 설마...)",
        "(<color=#0000ffff>누군가에 의해서</color> 나는 이딴일을 당하고 있는거야?)","(이 글쓴이는 내가 무슨 상황인지 대충 파악하고 있는거 같고 그걸 또 즐기는거 같아..)","(아주 화나는 놈이야... 그리고 내가 저지른 일이라니? 꼭 내가 나쁜짓이라도 한거처럼 말하네 기분 더럽게..)"});
        talkData.Add(130, new string[]{"(열쇠를 주웠다)"});
        talkData.Add(140, new string[] { "(일단 이곳에서 나갈 수 있는 문인거같다.)", "(하지만... 이 문을 열기위해서는 열쇠가 필요해. 이 문은 힘이나 다른 도구를 써서 부술 수 있는 것도 아니야.)", "(그럼 다른 출구를 찾거나 이 문의 열쇠를 찾아봐야겠어)" });
        talkData.Add(141, new string[] {"(방금 주운 열쇠로 이 문을 열 수 있을것 같다. 이제 나가볼까?)"});
        talkData.Add(150, new string[] {"(먹으면 기운을 좀 더 낼 수 있을것 같은 사과다. 아직 신선하니까 먹어도 문제는 없을거같다.)"});
        talkData.Add(155, new string[] {"(이 칼은....어디서 본적이...)","(아! 설마.. 이 칼은 내가 처음에 봤던 그 괴물에게 찔려있었던 칼이랑 같은 종류인거 같다.)"});
        talkData.Add(1001, new string[] { "이곳은 두번째 저장 구간입니다. 지금까지의 플레이 기록을 저장하시겠습니까?" });  //저장
        talkData.Add(165, new string[] {"(신선해 물이다. 이걸 마시면 갈증이 해소될 것 같다.)"});
        talkData.Add(160, new string[] { "'이것이 <color=#ff0000ff>질병</color>? 아니, 그저 성격일 뿐이야.'라고 적혀있다.", "(이 글은 내가 아닌 다른 누군가에게 전하는 것 같다.)"});
        talkData.Add(175, new string[]{"이건...", "(화살이다. <color=#0000ffff>활</color>과 함께 이용하면 매우 유용하겠어.)" });
        talkData.Add(180, new string[]{"(여기에 활이 있네.)", "(만약 <color=#0000ffff>화살</color>도 있다면 좋은 무기가 되겠어)" });
        talkData.Add(170, new string[] { "(다음 구역으로 넘어갈 수 있는 문이다)" });
        talkData.Add(171, new string[] { "(이제 다음 구역으로 넘어갈까?)" });
        talkData.Add(185, new string[]{"어??","(이건 사람의 머리카락인데..)","(그렇다면 역시.. 이곳엔 나와 같은 처지에 놓인 사람이 있는것 같아.)"});
        talkData.Add(190, new string[]{ "'축하한다. 너같은 사람은 <color=#0000ffff>오랜만</color>이야.'","'여기까지 왔으니 많이 힘들었겠지? 내가 이 구간을 넘을 수 있는 힌트를 알려줄게.'",
        "'그냥 축하선물이라고 알면 돼. 너는 일단 여기서 어떤 카드 4개를 찾아야 해.'","'카드의 색은 각각 빨,주,노,초고 각각 1000의 자리, 100의, 10의, 1의 자리를 의미해.'",
        "'무슨 의미인지 알겠어? 그냥 알고 가면 도움이 될거야. 그럼 20000'이라고 적혀있다.","(힌트? 무언가의 <color=#0000ffff>비밀번호</color>를 의미하는 건가?)",
        "(이 새끼... 사람을 아주 가지고 노네..)","(역시 이곳을 빠져나가야할 이유가 하나 더 생겼어)"});
        talkData.Add(191, new string[]{"'...이런, 내가 기획한 플레이 타임보다 훨씬 짧잖아, 내가 준비한 것도 안먹고..'","'너의 옆방에서 시작한 놈도 보통이 아니던데..아하하..'","'이번 시즌은 진짜 재밌겠네'" +
            "라고 적혀있다.","(이 메모... 나한테 전달하려는 내용은 아닌거 같다.)"});
        talkData.Add(192, new string[]{"(여기에 누군가의 신발이 떨어져있다.)","(누구의 것일까?...)"});
        talkData.Add(193, new string[] {"(칼이다.. 누군가가 이 칼로 여기에 있는 괴물같은 놈들이랑 싸운건가?)"});
        talkData.Add(194, new string[]{"(수상한 주사기다. 하지만 믿고 그냥 나한테 사용해봤더니 몸에 약간의 변화가 온것 같다.)"});
        talkData.Add(195, new string[]{"(조금 더럽지만 먹을 수 있어보이는 고기다.)"});
        talkData.Add(200, new string[] { "(화살을 주웠다)" });

        if (GameManager.proCount == 7)
        {
            talkData.Add(210, new string[] { "(<color=#ff0000ff>빨간색</color> 카드를 발견했다.)", "(이 카드에 적힌 숫자는 <color=#ff0000ff>" + cardMng.r.ToString() + "</color>이다.)" });
            talkData.Add(211, new string[] { "(<color=#DB862A>주황색</color> 카드를 발견했다.)", "(이 카드에 적힌 숫자는 <color=#DB862A>" + cardMng.o.ToString() + "</color>이다.)" });
            talkData.Add(212, new string[] { "(<color=#D4DB2B>노란색</color> 카드를 발견했다.)", "(이 카드에 적힌 숫자는 <color=#D4DB2B>" + cardMng.y.ToString() + "</color>이다.)" });
            talkData.Add(213, new string[] { "(<color=#50DB2A>초록색</color> 카드를 발견했다.)", "(이 카드에 적힌 숫자는 <color=#50DB2A>" + cardMng.g.ToString() + "</color>이다.)" });
        }

        talkData.Add(220, new string[] {"어??","(이 문은 여태까지의 문과는 다르게 비밀번호를 입력해야지 안으로 들어갈 수 있네)"});
        talkData.Add(221, new string[]{"(비밀번호를 입력해보자)"," "});
        talkData.Add(222, new string[]{"(다음 구역으로 넘어갈까?)"});
        talkData.Add(230, new string[]{"'이 글을 보는 사람은 누구일까? <color=#0000ffff>관리자</color>일까? 아니면....'","'다음 <color=#0000ffff>처벌자</color>일려나? 전자면 할 말 없고, 만약 후자라면...'","' 하고 싶은 말은 많지만 지금은 급하니까 짧게 얘기하지.'",
        "'난 이곳에 8일 간 있었고 그 만큼 알아낸게 꽤 많아. 일단 먼저 너에 대해서 설명해야겠지?'","'너의 정체는 아마도 #$*일거야. 못믿겠지만 너는 그저 <color=#ff0000ff>기억</color>을 잃었을 뿐이고'",
        "'내가 이걸 아는 이유는 관리자의 전 ㅅ*&@을 찾았고 그 안에서 단서를 찾았기 때문이고'","'그리고 너가 이 일을 당하는 것도 눈치 챘겠지만 관리자 때문이고... 정확히는 관리자의 *&%$#라는 질병때문일려나...'",
        "'어쨋든... 그저 여기를 탈출하기위해서 발악을 해! 의미없이 뒤지지말고'","(이건 이곳의 또 다른 피해자가 나에게 전달하려는 메세지인거 같다.)","(중간에 지워진 부분들이 있어서 궁금하긴 하지만...)",
        "(어차피 난 그 녀석... 아니 관리자를 족치기 위해서라도 발악을 할 생각이었어)","(여긴 무조건 빠져나간다!)"});
        talkData.Add(236, new string[] {"(누군가의 옷이다..)", "(찢어져 있고 <color=#ff0000ff>피</color>까지 묻어있어...)", "(대체.. 이곳에서 무슨일이 있었던거지?)"});
        talkData.Add(240, new string[] { "(다음 구역으로 넘어갈 수 있는 문이다(단, 넘어가기 위해서는 특정 조건에 만족해야합니다.))"});
        talkData.Add(241, new string[]{ "(이제 다음 구역으로 넘어갈까?)" });
        talkData.Add(250, new string[] {"(이 상자는 뭐지?)","(잠겨있어서 열 수 없다)","(그렇다면 이건 부숴야지)","클릭하면 상자를 부술 수 있습니다."});
        talkData.Add(1002, new string[] { "이곳은 세번째 저장 구간입니다. 지금까지의 플레이 기록을 저장하시겠습니까?" });  //저장
        talkData.Add(255, new string[] {"(알약과 캡슐약이 하나씩 들어있는 약 봉투다)","(이건 무슨 약이지?)","(혹시 모르니까 일단 챙겨야겠다)"});
        talkData.Add(260, new string[] {"(이건 무전기인데?... ..  .):0","(한 번 다른 곳과의 연결을 시도해봐야겠다):0","갑자기 무전기에 반응이 왔다:0","???  ??:0",
        "지지직....지지직......지지....치지지지지지지직........   . .  .:0","(뭐지? 이미 연결된 상태였나? 일단 말을 걸어봐야겠다.) 여보세요??:0",".....지이이익...치직....지지..직....:0",
        "(아직도 노이즈밖에 안들린다. 다시 말해봐야겠다.) 여보세요? 제 말 들리나요?:0","지직...들...지지직....지직...여기ㄴ..지직....:1","(방금 희미하게 사람 목소리가 들렸다!!)노이즈가 심해서 잘 안들립니다. 다시 한 번...:0",
        "지금 당장!!!... 지지지직.........지직...가라고!!!!:1","???:0","여긴..૦ ૧ ૨ ૩ ૪ ૫ ૬ ૭ ૮ ૯и й к ឍ ណ ត ថ ទ ធ ន ប ផ ភ ម យ ល:1","(갑자기 노이즈가 엄청 심해졌다..):0",
        "ᘩ ᘪ ᘫ ᘬ ᘭ  ઇ ઈ ઉ ઊड ढ ण त द न ऩ प फ ब ޑ ޒ ޓ ޔ  ఏ ఐ ఒ ఓ ఔ క ఖ గ ఘ ఙ:1","ఫ బ భ మ య ర ఱ ల..지지직........지직......:1","무전기 연결이 끊어졌다:0",
        "(뭐지... 중간에 노이즈때문에 잘 못들었지만 분명히 가라고 했는데.... 갑자기 불길한 예감이 확 들기시작했다.):0"});
        talkData.Add(270, new string[] {"돌덩이를 던질까?"});
        talkData.Add(280, new string[] {"화살을 챙길까?"});
        talkData.Add(290, new string[] { "뭐 좀 먹어서 체력을 회복시킬까?" });
        talkData.Add(300, new string[]{"저기....:0",".....:1","(말이 없네.. 한 번 일방적으로 질문해보자)저기, 혹시 여기가 어디인지 아세요? 버스타고 있었는데 갑자기 기절했었고 " +
            "눈 떠보니 제가 이곳에 있더라고요..:0","근데 여기에는 무슨 이상한...:0","하....하아....:1","(???)뭐라고 하셨나요?:0","흐으으.....흐흐....하아....즈....어...:1","네?:0","....주.욱....어:1",
        "!!!?:0","크...흐.....하...하......아....:2","(이건 또 뭐야?!):0","(아무래도 이 녀석도 괴물인것 같아... 아주 끝이 없네.. 후....):0","새끼가....와봐!:0"});
        talkData.Add(310, new string[]{"(빨리 나가자)"});
        talkData.Add(1003, new string[]{ "축하합니다. 이곳은 네번째 저장 구간입니다. 지금까지의 플레이 기록을 저장하시겠습니까?" });
        talkData.Add(320, new string[]{"이제 간다"});
        talkData.Add(330, new string[] {"(음? 길을 잘못 찾았나?)","(여기서부터는 길이 없는데...)","(돌아가서 다시 길을 찾아봐야겠네..)"});
        talkData.Add(340, new string[]{"(? 이번엔 이상한 가면을 쓰고있는 사람이 있네. 얘도 적인가?):0","(아까전까지는 없었는데 어느새 나타난거지?):0","이봐!...요.. 여기서 나가는 방법 알아요?(알 리가 없겠지만):0","....:1",
        "(분명 아까 그 흰옷의 괴물도 내 말을 무시하다가 갑자기 공격했었지...):0","(그냥 여기는 먼저 공격을 하자):0","아야:1","이거 생각보다 아프네~:1","?!(제대로 말을 할 줄 아는걸 보면 진짜 사람인가? 그런데 왜이렇게 분위기가 이상하지?..):0",
        "(게다가 화살을 맞고도 거의 멀쩡하고... 뭐야 대체...):0","이봐!! 왜 갑자기 사람한테 화살을 쏘는거죠?:1","아.. 이건... 죄..죄송합니다:0","아까부터 저를 <color=#0000ffff>공격하는 존재</color>가 있어가지고 그만...:0","...궁금한게 있는데 왜 앞으로 갔다가 다시 돌아오는거죠?:1",
        "앞에는 <color=#0000ffff>길</color>이 없어요:0","그럴리가.. 당신이 잘못본거죠. 앞에는 <color=#0000ffff>길</color>이 있는데:1","?? 아니.. 분명히 없는데...:0","아뇨... 앞에는 확실히 <b>길</b>이 있으니까.....:1","지금 <color=#0000ffff>저 녀석들</color>이 이쪽으로 오고있는거죠:1","?!?:0","으악! 뭐야! 길은 없었는데 도대체 어디서?!:0",
        "푸훕.. <b>길</b>을 못본거죠... <color=#ff0000ff>부작용</color>때문에..아마 잘못본건 이번이 <i>처음이 아닐텐데</i>..:1","당신도 슬슬 <color=#ff0000ff>그 녀석들</color>처럼 될겁니다.:1"," 그게 무슨....도대체 내가 왜 이런 일을 당해야하는거지....:0","기억을 되찾고도 그런 <color=#ff0000ff>뻔뻔한</color> 소리가 나오는지 궁금하군:1",
        "으윽...뭔...소리야...:0","일단 <color=#50DB2A>약속</color>은 했으니까 <color=#0000ffff>기억</color>은 되살려줄게:1","너가 <color=#ff0000ff>사진 속의 그 사람들</color>을 모두 <color=#ff0000ff>죽인 기억</color>을...:1","으윽...:0"});

        //Debug.Log("Test");
    }
 
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            if (id == 20)
            {
                isMemo = true;
                openMemo.SetActive(true);
            }
           
            else if (id == 60)
            {
                qsm.DataChange();
               
            }
            else if(id==70)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.id = 71;
                Invoke("AutoMove", 0.8f);
            }
            else if(id==65)
            {
                qsm.DataChange2();
                GameManager.qCount = 6;
        
            }
            else if(id==100)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.id = 101;
            }
            else if(id==130)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.SetActive(false);
                mng2.Exit1();
                
            }
            else if(id==150 || id==195 || id==194)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.SetActive(false);
                if (id == 150)
                    player.HpManage = 80;
                else if (id == 195)
                {
                    player.HpManage = 60;
                    if (GameManager.proCount == 8)
                        GameManager.helpNum = 2;
                }
                else
                    player.IncMaxHp(15);
            }
            else if(id==165)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.SetActive(false);
                player.HpManage = 30;
                if(GameManager.proCount==6)
                   GameManager.helpNum = 1;
            }
            else if(id==175)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.SetActive(false);
                manager.b += 1;
            }
            else if(id==180)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.SetActive(false);
                manager.b += 1;
            }
            else if(id==200)
            {
                player.ArrowManage = pickup;
                uiMng.Inform();
                pickup = Random.Range(1, 8);
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.SetActive(false);  
            }
            else if(id==220)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.id = 221;
            }
            else if(id==221)
            {
               if(manager.s1== "(문이 열렸다!!)")
                {
                    ObjData objData = manager.scanObject.GetComponent<ObjData>();
                    objData.id = 222;
                }
            }
            else if(id==250)
            {
                manager.mainButt.interactable = false;
                manager.ShowBoxImage.SetActive(true);
                manager.ani.speed = 0;
            }
            else if(id==255)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.SetActive(false);
            }
            else if(id==260)
            {
                manager.soundMng.SoundStop();
                manager.NoiseImage.SetActive(false);
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                objData.gameObject.layer = 0;
            }
            else if(id==300)
            {
                ObjData objData = manager.scanObject.GetComponent<ObjData>();
                Boss boss = objData.GetComponent<Boss>();
                objData.gameObject.layer = 12;
                boss.SliderOn();
                objData.enabled = false;
                player.transform.position = new Vector2(0, 4);
                Invoke("DelayCamTa", 0.6f);
            }
            else if(id==340)
            {
                manager.GoStart = false;
                EndPanel.SetActive(true);
                Invoke("DelayEnd", 9f);
                sound.VVVV();
            }
            
            return null;
        }
        else
        {
            if (id == 221)
            {    
                    if (mng2.InputTxt.text == mng2.GetSum().ToString())
                    {
                        manager.s1 = "(문이 열렸다!!)";
                    }
                    else
                    {
                        manager.s1 = "(이건 아닌것 같고..)";
                    }
            }

            return talkData[id][talkIndex]; 
        }
    }
    
    private void DelayCamTa()
    {
        GameManager.qCount = 13;
        manager.CameraTalk();
    }

    public void CloseMemo()
    {
        openMemo.SetActive(false);
        manager.AfterMemo();
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
    void AutoMove()
    {
        manager.FirstSelf();
        GameManager.qCount = 4;
    }

    private void DelayEnd()
    {
        GameManager.sceneNum = 13;
        PlayerPrefs.SetInt("SCENENUM", GameManager.sceneNum);
        PlayerPrefs.SetString("CLEAR", "clear");
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
    }
}
