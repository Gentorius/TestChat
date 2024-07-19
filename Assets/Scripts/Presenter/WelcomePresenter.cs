using Presenter.View;
using Sirenix.OdinInspector.Editor.Drawers;

namespace Presenter
{
    public class WelcomePresenter : BasicPresenter<WelcomeView>
    {
        protected override void OnShow()
        {
            View.OnClick += CloseWindow;
        }
    }
}