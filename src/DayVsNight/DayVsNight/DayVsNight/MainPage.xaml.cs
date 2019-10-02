using DayVsNight.Models;
using DayVsNight.Themes;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace DayVsNight
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        public MainPage()
        {
            InitializeComponent();

            Room kitchen = new Room() { Name = "Kitchen" };
            kitchen.Devices.Add(new Models.Device() { Name = "Ceiling light", IsActive = true });
            kitchen.Devices.Add(new Models.Device() { Name = "Oven", IsActive = true });
            kitchen.Devices.Add(new Models.Device() { Name = "Toaster", IsActive = true });
            kitchen.Devices.Add(new Models.Device() { Name = "Dishwasher", IsActive = false });

            Rooms.Add(kitchen);

            Room study = new Room() { Name = "Study" };
            study.Devices.Add(new Models.Device() { Name = "Ceiling light", IsActive = true });
            study.Devices.Add(new Models.Device() { Name = "Bureau light", IsActive = false });

            Rooms.Add(study);

            Room bedRoom = new Room() { Name = "Bedroom" };
            bedRoom.Devices.Add(new Models.Device() { Name = "Ceiling light", IsActive = false });
            bedRoom.Devices.Add(new Models.Device() { Name = "Nightdesk light", IsActive = true });
            bedRoom.Devices.Add(new Models.Device() { Name = "Airco", IsActive = true });

            Rooms.Add(bedRoom);

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // MessagingCenter.Subscribe<LoginMessage>(this, "successful_login", (lm) => HandleLogin(lm));
            MessagingCenter.Subscribe<ThemeMessage>(this, ThemeMessage.ThemeChanged, (tm) => UpdateTheme(tm));
        }

        private void UpdateTheme(ThemeMessage tm)
        {
            BackgroundGradient.InvalidateSurface();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<ThemeMessage>(this, ThemeMessage.ThemeChanged);
        }

        string themeName = "light";

        private void ProfileImage_Tapped(object sender, EventArgs e)
        {
            if (themeName == "light")
            {
                themeName = "dark";
            }
            else
            {
                themeName = "light";
            }

            ThemeHelper.ChangeTheme(themeName);
        }

        // background brush
        SKPaint backgroundBrush = new SKPaint()
        {
            Style = SKPaintStyle.Fill,
            Color = Color.Red.ToSKColor()
        };

        private void BackgroundGradient_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // get the brush based on the theme
            SKColor gradientStart = ((Color)Application.Current.Resources["BackgroundGradientStartColor"]).ToSKColor();
            SKColor gradientMid = ((Color)Application.Current.Resources["BackgroundGradientMidColor"]).ToSKColor();
            SKColor gradientEnd = ((Color)Application.Current.Resources["BackgroundGradientEndColor"]).ToSKColor();

            // gradient backround
            backgroundBrush.Shader = SKShader.CreateRadialGradient
                (new SKPoint(0, info.Height * .8f),
                info.Height*.8f,
                new SKColor[] { gradientStart, gradientMid, gradientEnd },
                new float[] { 0, .5f,  1 },
                SKShaderTileMode.Clamp);

            //backgroundBrush.Shader = SKShader.CreateLinearGradient(
            //                              new SKPoint(0, 0),
            //                              new SKPoint(info.Width, info.Height),
            //                              new SKColor[] {
            //                                  gradientStart, gradientEnd },
            //                              new float[] { 0, 1 },
            //                              SKShaderTileMode.Clamp);
            SKRect backgroundBounds = new SKRect(0, 0, info.Width, info.Height);
            canvas.DrawRect(backgroundBounds, backgroundBrush);


        }
    }
}
