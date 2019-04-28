using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App5.Models;
using App5.ViewModels;

namespace App5.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;
            //BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadWorkersCommand.Execute(null);
        }

        async void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem as Worker;
            await viewModel.ChangeAssignedWorker(selectedOption);
        }
    }
}