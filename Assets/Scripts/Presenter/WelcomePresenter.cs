using Presenter.View;
using Sirenix.OdinInspector.Editor.Drawers;

namespace Presenter
{
    public class WelcomePresenter : BasisPresenter<WelcomeView>
    {
        public override void OnShow()
        {
            View.OnClick += CloseWindow;
        }
    }
}