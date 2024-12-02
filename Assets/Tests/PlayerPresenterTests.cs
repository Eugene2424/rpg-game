using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
using Game.BattleSystem;
using Game.Services;

public class PlayerPresenterTests
{
    //
    // WhenAct_AndArrange_ThenAssert
    private Mock<IPlayerView> _mockView;
    private PlayerModel _model;
    private PlayerPresenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _mockView = new Mock<IPlayerView>();
        _model = new PlayerModel(new PlayerDataService());

        _presenter = new PlayerPresenter(_model, _mockView.Object);
    }


    [Test]
    public void WhenInitialize_HpAndMpSet_ThenHpMpBarsUpdateView()
    {
        // Act
        _presenter.Initialize();

        // Assert
        _mockView.Verify(view => view.SetMaxHP(100), Times.Once);
        _mockView.Verify(view => view.SetMaxMP(50), Times.Once);
        _mockView.Verify(view => view.SetHP(100), Times.Once);
        _mockView.Verify(view => view.SetMP(50), Times.Once);
    }

    [Test]
    public void WhenDecreaseHealth_AndHpIsZero_ThenShouldBeDead()
    {
        // Act
        _model.DecreaseHealthPercent(1);

        _mockView.Verify(view => view.ShowDeadAnimation());
        Assert.IsTrue(_model.IsDead);
    }

    
}
