using System;
using System.IO;

namespace ToastNotifications
{

    public class Notifier : INotifier
    {
        public void ShowSuccess(string title, string text, int duration = -1, FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Fade, FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Left)
        {
            if (duration <= 0) duration = -1;
            var toastNotification = new Notification(title, text, duration, animationMethod, animationDirection, NotificationType.Success);
            PlayNotificationSound("garden");
            toastNotification.Show();
        }

        public void ShowError(string title, string text, int duration = -1, FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Fade, FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Left)
        {
            if (duration <= 0) duration = -1;
            var toastNotification = new Notification(title, text, duration, animationMethod, animationDirection, NotificationType.Error);
            PlayNotificationSound("cityscape");
            toastNotification.Show();
        }

        private static void PlayNotificationSound(string sound)
        {
            var soundsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");
            var soundFile = Path.Combine(soundsFolder, sound + ".wav");

            using (var player = new System.Media.SoundPlayer(soundFile))
            {
                player.Play();
            }
        }
    }
}
