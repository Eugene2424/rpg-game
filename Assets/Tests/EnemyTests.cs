using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;

public class EnemyTests
{
    private Mock<IEnemyView> _mockView;
    private EnemyModel _model;
    private EnemyPresenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _mockView = new Mock<IEnemyView>();
        _model = new EnemyModel(100, 20);
        _presenter = new EnemyPresenter(_model, _mockView.Object);
    }

    [Test]
    public void WhenInitialize_AndEnemyExist_ThenUpdateHealthBar()
    {

        _presenter.Initialize();

        _mockView.Verify(view => view.SetMaxHP(100), Times.Once);
        _mockView.Verify(view => view.SetHP(100), Times.Once);
    }

    [Test]
    public void WhenDecreaseHealth_AndHpIsZero_ThenShouldBeDead()
    {
        _model.DecreaseHealth(100);

        _mockView.Verify(view => view.ShowThatDead(), Times.Once);
        Assert.IsTrue(_model.IsDead);
    }
}
