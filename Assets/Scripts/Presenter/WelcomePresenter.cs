using Attributes;
using Presenter.View;
using Utility.DependencyInjection;

namespace Presenter
{
    public class WelcomePresenter : BasicPresenter<WelcomeView>
    {
        [Inject]
        private ChatPresenter _chatPresenter;
        [Inject]
        private DIContainer _diContainer;
        protected override void OnShow()
        {
            _diContainer.InjectDependencies(View.UserBasicProfileWidget);
            View.OnClick += CloseWindow;
        }

        protected override void OnHide()
        {
            _chatPresenter.OpenWindow();
        }
    }
}