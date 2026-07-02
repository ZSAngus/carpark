using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using SkyInno.Lang;

namespace CarPark.DB;

[Serializable]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "CardType")]
[DataContract(IsReference = true)]
public class CardType : EntityObject
{
	private int _AntipassBack;

	private int _CardTypeID;

	private string _CardNameCn;

	private string _CardNamePt;

	private int _LPRSDisable;

	private bool _IsCheckLoop;

	private bool _IsDelete;

	public string CardName
	{
		get
		{
			string cardNameCn = CardNameCn;
			switch (LangManager.CurLanguage)
			{
			case SysLanguage.CHS:
			case SysLanguage.CHT:
				return cardNameCn;
			case SysLanguage.ENG:
			case SysLanguage.PT:
				return CardNamePt;
			default:
				return cardNameCn;
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int AntipassBack
	{
		get
		{
			return _AntipassBack;
		}
		set
		{
			ReportPropertyChanging("AntipassBack");
			_AntipassBack = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("AntipassBack");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	public int CardTypeID
	{
		get
		{
			return _CardTypeID;
		}
		set
		{
			if (_CardTypeID != value)
			{
				ReportPropertyChanging("CardTypeID");
				_CardTypeID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("CardTypeID");
			}
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string CardNameCn
	{
		get
		{
			return _CardNameCn;
		}
		set
		{
			ReportPropertyChanging("CardNameCn");
			_CardNameCn = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CardNameCn");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CardNamePt
	{
		get
		{
			return _CardNamePt;
		}
		set
		{
			ReportPropertyChanging("CardNamePt");
			_CardNamePt = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CardNamePt");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int LPRSDisable
	{
		get
		{
			return _LPRSDisable;
		}
		set
		{
			ReportPropertyChanging("LPRSDisable");
			_LPRSDisable = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("LPRSDisable");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsCheckLoop
	{
		get
		{
			return _IsCheckLoop;
		}
		set
		{
			ReportPropertyChanging("IsCheckLoop");
			_IsCheckLoop = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsCheckLoop");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			ReportPropertyChanging("IsDelete");
			_IsDelete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDelete");
		}
	}

	public static CardType CreateCardType(int cardTypeID, string cardNameCn, string cardNamePt, int antipassBack, int lPRSDisable, bool isCheckLoop, bool isDelete)
	{
		CardType cardType = new CardType();
		cardType.CardTypeID = cardTypeID;
		cardType.CardNameCn = cardNameCn;
		cardType.CardNamePt = cardNamePt;
		cardType.AntipassBack = antipassBack;
		cardType.LPRSDisable = lPRSDisable;
		cardType.IsCheckLoop = isCheckLoop;
		cardType.IsDelete = isDelete;
		return cardType;
	}
}
