using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public delegate void EventHandler(EventNotification notification);
    public class EventManager:UnitySingleton<EventManager>
    {
        private Dictionary<NotificationType, HashSet<EventHandlerInstance>> _eventListeners
        = new Dictionary<NotificationType, HashSet<EventHandlerInstance>>();

        private List<KeyValuePair<NotificationType, EventHandlerInstance>> _listenersToAdd
        = new List<KeyValuePair<NotificationType, EventHandlerInstance>>();
        private List<KeyValuePair<NotificationType, EventHandlerInstance>> _listenersToRemove
        = new List<KeyValuePair<NotificationType, EventHandlerInstance>>();

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void LateUpdate()
        {
            UpdateListeners();
        }

        public void SendSimpleEvent(NotificationType type)
        {
            if (!_eventListeners.ContainsKey(type))
                return;

            HashSet<EventHandlerInstance> handlers = _eventListeners[type];
            foreach (var handler in handlers)
            {
                if (!handler.IsExecutableHandler())
                {
                    RemoveEventListener(type, handler);
                    continue;
                }
                handler.Handler(null);
            }
        }

        public void AddEventListener(NotificationType type, EventHandler handler, IEntity receiver = null)
        {
            if (handler == null)
            {
                Debug.LogError("Add empty method as event handler!");
                return;
            }

            EventHandlerInstance handlerInst = new EventHandlerInstance(handler, receiver);
            _listenersToAdd.Add(new KeyValuePair<NotificationType, EventHandlerInstance>(type, handlerInst));
        }

        public void RemoveEventListener(NotificationType type, EventHandler handler)
        {
            if (handler == null)
                return;
            EventHandlerInstance handlerInst = new EventHandlerInstance(handler, null);
            _listenersToRemove.Add(new KeyValuePair<NotificationType, EventHandlerInstance>(type, handlerInst));
        }

        private void RemoveEventListener(NotificationType type, EventHandlerInstance handlerInst)
        {
            if (handlerInst == null)
                return;
            _listenersToRemove.Add(new KeyValuePair<NotificationType, EventHandlerInstance>(type, handlerInst));
        }

        public void SendEventNotification(NotificationType type, EventNotification notification = null, params EventNotificationFeature[] _features)
        {
            if (!_eventListeners.ContainsKey(type))
                return;

            if (notification == null)
            {
                notification = new EventNotification(null);
            }
            notification.AddFeatures(_features);

            HashSet<EventHandlerInstance> handlers = _eventListeners[type];
            foreach (var handler in handlers)
            {
                if (!handler.IsExecutableHandler())
                {
                    RemoveEventListener(type, handler);
                    continue;
                }

                if (notification.IsDiscarded)
                    break;

                if (!notification.IsCompatibleHandler(handler))
                    continue;

                try
                {
                    handler.Handler(notification);
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("Event handler exception: \ntype: {0}\n{1}\n{2}",
                        type.ToString(),
                        notification.ToString(),
                        handler.Handler.Method.ToString());
                    throw e;
                }

                notification.AfterHandlerExcute(handler);
            }
        }

        public void InstantAddEventListener(NotificationType type, EventHandler handler, IEntity  receiver = null)
        {
            if (handler == null)
            {
                Debug.LogError("Add empty method as event handler!");
                return;
            }

            if (!_eventListeners.ContainsKey(type))
            {
                _eventListeners.Add(type, new HashSet<EventHandlerInstance>());
            }

            _eventListeners[type].Add(new EventHandlerInstance(handler, receiver));
        }

        private void InstantAddEventListener(NotificationType type, EventHandlerInstance handlerInst)
        {
            if (handlerInst == null)
            {
                Debug.LogError("Add empty method as event handler!");
                return;
            }

            if (!_eventListeners.ContainsKey(type))
            {
                _eventListeners.Add(type, new HashSet<EventHandlerInstance>());
            }

            _eventListeners[type].Add(handlerInst);
        }

        public void InstantRemoveEventListener(NotificationType type, EventHandler handler)
        {
            if (!_eventListeners.ContainsKey(type))
                return;
            _eventListeners[type].Remove(new EventHandlerInstance(handler, null));
        }

        private void InstantRemoveEventListener(NotificationType type, EventHandlerInstance handlerInst)
        {
            if (!_eventListeners.ContainsKey(type))
                return;
            _eventListeners[type].Remove(handlerInst);
        }

        public void CleanNoListenerEvent()
        {
            foreach (var pair in _eventListeners)
            {
                if (pair.Value.Count > 0)
                    continue;
                _eventListeners.Remove(pair.Key);
            }
        }



        private void UpdateListeners()
        {
            for (int i = 0; i < _listenersToAdd.Count; i++)
            {
                InstantAddEventListener(_listenersToAdd[i].Key, _listenersToAdd[i].Value);
            }
            _listenersToAdd.Clear();

            for (int i = 0; i < _listenersToRemove.Count; i++)
            {
                InstantRemoveEventListener(_listenersToRemove[i].Key, _listenersToRemove[i].Value);
            }
            _listenersToRemove.Clear();
        }
    }

public class EventHandlerInstance
{
    private EventHandler _handler;
    private bool _isStatic;
    private IEntity _receiver;

    public EventHandlerInstance(EventHandler handler, IEntity receiver)
    {
        _handler = handler;
        _isStatic = handler.Method.IsStatic;
        _receiver = receiver;
    }

    public EventHandler Handler
    {
        get { return _handler; }
    }

    public bool IsExecutableHandler()
    {
        if (_isStatic)
            return true;
        if (_handler.Target == null)
            return false;
        if (_handler.Target is UnityEngine.Object && _handler.Target.Equals(null))
            return false;
        return true;
    }

    public bool IsStatic
    {
        get { return _isStatic; }
    }

    public IEntity Receiver
    {
        get { return _receiver; }
    }

    public override bool Equals(object obj)
    {
        if (obj == this)
            return true;
        if (obj is EventHandlerInstance)
        {
            EventHandlerInstance target = obj as EventHandlerInstance;
            return _handler.Equals(target.Handler);
        }
        if (obj is EventHandler)
        {
            EventHandler target = obj as EventHandler;
            return _handler.Equals(target);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return _handler.GetHashCode();
    }
}

