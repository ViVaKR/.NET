namespace Contacts.Views;

public partial class ContactPage : ContentPage
{
	public ContactPage()
	{
		InitializeComponent();
	}

    private void BtnEditContact_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(EditContactPage));
    }

    private void BtnAddContact_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }
}