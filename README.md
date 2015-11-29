# SceneUtils

SceneUtils使用方法について

# SceneUtilsとは
シーン内でのオブジェクト・スクリプトの動作を状態ごとに切り分けて実装するために使用するスクリプト。

<s>// 状態管理とか書いたけど無知&&オレオレなので注意</s>
<s>// そしてこれは多分世間一般で言う状態管理ではない</s>

## SceneUtilsファイル構成
``` c
ASSETS
└─Scripts
    └─SceneUtils
    　      SceneEvent.cs
    　      SceneManager.cs
    　      SceneMonoBehaviour.cs
```

## スクリプトについて
### SceneManager.cs
- 状態の設定と遷移を行うスクリプト
![WS000000.BMP](C:\Users\sugawara\Pictures\WinShot\WS000000.BMP)

### Inspectorからの設定項目
- Initial Scene ID
> 最初に実行したい状態のScenesのElementID

- Finalize Scene ID
> SceneManager.GotoFinalScene()を実行した時に遷移する状態のElementID

- Scenes
> 使用する状態の数を指定
> SceneMonoBehaviourの実装を行ったスクリプトを設定

### 遷移方法
Inspectorで設定した状態を遷移するにはSceneManagerのインスタンスから遷移用のMethodを実行する
##### インスタンスの取得
```c
SceneManager.GetInstance()
```
##### 遷移用Method
```c
// Scenesに設定した順番で次の状態に遷移
SceneManager.GotoNextScene()
// Scenesに設定した順番で前の状態に遷移
SceneManager.GotoPrevScene()
// Scenesに設定した順番で指定したIDの状態に遷移
SceneManager.GotoSceneById(int)
// FinalizeSceneに設定したIDの状態に遷移
SceneManager.GotoFinalScene()
// 状態遷移を終了
SceneManager.GotoEnd()
```
これとは別にSceneMonoBehaviourで実装できる時間管理の処理(OnTimerComplete)を実行するためには(主にInitializeSceneで)以下のメソッドを使用する
```c
// Timer開始
SceneManager.StartTimer(double)
// Timer終了
SceneManager.StopTimer()
```

### SceneMonoBehaviour.cs
- SceneMonoBehaviourで使用される状態ごとの実装を行うスクリプト

#### 必須
SceneMonoBehaviour.InitializeScene()
> SceneManagerを使用し、他の状態からこの状態に遷移した時に実行される最初の処理

SceneMonoBehaviour.FinalizeScene()
> SceneManagerを使用し、この状態から他の状態に遷移する時に実行される最後の処理

#### 任意
SceneMonoBehaviour.OnTimerComplete(System.EventArgs)
> SceneManagerからStartTimerで設定した時間が経過したときに実行される処理

SceneMonoBehaviour.OnChangeScene(System.EventArgs)
> 状態が切り替わる直前に実行される処理
> 実行順：OnChangeScene→FinalizeScene→次の状態のInitializeScene

SceneMonoBehaviour.OnEndScene(System.EventArgs)
> SceneManagerからGotoEnd()と実行された時に処理される専用処理
> 他の状態には遷移せずそこで終了なる

SceneMonoBehaviour.OnStageClicked(System.EventArgs)
> 画面上がクリックされた時に実行する処理
> (主にテストなどで使用する・無効化していたかもしれない…)

### SceneEvent.cs
delegateの宣言とか

### 使用方法
SceneManagerをシーン内の適当なオブジェクトにAddし、状態ごとの実装を行ったSceneMonoBehaviourをSceneManagerのScenesに設定する。

### 実装方法
サンプルスクリプト参照
