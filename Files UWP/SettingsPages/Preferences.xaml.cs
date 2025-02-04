﻿using Windows.Storage;
using Windows.UI.Xaml.Controls;
using System;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.IO;
using Files.Filesystem;

namespace Files.SettingsPages
{
    
    public sealed partial class Preferences : Page
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public Preferences()
        {
            this.InitializeComponent();

            if (localSettings.Values["customLocationsSetting"] != null)
            {
                if (localSettings.Values["customLocationsSetting"].Equals(true))
                {
                    CustomLocationToggle.IsOn = true;

                    DesktopL.IsEnabled = true;
                    DesktopL.Text = localSettings.Values["DesktopLocation"].ToString();

                    DownloadsL.IsEnabled = true;
                    DownloadsL.Text = localSettings.Values["DownloadsLocation"].ToString();

                    DocumentsL.IsEnabled = true;
                    DocumentsL.Text = localSettings.Values["DocumentsLocation"].ToString();

                    PictureL.IsEnabled = true;
                    PictureL.Text = localSettings.Values["PicturesLocation"].ToString();

                    MusicL.IsEnabled = true;
                    MusicL.Text = localSettings.Values["MusicLocation"].ToString();

                    VideosL.IsEnabled = true;
                    VideosL.Text = localSettings.Values["VideosLocation"].ToString();

                    SaveCustomL.IsEnabled = true;
                }
                else
                {
                    CustomLocationToggle.IsOn = false;
                    DesktopL.IsEnabled = false;
                    DownloadsL.IsEnabled = false;
                    DocumentsL.IsEnabled = false;
                    PictureL.IsEnabled = false;
                    MusicL.IsEnabled = false;
                    VideosL.IsEnabled = false;
                    SaveCustomL.IsEnabled = false;
                }
            }
            else
            {
                CustomLocationToggle.IsOn = false;
                DesktopL.IsEnabled = false;
                DownloadsL.IsEnabled = false;
                DocumentsL.IsEnabled = false;
                PictureL.IsEnabled = false;
                MusicL.IsEnabled = false;
                VideosL.IsEnabled = false;
                SaveCustomL.IsEnabled = false;
            }
            SuccessMark.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void ToggleSwitch_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn)
            {
                localSettings.Values["customLocationsSetting"] = true;

                DesktopL.IsEnabled = true;
                localSettings.Values["DesktopLocation"] = ProHome.DesktopPath;

                DownloadsL.IsEnabled = true;
                localSettings.Values["DownloadsLocation"] = ProHome.DownloadsPath;

                DocumentsL.IsEnabled = true;
                localSettings.Values["DocumentsLocation"] = ProHome.DocumentsPath;

                PictureL.IsEnabled = true;
                localSettings.Values["PicturesLocation"] = ProHome.PicturesPath;

                MusicL.IsEnabled = true;
                localSettings.Values["MusicLocation"] = ProHome.MusicPath;

                VideosL.IsEnabled = true;
                localSettings.Values["VideosLocation"] = ProHome.VideosPath;

                DesktopL.Text = localSettings.Values["DesktopLocation"].ToString();
                DownloadsL.Text = localSettings.Values["DownloadsLocation"].ToString();
                DocumentsL.Text = localSettings.Values["DocumentsLocation"].ToString();
                PictureL.Text = localSettings.Values["PicturesLocation"].ToString();
                MusicL.Text = localSettings.Values["MusicLocation"].ToString();
                VideosL.Text = localSettings.Values["VideosLocation"].ToString();

                SaveCustomL.IsEnabled = true;
            }
            else
            {
                localSettings.Values["customLocationsSetting"] = false;
                DesktopL.IsEnabled = false;
                DownloadsL.IsEnabled = false;
                DocumentsL.IsEnabled = false;
                PictureL.IsEnabled = false;
                MusicL.IsEnabled = false;
                VideosL.IsEnabled = false;
                SaveCustomL.IsEnabled = false;
            }
        }

        private async void SaveCustomL_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            StorageFolder newLocationSetting;
            bool isFlawless = true;

            if (!string.IsNullOrEmpty(DesktopL.Text))
            {
                try
                {
                    newLocationSetting = await StorageFolder.GetFolderFromPathAsync(DesktopL.Text);
                    localSettings.Values["DesktopLocation"] = DesktopL.Text;
                    DesktopL.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                catch (UnauthorizedAccessException)
                {
                    await ItemViewModel<Preferences>.GetCurrentSelectedTabInstance<ProHome>().permissionBox.ShowAsync();
                    return;
                }
                catch (ArgumentException)
                {
                    DesktopL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
                catch (FileNotFoundException)
                {
                    DesktopL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
            }

            if (!string.IsNullOrEmpty(DownloadsL.Text))
            {
                try
                {
                    newLocationSetting = await StorageFolder.GetFolderFromPathAsync(DownloadsL.Text);
                    localSettings.Values["DownloadsLocation"] = DownloadsL.Text;
                    DownloadsL.BorderBrush = new SolidColorBrush(Colors.Black);

                }
                catch (UnauthorizedAccessException)
                {
                    await ItemViewModel<Preferences>.GetCurrentSelectedTabInstance<ProHome>().permissionBox.ShowAsync();
                    return;
                }
                catch (ArgumentException)
                {
                    DownloadsL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
                catch (FileNotFoundException)
                {
                    DownloadsL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
            }

            if (!string.IsNullOrEmpty(DocumentsL.Text))
            {
                try
                {
                    newLocationSetting = await StorageFolder.GetFolderFromPathAsync(DocumentsL.Text);
                    localSettings.Values["DocumentsLocation"] = DocumentsL.Text;
                    DocumentsL.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                catch (UnauthorizedAccessException)
                {
                    await ItemViewModel<Preferences>.GetCurrentSelectedTabInstance<ProHome>().permissionBox.ShowAsync();
                    return;
                }
                catch (ArgumentException)
                {
                    DocumentsL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
                catch (FileNotFoundException)
                {
                    DocumentsL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
            }

            if (!string.IsNullOrEmpty(PictureL.Text))
            {
                try
                {
                    newLocationSetting = await StorageFolder.GetFolderFromPathAsync(PictureL.Text);
                    localSettings.Values["PicturesLocation"] = PictureL.Text;
                    PictureL.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                catch (UnauthorizedAccessException)
                {
                    await ItemViewModel<Preferences>.GetCurrentSelectedTabInstance<ProHome>().permissionBox.ShowAsync();
                    return;
                }
                catch (ArgumentException)
                {
                    PictureL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
                catch (FileNotFoundException)
                {
                    PictureL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
            }

            if (!string.IsNullOrEmpty(MusicL.Text))
            {
                try
                {
                    newLocationSetting = await StorageFolder.GetFolderFromPathAsync(MusicL.Text);
                    localSettings.Values["MusicLocation"] = MusicL.Text;
                    MusicL.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                catch (UnauthorizedAccessException)
                {
                    await ItemViewModel<Preferences>.GetCurrentSelectedTabInstance<ProHome>().permissionBox.ShowAsync();
                    return;
                }
                catch (ArgumentException)
                {
                    MusicL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
                catch (FileNotFoundException)
                {
                    MusicL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
            }

            if (!string.IsNullOrEmpty(VideosL.Text))
            {
                try
                {
                    newLocationSetting = await StorageFolder.GetFolderFromPathAsync(VideosL.Text);
                    localSettings.Values["VideosLocation"] = VideosL.Text;
                    VideosL.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                catch (UnauthorizedAccessException)
                {
                    await ItemViewModel<Preferences>.GetCurrentSelectedTabInstance<ProHome>().permissionBox.ShowAsync();
                    return;
                }
                catch (ArgumentException)
                {
                    VideosL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
                catch (FileNotFoundException)
                {
                    VideosL.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    isFlawless = false;
                }
            }

            if (isFlawless)
            {
                SuccessMark.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
    }
}
