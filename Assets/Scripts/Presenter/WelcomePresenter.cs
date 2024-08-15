using Attributes;
using Presenter.View;

namespace Presenter
{
    public class WelcomePresenter : BasicPresenter<WelcomeView>
    {
        [Inject]
        private ChatPresenter _chatPresenter;
        protected override void OnShow()
        {
            View.OnClick += CloseWindow;
        }

        protected override void OnHide()
        {
            _chatPresenter.OpenWindow();
        }
    }
}