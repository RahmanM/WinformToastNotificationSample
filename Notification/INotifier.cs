namespace ToastNotifications
{
    public interface INotifier
    {
        void ShowSuccess(string title, string text, int duration = -1, FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Fade, FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Left);
        void ShowError(string title, string text, int duration = -1, FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Fade, FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Left);
    }
}