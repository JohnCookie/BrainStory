using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JCFramework;
using UnityEngine.EventSystems;

public class CardCollectionItem : MonoBehaviour, IPointerClickHandler
{
	public Text costLabel;
	public Text cardName;
	public Image cardImg;
	public Text cardDesc;

	CardPO data;

	public void Init(CardPO cardData){
		data = cardData;
		//		charImg.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Character/"+data.img);
		//		jobImg.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Job/"+data.job);
		//		charName.text = data.name;
	}

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		//		Debug.Log ("on Click "+data.name);
	}

	#endregion
}

