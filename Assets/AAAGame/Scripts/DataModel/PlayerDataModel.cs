using GameFramework;
using GameFramework.Event;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
public enum PlayerDataType
{
    /// <summary>
    /// ��ҽ��
    /// </summary>
    Coins,
    /// <summary>
    /// �����ʯ
    /// </summary>
    Diamond,
    /// <summary>
    /// ���Ѫ��
    /// </summary>
    Hp,
    /// <summary>
    /// �������
    /// </summary>
    Energy,
    /// <summary>
    /// �ؿ�Id
    /// </summary>
    LevelId
}

/// <summary>
/// ���������, ���/Ѫ����
/// </summary>
public class PlayerDataModel : DataModelStorageBase
{
    [JsonProperty]
    private Dictionary<PlayerDataType, int> m_PlayerDataDic;
    public int Hp
    {
        get=>GetData(PlayerDataType.Hp);
        set=>SetData(PlayerDataType.Hp, Mathf.Max(0, value));
    }
    public int Coins
    {
        get => GetData(PlayerDataType.Coins);
        set => SetData(PlayerDataType.Coins, Mathf.Max(0, value));
    }
    /// <summary>
    /// �ؿ�
    /// </summary>
    public int LevelId
    {
        get => GetData(PlayerDataType.LevelId);
        set
        {
            var lvTb = GF.DataTable.GetDataTable<LevelTable>();
            int nextLvId = Const.RepeatLevel ? value : Mathf.Clamp(value, lvTb.MinIdDataRow.Id, lvTb.MaxIdDataRow.Id);
            SetData(PlayerDataType.LevelId, nextLvId);
        }
    }
    public PlayerDataModel()
    {
        m_PlayerDataDic = new Dictionary<PlayerDataType, int>();
    }
    protected override void OnCreate(RefParams userdata)
    {
        base.OnCreate(userdata);
        GF.Event.Subscribe(GFEventArgs.EventId, OnGFEventCallback);
    }


    protected override void OnRelease()
    {
        base.OnRelease();
        GF.Event.Unsubscribe(GFEventArgs.EventId, OnGFEventCallback);
    }

    private void OnGFEventCallback(object sender, GameEventArgs e)
    {
        var args = e as GFEventArgs;
        if(args.EventType == GFEventType.ApplicationQuit)
        {
            GF.DataModel.ReleaseDataModel<PlayerDataModel>();
        }
    }
    protected override void OnInitialDataModel()
    {
        m_PlayerDataDic[PlayerDataType.Coins] = GF.Config.GetInt("DefaultCoins");
        m_PlayerDataDic[PlayerDataType.Diamond] = GF.Config.GetInt("DefaultDiamonds");
        m_PlayerDataDic[PlayerDataType.Hp] = 100;
        m_PlayerDataDic[PlayerDataType.Energy] = 100;
        m_PlayerDataDic[PlayerDataType.LevelId] = 1;

    }
    
    public int GetData(PlayerDataType tp)
    {
        return m_PlayerDataDic[tp];
    }
    public void SetData(PlayerDataType tp, int value, bool triggerEvent = true)
    {
        int oldValue = m_PlayerDataDic[tp];
        m_PlayerDataDic[tp] = value;

        if (triggerEvent)
            GF.Event.Fire(this, PlayerDataChangedEventArgs.Create(tp, oldValue, value));
    }
}
