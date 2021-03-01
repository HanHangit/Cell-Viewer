using System;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interaction
{
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField]
        private InputInteractionHandlerFactory _inputHandlerFactory = null;
        [SerializeField]
        private InteractObjectHandlerFactory _interactObjectHandlerFactory = null;
        [SerializeField]
        private InteractArgsCreatorFactory _interactArgsCreatorFactory = null;

        private List<InteractObject> _currentInteractObjects = new List<InteractObject>();
        private InputInteractionHandler _inputHandler = null;
        private InteractObjectHandler _interactObjectHandler = null;
        private InteractArgsCreator _interactArgsCreator = null;

        private void Start()
        {
            _inputHandler = _inputHandlerFactory.GetInteractionHandler();
            _interactArgsCreator = _interactArgsCreatorFactory.GetInteractArgsCreator();
            _interactObjectHandler = _interactObjectHandlerFactory.GetInteractObjectHandler();


            _interactObjectHandler.AddHoverBeginEventListener(HoverBeginEventListener);
            _interactObjectHandler.AddHoverUpdateEventListener(HoverUpdateEventListener);
            _interactObjectHandler.AddHoverEndEventListener(HoverEndEventListener);
            _inputHandler.AddInteractionPressedEventListener(InputPressedEventListener);
            _inputHandler.AddInteractionReleasedEventListener(InputReleasedEventListener);
        }

        private void InputReleasedEventListener()
        {
            foreach (var interactObject in _currentInteractObjects)
            {
                interactObject.EndTrigger(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void InputPressedEventListener()
        {
            foreach (var interactObject in _currentInteractObjects)
            {
                interactObject.BeginTrigger(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void HoverBeginEventListener(List<InteractObject> obj)
        {
            AddNewInteractObjects(obj);
            foreach (var interactObject in obj)
            {
                interactObject.HoverBegin(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void HoverUpdateEventListener(List<InteractObject> obj)
        {
            foreach (var interactObject in obj)
            {
                interactObject.HoverUpdate(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void HoverEndEventListener(List<InteractObject> obj)
        {
            RemoveEndInteractObjects(obj);

            foreach (var interactObject in obj)
            {
                interactObject.HoverEnd(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void AddNewInteractObjects(List<InteractObject> newObjects)
        {
            foreach (var newObject in newObjects)
            {
                if (!_currentInteractObjects.Contains(newObject))
                {
                    _currentInteractObjects.Add(newObject);
                }
            }
        }

        private void RemoveEndInteractObjects(List<InteractObject> endObjects)
        {
            foreach (var endObject in endObjects)
            {
                if (_currentInteractObjects.Contains(endObject))
                {
                    _currentInteractObjects.Remove(endObject);
                }
            }
        }
    }

    public interface InteractArgsCreator
    {
        InteractArgs GetInteractArgs();
    }

    public interface InputInteractionHandler
    {
        void AddInteractionPressedEventListener(Action callback);
        void AddInteractionReleasedEventListener(Action callback);
    }

    public interface InteractObjectHandler
    {
        void AddHoverUpdateEventListener(UnityAction<List<InteractObject>> callback);

        void AddHoverBeginEventListener(UnityAction<List<InteractObject>> callback);

        void AddHoverEndEventListener(UnityAction<List<InteractObject>> callback);
    }
}
