using Zenject;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using Game.BattleSystem;

[TestFixture]
public class BattleInstallerTests : MonoBehaviour
{
    

    [UnityTest]
    public IEnumerator WhenInitialize_AndBattleInstallerIsHere_ThenViewModelBindedToPlayerPresenter()
    {
        yield return SceneManager.LoadSceneAsync("BattleForest 1");

        yield return new WaitForSeconds(1);
        var sceneContext = Object.FindObjectOfType<SceneContext>();
        Assert.IsNotNull(sceneContext, "SceneContext should be present in the scene.");

        var view = GameObject.FindObjectOfType<PlayerView>();
        var model = sceneContext.Container.Resolve<PlayerModel>();
        var presenter = sceneContext.Container.Resolve<PlayerPresenter>();

        Assert.IsNotNull(presenter.View);
        Assert.IsNotNull(presenter.Model);

        Assert.AreEqual(view, presenter.View);
        Assert.AreEqual(model, presenter.Model);
    }
    
}