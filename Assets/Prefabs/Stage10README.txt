必要要件
・バールからしりとりの順で破壊
・壊した時に壊した物の説明が出る。
・壊したら効果音（ガシャンみたいな）と壊れた画像への切り替え
------------------------------------------------------------------------------------------------------------------
アイテム管理票


バール(CrowBar)
取得できるアイテム。
これを取得すると、周りのオブジェクトを破壊可能になるフラグ[CanBreakObject]が立つ。

以下[CanBreakObject]==True;のみ機能。クリック時にオブジェクトの画像が変化。壊れた画像に指し変わる。（Falseのとき何かテキストが出るなりするのかは要相談）

留守番電話
クリック時Text[];
フラグ[Broken]が立つ。
フラグ[BackWord0]が立つ。


藁人形
クリック時Text[];フラグ[Broken]が立つ。
BackWord0==trueのとき、フラグ[BackWord1]が立つ。
BackWord0=falseにする。

浮世絵
クリック時Text[];フラグ[Broken]が立つ。
BackWord1==trueのとき、フラグ[BackWord2]が立つ。
BackWord1=falseにする。

映写機
クリック時Text[];フラグ[Broken]が立つ。
BackWord2==trueのとき、フラグ[BackWord3]が立つ。
BackWord2=falseにする。

金庫
クリック時Text[];フラグ[Broken]が立つ。
BackWord3==trueのとき、フラグ[BackWord4]が立つ。
BackWord3=falseにする。

公衆電話
クリック時Text[];フラグ[Broken]が立つ。
BackWord4==trueのとき、フラグ[BackWord5]が立つ。
BackWord4=falseにする。

ワライダケ
クリック時Text[];フラグ[Broken]が立つ。
BackWord5==trueのとき、フラグ[BackWord6]が立つ。
BackWord5=falseにする。

ケーブル
クリック時Text[];フラグ[Broken]が立つ。
BackWord6==trueのとき、フラグ[BackWord7]が立つ。
BackWord6=falseにする。

ルアー
クリック時Text[];フラグ[Broken]が立つ。
BackWord7==trueのとき、フラグ[BackWord8]が立つ。
BackWord7=falseにする。

朝顔
クリック時Text[];フラグ[Broken]が立つ。
BackWord8==trueのとき、フラグ[BackWord9]が立つ。
BackWord8=falseにする。

お地蔵さん
クリック時Text[];フラグ[Broken]が立つ。
BackWord9==trueのとき、壊れた地蔵の中にアイテム[お札]が出現する。

お札
持つとステージクリア可能フラグ[GetOutF**kinGHOST]が立つ。取得可能オブジェクト。

幽霊(YU-Rei)
フラグ進行を管理するオブジェクト。カメラの子オブジェクトにするとかなんとか。時々現れ、ビビらせにくる。
正しくないフラグを踏むと、BackwordとBrokenのフラグを全てFalseにする。
[GetOutF**kinGHOST]==Trueの時、触れるようになり、その状態でクリックすると、ステージクリア。

------------------------------------------------------------------------------------------------------------------
管理フラグ
CanBreakObject:バールを持つと立つフラグ。以後触ったオブジェクトにBrokenフラグを付与する。
Broken:物が壊れていることを示すフラグ。しりとりフラグと合体させてもいいが、物は触ったら壊れる設定なのでしりとりと両立しなくなる可能性がある。要相談
BackWordX:しりとり管理用フラグ。基本的には以下のロジックで動く。

if(BackWordX==true)
{
  BackWordX+1=true;
  BackWordX=false;
}
else
{
全てのBackWordフラグとBrokenフラグをFalseにする。
}

GetOutF**kinGHOST:お札を持つと立つフラグ。この状態で幽霊をクリックすると、クリア。ステージ移行。

