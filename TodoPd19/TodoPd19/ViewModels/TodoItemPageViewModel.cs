using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using System;
using TodoPd19.Models;
using TodoPd19.Services;

namespace TodoPd19.ViewModels
{
	public class TodoItemPageViewModel : ViewModelBase, INavigationAware
	{
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;

        private TodoItem _todoItem;
        public TodoItem TodoItem
        {
            get { return _todoItem; }
            set { SetProperty(ref _todoItem, value); }
        }

        public DelegateCommand OnSaveClicked { get; set; }
        public DelegateCommand OnDeleteClicked { get; set; }
        public DelegateCommand OnCancelClicked { get; set; }
        public DelegateCommand OnSpeakClicked { get; set; }

        public TodoItemPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDbService dbService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _dbService = dbService;

            OnSaveClicked = new DelegateCommand(SaveItem);
            OnDeleteClicked = new DelegateCommand(DeleteItem);
            OnCancelClicked = new DelegateCommand(CancelPage);
            OnSpeakClicked = new DelegateCommand(SpeakNote);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("item"))
            {
                TodoItem = (TodoItem)parameters["item"];
                //_dialogService.DisplayAlertAsync("On NavTo", ItemsExpander(TodoItem), "OK");
                TodoItem.ID = TodoItem.ID;
                Name = TodoItem.Name;
                Notes = TodoItem.Notes;
                Edad = TodoItem.edad;
                Puesto = TodoItem.puesto;
                Direccion= TodoItem.direccion;
                _todoItem.Done = TodoItem.Done;
                EditMode = "Edit";
                StrId = _todoItem.Name + " (" + _todoItem.ID.ToString() + ")";  // ???
            }
            else
            {
                EditMode = "Add";
                StrId = "(New..)";
            }
        }

        private string ItemsExpander(TodoItem item)
        {
            string result;
            //result = item.ID + " || " + item.Name + " || " + item.Notes + " || " + item.Done.ToString();
            result = item.ID + " || " + item.Name + " || " + item.apellidos+ " || " + item.edad + " || " + item.puesto + " || " + item.direccion + " || " + item.Notes + " || " + item.Done.ToString();
            return result;
        }

        private void SpeakNote()
        {
            _dialogService.DisplayAlertAsync("SpeakNote", "Not Implemented", "OK");
        }

        private void CancelPage()
        {
            //_dialogService.DisplayAlertAsync("Cancel Page", "Go back async", "OK");
            _navigationService.GoBackAsync();
        }

        private void DeleteItem()
        {
            int id = TodoItem.ID;   // nur für Alert
            _dbService.DeleteItemAsync(TodoItem);
            _dialogService.DisplayAlertAsync("Empleado eliminado", "el empleado con el siguiente ID fue eliminado: " + id.ToString(), "OK");
            _navigationService.GoBackAsync();
        }

        private async void SaveItem()
        {
            // TODO try
            if (EditMode == "Edit")       // (TodoItem.ID > 0)
            {
                TodoItem.Name = Name;
                TodoItem.apellidos = Apellidos;
                TodoItem.puesto = Puesto;
                TodoItem.edad = Edad;
                TodoItem.direccion = Direccion;

                TodoItem.Notes = Notes;
                TodoItem.Done = Done;
                await _dbService.UpdateItemAsync(TodoItem);
                //await _dialogService.DisplayAlertAsync("Update", "entry ID: " + id.ToString(), "OK");
            }
            else if (EditMode == "Add")
            {
                TodoItem item = new TodoItem();
                item.Name = Name;
                item.apellidos = Apellidos;
                item.puesto = Puesto;
                item.edad = Edad;
                item.direccion = Direccion;

                item.Notes = Notes;
                item.Done = Done;

                await _dbService.InsertItemAsync(item);
                //await _dialogService.DisplayAlertAsync("Insert", "New entry..." + item.Name, "OK");
            }

            await _navigationService.GoBackAsync();
        }

        // Properties
        private string _editMode;    // NMackay Crud
        public string EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        private string _strid;
        public string StrId
        {
            get { return _strid; }
            set { SetProperty(ref _strid, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        private bool _done;
        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }


        private string _puesto;
        public string Puesto
        {
            get { return _puesto; }
            set { SetProperty(ref _puesto, value); }
        }

        private string _apellidos;
        public string Apellidos
        {
            get { return _apellidos; }
            set { SetProperty(ref _apellidos, value); }
        }

        private string _edad;
        public string Edad
        {
            get { return _edad; }
            set { SetProperty(ref _edad, value); }
        }

        private string _direccion;
        public string Direccion
        {
            get { return _direccion; }
            set { SetProperty(ref _direccion, value); }
        }
    }
}
