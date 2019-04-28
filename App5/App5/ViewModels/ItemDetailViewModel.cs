using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using App5.Models;
using Xamarin.Forms;

namespace App5.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public ObservableCollection<Worker> WorkersList { get;set;}
        public Command LoadWorkersCommand { get; set; }
        public ItemDetailViewModel(Item item = null, ObservableCollection<Worker> workerList = null)
        {
            Title = item?.Text;
            Item = item;
            LoadWorkersCommand = new Command(async () => await ExecuteLoadWorkersCommand());

            WorkersList = new ObservableCollection<Worker> { 
                new Worker()
                {
                    Id = "ab1",
                    Name = "TestName",
                    Email = "aaa@aa.aa"
                }
            };
        }
        async Task ExecuteLoadWorkersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                WorkersList.Clear();
                var workers = await WorkerStore.GetItemsAsync(true);
                foreach (var worker in workers)
                {
                    WorkersList.Add(worker);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task ChangeAssignedWorker(Worker worker)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await WorkerStore.SetWorkerAsync(worker, Item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
