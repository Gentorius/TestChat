using System;
using Presenter.View;

namespace Presenter
{
    public class WelcomePresenter : BasisPresenter<WelcomeView>
    {
        public Action OnClickHandler;
    }
}