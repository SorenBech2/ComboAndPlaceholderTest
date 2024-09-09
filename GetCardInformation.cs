using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Shapes;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Popup;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ComboAndPlaceholderTest
{
    public partial class GetCardInformation : ObservableObject
    {
        private string? selectedExpirationMonth;
        private string? selectedExpirationYear;
        private SfMaskedEntry cardHolderNameEntry;
        private SfMaskedEntry cardNumberEntry;
        private SfMaskedEntry securityCodeEntry;
        private readonly SfPopup getCardInfoPopUp;


        [ObservableProperty] private ObservableCollection<DateInformation> expirationMonths = [];
        [ObservableProperty] private ObservableCollection<DateInformation> expirationYears = [];

        public GetCardInformation()
        {
            // Builds the expiration month list
            for (int i = 1; i <= 12; i++)
            {
                string month;
                if (i < 10)
                    month = $"0{i}";
                else
                    month = i.ToString();

                DateInformation dateInformation = new()
                {
                    Days = string.Empty,
                    Months = month,
                    Years = string.Empty
                };
                ExpirationMonths.Add(dateInformation);
            }

            // Builds the expiration year list
            DateTime year = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                DateInformation dateInformation = new()
                {
                    Days = string.Empty,
                    Months = string.Empty,
                    Years = year.Year.ToString()
                };
                ExpirationYears.Add(dateInformation);
                year = year.AddYears(1);
            }

            double popupHeight = 410;

            getCardInfoPopUp = new()
            {
                ShowHeader = true,
                HeaderHeight = 45,
                ShowFooter = false,
                MinimumHeightRequest = popupHeight,
                WidthRequest = 355,
                StaysOpen = true,
                ShowCloseButton = true,
                PopupStyle = new PopupStyle()
                {
                    CornerRadius = 7,
                    HasShadow = true
                },
                StartY = (int)(DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density / 5)
            };
            getCardInfoPopUp.PopupStyle.SetAppThemeColor(PopupStyle.PopupBackgroundProperty, Colors.WhiteSmoke, Microsoft.Maui.Graphics.Color.FromArgb("#FF6E6E6E"));

            DataTemplate headerTemplate = new(() =>
            {
                Label headerContent = new() 
                {
                    Text = "Heading",
                    FontSize = 19,
                    Padding = new Thickness(15, 0, 0, 5),
                    VerticalTextAlignment = TextAlignment.End,
                };
                return headerContent;
            });
            getCardInfoPopUp.HeaderTemplate = headerTemplate;

            Grid getCardInfoGrid = new()
            {
                RowDefinitions = {
                    new RowDefinition() { Height = new GridLength(20) },
                    new RowDefinition() { Height = new GridLength(30) },
                    new RowDefinition() { Height = new GridLength(5) },
                    new RowDefinition() { Height = new GridLength(40) },
                    new RowDefinition() { Height = new GridLength(30) },
                    new RowDefinition() { Height = new GridLength(5) },
                    new RowDefinition() { Height = new GridLength(40) },
                    new RowDefinition() { Height = new GridLength(30) },
                    new RowDefinition() { Height = new GridLength(5) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition() { Width = new GridLength(20) },
                    new ColumnDefinition() { Width = new GridLength(65) },
                    new ColumnDefinition() { Width = new GridLength(110) },
                    new ColumnDefinition() { Width = new GridLength(20) },
                    new ColumnDefinition() { Width = new GridLength(120) }
                },
                Margin = new Thickness(5, 5, 0, 0)
            };

            SolidColorBrush shadowBrush = new();
            shadowBrush.SetAppThemeColor(SolidColorBrush.ColorProperty, Colors.LightGray, Colors.Gray);
            Border gridBorder = new() 
            {
                HeightRequest = 225, WidthRequest = 325,
                Margin = new Thickness(0, 10, 0, 0),
                Padding = new Thickness(5, 5, 0, 0),
                Shadow = new() 
                { 
                    Brush = shadowBrush,
                    Offset = new Point(5, 5),
                    Opacity = 0.5F
                }
            };
            gridBorder.Content = getCardInfoGrid;

            // Name of card holder
            getCardInfoGrid.AddWithSpan(new Label
            {
                Text = "Card Holder Name",
                VerticalOptions = LayoutOptions.End,
            }, 0, 0, 1, 4);
           
            cardHolderNameEntry = new()
            {
                Background = Colors.Transparent,
                Stroke = Colors.Transparent,
                FontSize = 18,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                HeightRequest = 40, WidthRequest = 280,
                Keyboard = Keyboard.Default,
                Margin = new Thickness(-5, 10, 0, 0),
                Placeholder = "Name",
                HorizontalOptions = LayoutOptions.Start
            };
            cardHolderNameEntry.SetAppThemeColor(SfMaskedEntry.TextColorProperty, Colors.Black, Colors.White);
            cardHolderNameEntry.SetAppThemeColor(SfMaskedEntry.PlaceholderColorProperty, Colors.LightGray, Colors.DarkGray);
            getCardInfoGrid.AddWithSpan(cardHolderNameEntry, 1, 1, 1, 4);
            getCardInfoGrid.AddWithSpan(new Line()
            {
                X2 = 295,
                VerticalOptions = LayoutOptions.End
            }, 2, 0, 1, 5);

            // Card number
            getCardInfoGrid.AddWithSpan(new Label()
            {
                Text = "Card Number",
                VerticalOptions = LayoutOptions.End,
            }, 3, 0, 1, 4);

            cardNumberEntry = new()
            {
                Background = Colors.Transparent,
                ClearButtonVisibility = ClearButtonVisibility.WhileEditing,
                FontSize = 18,
                HeightRequest = 40, WidthRequest = 280,
                HidePromptOnLeave = true,
                HorizontalOptions = LayoutOptions.Start,
                Keyboard = Keyboard.Numeric,
                Margin = new Thickness(-5, 10, 0, 0),
                Mask = "0000 0000 0000 0000",
                MaskType = MaskedEntryMaskType.Simple,
                Placeholder = "XXXX XXXX XXXX XXXX",
                PromptChar = 'X',
                Stroke = Colors.Transparent,
                ShowBorder = false,
                ValueMaskFormat = MaskedEntryMaskFormat.ExcludePromptAndLiterals,
            };
            cardNumberEntry.SetAppThemeColor(SfMaskedEntry.TextColorProperty, Colors.Black, Colors.White);
            cardNumberEntry.SetAppThemeColor(SfMaskedEntry.PlaceholderColorProperty, Colors.LightGray, Colors.DarkGray); 
            getCardInfoGrid.AddWithSpan(cardNumberEntry, 4, 1, 1, 4);
            getCardInfoGrid.AddWithSpan(new Line()
            {
                X2 = 295,
                VerticalOptions = LayoutOptions.End
            }, 5, 0, 1, 5);

            // Valid until
            getCardInfoGrid.AddWithSpan(new Label()
            {
                Text = "Valid Until",
                HeightRequest = 20, WidthRequest = 90,
                HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End,
            }, 6, 0, 1, 4);

            SfComboBox expiryMonthDropDown = new()
            {
                Background = Colors.Transparent,
                FontSize = 18,
                DropDownItemFontSize = 16,
                IsClearButtonVisible = false,
                Stroke = Colors.Transparent,
                DisplayMemberPath = "Months",
                ItemsSource = ExpirationMonths,
                MaxDropDownHeight = 150,
                Margin = new Thickness(5, 5, 0, 0),
                Placeholder = "MM",
                TextMemberPath = "Months",
                HeightRequest = 40, WidthRequest = 70,
                HorizontalTextAlignment = TextAlignment.End
            };
            expiryMonthDropDown.SetAppThemeColor(SfComboBox.TextColorProperty, Colors.Black, Colors.White);
            expiryMonthDropDown.SetAppThemeColor(SfComboBox.PlaceholderColorProperty, Colors.LightGray, Colors.DarkGray);
            getCardInfoGrid.Add(expiryMonthDropDown, 1, 7);

            SfComboBox expiryYearDropDown = new()
            {
                Background = Colors.Transparent,
                DropDownItemFontSize = 16,
                FontSize = 18,
                IsClearButtonVisible = false,
                Stroke = Colors.Transparent,
                DisplayMemberPath = "Years",
                ItemsSource = ExpirationYears,
                MaxDropDownHeight = 150,
                Margin = new Thickness(-5, 5, 0, 0),
                Placeholder = "YYYY",
                TextMemberPath = "Years",
                HeightRequest = 40, WidthRequest = 85, 
                HorizontalOptions = LayoutOptions.Start,
            };
            expiryYearDropDown.SetAppThemeColor(SfComboBox.TextColorProperty, Colors.LightGrey, Colors.DarkGray);
            expiryYearDropDown.SetAppThemeColor(SfComboBox.PlaceholderColorProperty, Colors.LightGrey, Colors.DarkGray);
            getCardInfoGrid.Add(expiryYearDropDown, 2, 7);
            getCardInfoGrid.AddWithSpan(new Line() { X2 = 160 }, 8, 0, 1, 3);

            // Security Code
            getCardInfoGrid.AddWithSpan(new Label()
            {
                Text = "Security Code",
                HeightRequest = 20, WidthRequest = 110,
                HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End,
            }, 6, 3, 1, 2);
            securityCodeEntry = new()
            {
                Background = Colors.Transparent,
                Stroke = Colors.Transparent,
                FontSize = 18,
                HeightRequest = 40, WidthRequest = 55,
                HidePromptOnLeave = true,
                HorizontalOptions = LayoutOptions.Start,
#if ANDROID
                Margin = new Thickness(-10, 5, 0, 0),
#elif IOS
                Margin = new Thickness(-5, 5, 0, 0),
#endif
                Keyboard = Keyboard.Numeric,
                Mask = "000",
                MaskType = MaskedEntryMaskType.Simple,
                Placeholder = "XXX",
                PromptChar = 'X',
                ValueMaskFormat = MaskedEntryMaskFormat.ExcludePromptAndLiterals
            };
            securityCodeEntry.SetAppThemeColor(SfMaskedEntry.TextColorProperty, Colors.Black, Colors.White);
            securityCodeEntry.SetAppThemeColor(SfMaskedEntry.PlaceholderColorProperty, Colors.LightGrey, Colors.DarkGray);
            getCardInfoGrid.Add(securityCodeEntry, 4, 7);
            getCardInfoGrid.AddWithSpan(new Line() { X2 = 70 }, 8, 3, 1, 2);

         
            getCardInfoPopUp.ContentTemplate = new DataTemplate(() =>
            {
                return new VerticalStackLayout() { gridBorder };
            });
        }

        public void GetCardInfoResult()
        {
            try
            {
                getCardInfoPopUp.Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}