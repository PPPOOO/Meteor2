using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoSingleton<TimeManager> {


    private float mCurrentTime = 0;
    private int mOneDayTime = 600;
    public int mMonth = 1;
    public int mDay = 1;
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


    public void NewDay()
    {
        mDay++;
        GameEventManager.Instance.NotifySubject(GameEventType.NewDay);
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
    }

    public void UpdateTime()
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
            GameEventManager.Instance.NotifySubject(GameEventType.NewDay);
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
