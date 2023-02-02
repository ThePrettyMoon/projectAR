using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class EventNotification
{
    protected object _sender = null;

    public object Sender
    {
        get { return _sender; }
    }

    /*
    protected object _param = null;
    public object Param
    {
        get { return _param; }
    }
    */

    public EventNotification(object sender = null, bool enableDiscard = true, params object[] parameters)
    {
        _sender = sender;
        _enableDiscard = enableDiscard;
        _params = parameters;
    }

    protected object[] _params = null;

    public object[] Params
    {
        get { return _params; }
    }

    private bool _discarded = false;

    public bool IsDiscarded
    {
        get { return _discarded; }
    }

    private bool _enableDiscard = true;

    public void Discard()
    {
        if (!_enableDiscard)
            return;
        _discarded = true;
    }

    List<EventNotificationFeature> _featureList = null;

    public void AddFeatures(params EventNotificationFeature[] features)
    {
        if (_featureList == null)
        {
            _featureList = new List<EventNotificationFeature>();
        }

        for (int i = 0; i < features.Length; ++i)
        {
            if (features[i] == null)
                continue;
            EventNotificationFeature sameFeature = _featureList.Find(x => x.GetType().Equals(features[i].GetType()));
            if (sameFeature != null)
                continue;

            features[i].InitNotification(this);
            _featureList.Add(features[i]);
        }
    }

    internal bool IsCompatibleHandler(EventHandlerInstance handlerInst)
    {
        if (_featureList == null)
            return true;

        for (int i = 0; i < _featureList.Count; ++i)
        {
            if (!_featureList[i].IsCompatibleHandler(handlerInst))
                return false;
        }

        return true;
    }

    internal void AfterHandlerExcute(EventHandlerInstance handlerInst)
    {
        if (_featureList == null)
            return;

        for (int i = 0; i < _featureList.Count; ++i)
        {
            _featureList[i].AfterHandlerExcute(handlerInst);
        }
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder(string.Format("Event: sender = {0}", GetObjStr(_sender)));
        for (int i = 0; i < _params.Length; ++i)
        {
            string typeStr = _params[i] == null ? "null" : _params[i].GetType().ToString();
            sb.Append(string.Format(", param = {0}, paramType = {1}", GetObjStr(_params[i]), typeStr));
        }
        return sb.ToString();
    }

    private string GetObjStr(object obj)
    {
        return obj == null ? "null" : obj.ToString();
    }
}

public abstract class EventNotificationFeature
{
    protected EventNotification _notification = null;

    protected internal void InitNotification(EventNotification notification)
    {
        if (_notification != null)
            return;
        _notification = notification;
    }

    internal virtual bool IsCompatibleHandler(EventHandlerInstance handlerIns)
    {
        return true;
    }

    internal virtual void AfterHandlerExcute(EventHandlerInstance handlerInst)
    {
    }
}

