using Presenter.View;
using Sirenix.OdinInspector.Editor.Drawers;

namespace Presenter
{
    public class WelcomePresenter : BasisPresenter<WelcomeView>
    {
        protected override void OnShow()
        {
            View.OnClick += CloseWindow;
        }
    }
}