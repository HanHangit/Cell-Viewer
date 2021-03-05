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

        private List<InteractHoverObject> _currentHoverObjects = new List<InteractHoverObject>();
        private List<InteractSelectionObject> _currentSelectionObjects = new List<InteractSelectionObject>();
        private InputInteractionHandler _inputHandler = null;
        private InteractObjectHandler _interactObjectHandler = null;
        private InteractArgsCreator _interactArgsCreator = null;

        private void Start()
        {
            _inputHandler = _inputHandlerFactory.GetInteractionHandler();
            _interactArgsCreator = _interactArgsCreatorFactory.GetInteractArgsCreator();
            _interactObjectHandler = _interactObjectHandlerFactory.CreateInteractObjectHandler();

            _interactObjectHandler.AddHoverBeginEventListener(HoverBeginEventListener);
            _interactObjectHandler.AddHoverUpdateEventListener(HoverUpdateEventListener);
            _interactObjectHandler.AddHoverEndEventListener(HoverEndEventListener);
            _inputHandler.AddInteractionPressedEventListener(InputPressedEventListener);
            _inputHandler.AddInteractionReleasedEventListener(InputReleasedEventListener);
        }

        private void InputReleasedEventListener()
        {
            foreach (var interactObject in _currentSelectionObjects)
            {
                interactObject.OnEndSelection(_interactArgsCreator.GetInteractArgs());
            }
            _currentSelectionObjects.Clear();
        }

        private void InputPressedEventListener()
        {
            _currentSelectionObjects.AddRange(_interactObjectHandler.GetCurrentSelectionObjects());
            foreach (var selectionObject in _currentSelectionObjects)
            {
                selectionObject.OnBeginSelection(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void Update()
        {
            foreach (var selectionObject in _currentSelectionObjects)
            {
                selectionObject.OnUpdateSelection(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void HoverBeginEventListener(List<InteractHoverObject> obj)
        {
            AddNewInteractObjects(obj);
            foreach (var interactObject in obj)
            {
                interactObject.OnHoverBegin(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void HoverUpdateEventListener(List<InteractHoverObject> obj)
        {
            foreach (var interactObject in obj)
            {
                interactObject.OnHoverUpdate(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void HoverEndEventListener(List<InteractHoverObject> obj)
        {
            RemoveEndInteractObjects(obj);

            foreach (var interactObject in obj)
            {
                interactObject.OnHoverEnd(_interactArgsCreator.GetInteractArgs());
            }
        }

        private void AddNewInteractObjects(List<InteractHoverObject> newObjects)
        {
            foreach (var newObject in newObjects)
            {
                if (!_currentHoverObjects.Contains(newObject))
                {
                    _currentHoverObjects.Add(newObject);
                }
            }
        }

        private void RemoveEndInteractObjects(List<InteractHoverObject> endObjects)
        {
            foreach (var endObject in endObjects)
            {
                if (_currentHoverObjects.Contains(endObject))
                {
                    _currentHoverObjects.Remove(endObject);
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
        void AddHoverUpdateEventListener(UnityAction<List<InteractHoverObject>> callback);

        void AddHoverBeginEventListener(UnityAction<List<InteractHoverObject>> callback);

        void AddHoverEndEventListener(UnityAction<List<InteractHoverObject>> callback);

        List<InteractSelectionObject> GetCurrentSelectionObjects();
    }
}
