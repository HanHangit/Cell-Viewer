using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interaction;
using Assets.Scripts.Interaction.Bhvrs.Game;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class InteractGame_SamePositionTests
    {
        private IObjectPosition _originObject = null;
        private IObjectPosition _targetObject = null;
        private IGameTask _taskObject = null;


        [SetUp]
        public void Setup()
        {
            _originObject = Substitute.For<IObjectPosition>();
            _targetObject = Substitute.For<IObjectPosition>();
            _taskObject = Substitute.For<IGameTask>();
        }

        [Test]
        public void Interact_GameBhvr_TargetReached()
        {
            _originObject.GetPosition().Returns(new Vector3(1, 1, 1));
            _targetObject.GetPosition().Returns(new Vector3(1, 1, 1));

            GameLogic_SamePosition gameLogic = new GameLogic_SamePosition(_originObject, _taskObject, 0.0f);

            gameLogic.SetDragableObject(_targetObject);
            gameLogic.Update();

            _taskObject.Received().OnTaskSuccess();
        }

        [Test]
        public void Interact_GameBhvr_TargetNotReached()
        {
            _originObject = Substitute.For<IObjectPosition>();
            _taskObject = Substitute.For<IGameTask>();
            _targetObject.GetPosition().Returns(new Vector3(1, 1, 1));

            _originObject.GetPosition().Returns(new Vector3(0, 0, 0));

            GameLogic_SamePosition gameLogic = new GameLogic_SamePosition(_originObject, _taskObject, 0.0f);

            gameLogic.SetDragableObject(_targetObject);
            gameLogic.Update();

            _taskObject.DidNotReceive().OnTaskSuccess();
        }

        [Test]
        public void Interact_GameBhvr_TargetOffsetReached()
        {
            _originObject = Substitute.For<IObjectPosition>();
            _taskObject = Substitute.For<IGameTask>();
            _targetObject.GetPosition().Returns(new Vector3(1, 1, 1));

            _originObject.GetPosition().Returns(new Vector3(1, 1, 0));

            GameLogic_SamePosition gameLogic = new GameLogic_SamePosition(_originObject, _taskObject, 1.0f);

            gameLogic.SetDragableObject(_targetObject);
            gameLogic.Update();

            _taskObject.Received().OnTaskSuccess();
        }

        [Test]
        public void Interact_GameBhvr_SetNewGameTask()
        {
            _originObject = Substitute.For<IObjectPosition>();
            _taskObject = Substitute.For<IGameTask>();
            _targetObject.GetPosition().Returns(new Vector3(1, 1, 1));

            _originObject.GetPosition().Returns(new Vector3(1, 1, 0));

            GameLogic_SamePosition gameLogic = new GameLogic_SamePosition(_originObject, 1.0f);
            gameLogic.SetGameTask(_taskObject);
            gameLogic.SetDragableObject(_targetObject);
            gameLogic.Update();

            _taskObject.Received().OnTaskSuccess();
        }

        [Test]
        public void Interact_GameBhvr_NoGameTask()
        {
            _originObject = Substitute.For<IObjectPosition>();
            _originObject.GetPosition().Returns(new Vector3(1, 1, 0));
            _targetObject.GetPosition().Returns(new Vector3(1, 1, 1));

            GameLogic_SamePosition gameLogic = new GameLogic_SamePosition(_originObject, 1.0f);

            gameLogic.SetDragableObject(_targetObject);
            gameLogic.Update();
        }

        [Test]
        public void Interact_GameBhvr_TargetReached_False()
        {
            _originObject = Substitute.For<IObjectPosition>();
            _taskObject = Substitute.For<IGameTask>();

            _originObject.GetPosition().Returns(new Vector3(1, 1, 0));

            GameLogic_SamePosition gameLogic = new GameLogic_SamePosition(_originObject, _taskObject, 0.0f);

            gameLogic.SetDragableObject(_targetObject);
            Assert.False(gameLogic.IsTargetReached());
        }

        [Test]
        public void Interact_GameBhvr_TargetReached_True()
        {
            _originObject = Substitute.For<IObjectPosition>();
            _taskObject = Substitute.For<IGameTask>();
            _targetObject.GetPosition().Returns(new Vector3(1, 1, 1));

            _originObject.GetPosition().Returns(new Vector3(1, 1, 1));

            GameLogic_SamePosition gameLogic = new GameLogic_SamePosition(_originObject, _taskObject, 0.0f);

            gameLogic.SetDragableObject(_targetObject);
            Assert.IsTrue(gameLogic.IsTargetReached());
        }
    }
}
