using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JCFramework;
using UnityEngine.EventSystems;

public class CharacterHeadItem : MonoBehaviour, IPointerClickHandler
{
	public Image charImg;
	public Image jobImg;
	public Text charName;

	CharacterPO data;

	public void Init(CharacterPO charData){
		data = charData;
		charImg.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Character/"+data.img);
		jobImg.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Job/"+data.job);
		charName.text = data.name;
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log ("on Click "+data.name);
		ViewManager.getInstance ().ShowView ("StatsView");
	}

	#endregion
}

