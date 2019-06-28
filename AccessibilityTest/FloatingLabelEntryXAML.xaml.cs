using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AccessibilityTest
{
    public partial class FloatingLabelEntryXAML : ContentView
    {

        int _placeholderFontSize = 16;
        int _titleFontSize = 12;
        int _topMargin = -13;

        public FloatingLabelEntryXAML()
        {
            InitializeComponent();
            input.Focused += Input_Focused;
            input.Unfocused += Input_Unfocused;
            AutomationProperties.SetLabeledBy(input, label);
        }

        private async void Input_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(input.Text))
            {
                await TransitionToPlaceholder(true);
            }
        }

        private async void Input_Focused(object sender, FocusEventArgs e)
        {
            await TransitionToTitle(true);
        }

        async Task TransitionToTitle(bool animated)
        {
            if (animated)
            {
                var t1 = label.TranslateTo(0, _topMargin, 100);
                var t2 = SizeTo(_titleFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                label.TranslationX = 0;
                label.TranslationY = _topMargin;
                label.FontSize = _titleFontSize;
            }

        }

        async Task TransitionToPlaceholder(bool animated)
        {
            if (animated)
            {
                var t1 = label.TranslateTo(10, 0, 100);
                var t2 = SizeTo(_placeholderFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                label.TranslationX = 10;
                label.TranslationY = 0;
                label.FontSize = _placeholderFontSize;
            }

        }

        Task SizeTo(int fontSize)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            // setup information for animation
            Action<double> callback = input => { label.FontSize = input; };
            double startingHeight = label.FontSize;
            double endingHeight = fontSize;
            uint rate = 5;
            uint length = 100;
            Easing easing = Easing.Linear;

            // now start animation with all the setup information
            label.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }

    }
}
