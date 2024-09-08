namespace ComboAndPlaceholderTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenPopUp_Clicked(object sender, EventArgs e)
        {
            GetCardInformation getCardInformation = new();
            getCardInformation.GetCardInfoResult();
        }
    }

}
