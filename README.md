# SceneUtils

SceneUtils�g�p���@�ɂ���

# SceneUtils�Ƃ�
�V�[�����ł̃I�u�W�F�N�g�E�X�N���v�g�̓������Ԃ��Ƃɐ؂蕪���Ď������邽�߂Ɏg�p����X�N���v�g�B

<s>// ��ԊǗ��Ƃ����������ǖ��m&&�I���I���Ȃ̂Œ���</s>
<s>// �����Ă���͑������Ԉ�ʂŌ�����ԊǗ��ł͂Ȃ�</s>

## SceneUtils�t�@�C���\��
``` c
ASSETS
����Scripts
    ����SceneUtils
    �@      SceneEvent.cs
    �@      SceneManager.cs
    �@      SceneMonoBehaviour.cs
```

## �X�N���v�g�ɂ���
### SceneManager.cs
- ��Ԃ̐ݒ�ƑJ�ڂ��s���X�N���v�g
![WS000000.BMP](C:\Users\sugawara\Pictures\WinShot\WS000000.BMP)

### Inspector����̐ݒ荀��
- Initial Scene ID
> �ŏ��Ɏ��s��������Ԃ�Scenes��ElementID

- Finalize Scene ID
> SceneManager.GotoFinalScene()�����s�������ɑJ�ڂ����Ԃ�ElementID

- Scenes
> �g�p�����Ԃ̐����w��
> SceneMonoBehaviour�̎������s�����X�N���v�g��ݒ�

### �J�ڕ��@
Inspector�Őݒ肵����Ԃ�J�ڂ���ɂ�SceneManager�̃C���X�^���X����J�ڗp��Method�����s����
##### �C���X�^���X�̎擾
```c
SceneManager.GetInstance()
```
##### �J�ڗpMethod
```c
// Scenes�ɐݒ肵�����ԂŎ��̏�ԂɑJ��
SceneManager.GotoNextScene()
// Scenes�ɐݒ肵�����ԂőO�̏�ԂɑJ��
SceneManager.GotoPrevScene()
// Scenes�ɐݒ肵�����ԂŎw�肵��ID�̏�ԂɑJ��
SceneManager.GotoSceneById(int)
// FinalizeScene�ɐݒ肵��ID�̏�ԂɑJ��
SceneManager.GotoFinalScene()
// ��ԑJ�ڂ��I��
SceneManager.GotoEnd()
```
����Ƃ͕ʂ�SceneMonoBehaviour�Ŏ����ł��鎞�ԊǗ��̏���(OnTimerComplete)�����s���邽�߂ɂ�(���InitializeScene��)�ȉ��̃��\�b�h���g�p����
```c
// Timer�J�n
SceneManager.StartTimer(double)
// Timer�I��
SceneManager.StopTimer()
```

### SceneMonoBehaviour.cs
- SceneMonoBehaviour�Ŏg�p������Ԃ��Ƃ̎������s���X�N���v�g

#### �K�{
SceneMonoBehaviour.InitializeScene()
> SceneManager���g�p���A���̏�Ԃ��炱�̏�ԂɑJ�ڂ������Ɏ��s�����ŏ��̏���

SceneMonoBehaviour.FinalizeScene()
> SceneManager���g�p���A���̏�Ԃ��瑼�̏�ԂɑJ�ڂ��鎞�Ɏ��s�����Ō�̏���

#### �C��
SceneMonoBehaviour.OnTimerComplete(System.EventArgs)
> SceneManager����StartTimer�Őݒ肵�����Ԃ��o�߂����Ƃ��Ɏ��s����鏈��

SceneMonoBehaviour.OnChangeScene(System.EventArgs)
> ��Ԃ��؂�ւ�钼�O�Ɏ��s����鏈��
> ���s���FOnChangeScene��FinalizeScene�����̏�Ԃ�InitializeScene

SceneMonoBehaviour.OnEndScene(System.EventArgs)
> SceneManager����GotoEnd()�Ǝ��s���ꂽ���ɏ���������p����
> ���̏�Ԃɂ͑J�ڂ��������ŏI���Ȃ�

SceneMonoBehaviour.OnStageClicked(System.EventArgs)
> ��ʏオ�N���b�N���ꂽ���Ɏ��s���鏈��
> (��Ƀe�X�g�ȂǂŎg�p����E���������Ă�����������Ȃ��c)

### SceneEvent.cs
delegate�̐錾�Ƃ�

### �g�p���@
SceneManager���V�[�����̓K���ȃI�u�W�F�N�g��Add���A��Ԃ��Ƃ̎������s����SceneMonoBehaviour��SceneManager��Scenes�ɐݒ肷��B

### �������@
�T���v���X�N���v�g�Q��
