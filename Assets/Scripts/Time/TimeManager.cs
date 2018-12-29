using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour {
    private static TimeManager mInstance;
    public static TimeManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new TimeManager();
            }
            return mInstance;
        }
    }
    

    private float mCurrentTime = 0;
    private int mOneDayTime = 10;
    private int mMonth = 1;
    private int mDay = 1;
    private string mDayNight;
    private string mMonthStr="炎";
    private void Start()
    {
        InvokeRepeating("UpdateTime", 0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NewDay();
        }
    }


    void NewDay()
    {
        CancelInvoke("UpdateTime");
        mDay++;
        //GameEventManager.Instance.NotifySubject(GameEventType.NewDay);
        mCurrentTime = 0;
        if (mDay >= 20)
        {
            mMonth++;
            mDay = 1;
            if (mMonth % 2 != 0)
            {
                mMonthStr = "炎";
            }
            else
            {
                mMonthStr = "雪";
            }
        }
        string timemsg = mMonthStr + "    " + mDay.ToString() + "日    " + mDayNight;
        TimePanel.Instance.ShowTimeInfo(timemsg);
        InvokeRepeating("UpdateTime", 0, 1);
    }

    void UpdateTime()
    {
        
        mCurrentTime++;
        if (mCurrentTime / (mOneDayTime/2) < 1)
        {
             mDayNight = "昼";
        }
        if (mCurrentTime / (mOneDayTime/2) > 1)
        {
             mDayNight = "夜";
        }
        if (mCurrentTime / mOneDayTime >= 1)
        {
            mDay++;
            mDayNight = "昼";
            //GameEventManager.Instance.NotifySubject(GameEventType.NewDay);
            mCurrentTime = 0;
            if (mDay >= 20)
            {
                mMonth++;
                mDay = 1;
                if (mMonth % 2 != 0)
                {
                     mMonthStr = "炎";
                }
                else
                {
                     mMonthStr = "雪";
                }
            }
        }
        string timemsg = mMonthStr + "    " + mDay.ToString() + "日    " + mDayNight;
        TimePanel.Instance.ShowTimeInfo(timemsg);
    }

}
