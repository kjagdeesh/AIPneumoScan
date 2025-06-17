using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIPneumoScan.ViewModels
{
    /// <summary>
    /// ViewModel for managing the Settings page in the AI PneumoScan application.
    /// Controls theme selection and persists the selected theme using app preferences.
    /// </summary>
    public class SettingPageViewModel : BaseViewModel
    {
        private const string ThemeIndexKey = "SelectedThemeIndex";

        private int _selectedThemeIndex;

        /// <summary>
        /// Gets or sets the currently selected theme index.
        /// 0 = Light, 1 = Dark, 2 = System.
        /// When set, this also applies the theme and saves it to preferences.
        /// </summary>
        public int SelectedThemeIndex
        {
            get => _selectedThemeIndex;
            set 
            { 
                _selectedThemeIndex = value; 
                OnPropertyChanged(nameof(SelectedThemeIndex));
                ApplyTheme(value);
                Preferences.Set(ThemeIndexKey, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingPageViewModel"/> class.
        /// Loads the stored theme preference and applies the theme accordingly.
        /// </summary>
        /// <param name="navigation">The navigation service used to manage page transitions.</param>
        public SettingPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            SelectedThemeIndex = Preferences.Get(ThemeIndexKey, 2);
            ApplyTheme(SelectedThemeIndex);
        }

        /// <summary>
        /// Applies the selected theme to the application based on the provided index.
        /// </summary>
        /// <param name="index">The index representing the theme: 0 = Light, 1 = Dark, 2 = System Default.</param>
        private void ApplyTheme(int index)
        {
            App.Current.UserAppTheme = index switch
            {
                0 => AppTheme.Light,
                1 => AppTheme.Dark,
                _ => AppTheme.Unspecified
            };
        }
    }
}
