using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class BasePanel : MonoBehaviour {
	//皮肤路径
	public string skinPath;
	//皮肤
	public GameObject skin;
	//层级
	public PanelManager.Layer layer = PanelManager.Layer.Layer4;
	//初始化
	public void Init(){
		//皮肤
		GameObject skinPrefab = Resources.Load<GameObject>(skinPath);
		skin = (GameObject)Instantiate(skinPrefab);
	}
	//关闭
	public void Close(){
		string name = this.GetType().ToString();
		PanelManager.Close(name);
	}

	//初始化时
	public virtual void OnInit(){

	}

	//显示时
	public virtual void OnShow(params object[] para){

	}


    #region 可以重写动画的方法
    //打开动画
    public virtual void OpenAnimation()
	{
		OnShowing();
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(skin.transform.DOScale(1.1f, 0.3f)).Append(skin.transform.DOScale(1f, 0.2f)).AppendCallback(() => { OnShowed(); });
	}

	//关闭动画
	public virtual void CloseAnimation()
	{
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(skin.transform.DOScale(0f, 0.5f)).AppendCallback(() => { OnShowed(); DestroyThisPanelObj(); });
	}
    #endregion

    //销毁当前面板
    public void DestroyThisPanelObj()
	{
		string name = this.GetType().ToString();
		BasePanel panel = PanelManager.panels[name];
		//没有打开
		if (panel == null)
		{
			return;
		}
		//列表
		PanelManager.panels.Remove(name);
		//销毁
		GameObject.Destroy(panel.skin);
		Component.Destroy(panel);
	}

	#region 面板的生命周期
	/// <summary>
	/// 显示面板动画播放前
	/// </summary>
	public virtual void OnShowing() { }

	/// <summary>
	/// 显示面板动画播放后
	/// </summary>
	public virtual void OnShowed() { }

	/// <summary>
	/// 帧更新
	/// </summary>
	public virtual void Update() { }

	/// <summary>e
	/// 关闭动画播放前
	/// </summary>
	public virtual void OnClosing() { }

	/// <summary>
	/// 关闭动画播放后
	/// </summary>
	public virtual void OnClosed() { }

    #endregion

    /// <summary>
    /// 注册事件和动画
    /// </summary>
    /// <param name="button"></param>
    /// <param name="tweenCallback"></param>
    public virtual void OnClickEventForButton(Button button, TweenCallback tweenCallback)
	{
		float ScaleX = button.transform.localScale.x;
		button.onClick.AddListener(
			() =>
			{
				Sequence mySequence = DOTween.Sequence();
				mySequence.Append(button.transform.DOScale(ScaleX * 0.9f, 0.1f)).Append(button.transform.DOScale(ScaleX, 0.1f))
					.AppendCallback(tweenCallback);
			}
			);
	}

}
