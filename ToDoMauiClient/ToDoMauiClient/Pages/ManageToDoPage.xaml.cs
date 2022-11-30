using System.Diagnostics;
using ToDoMauiClient.DataServices;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.Pages;

[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
    private readonly IRestDataService _dataService;

	bool _isNew;
	private ToDo _toDo;
	public ToDo ToDo
	{
		get => _toDo;
        set 
		{
			_isNew = IsNew(value);
			_toDo = value; 
			OnPropertyChanged();
		}
	}


	public ManageToDoPage(IRestDataService dataService)
	{
		InitializeComponent();

		_dataService = dataService;

        BindingContext = this;
    }


    bool IsNew(ToDo toDo) => toDo.Id == 0;

    async void OnSaveButtonClicked(object sender, EventArgs eventArgs)
	{
		if (_isNew)
		{
			Debug.WriteLine("---> Add new Item");
			await _dataService.AddToDoAsync(ToDo);
		}
		else
        {
            Debug.WriteLine("---> Add new Item");
            await _dataService.UpdateToDoAsync(ToDo);
        }

        await Shell.Current.GoToAsync("..");
    }


    async void OnDeleteButtonClicked(object sender, EventArgs eventArgs)
	{
		await _dataService.DeleteToDoAsync(ToDo.Id);
        await Shell.Current.GoToAsync("..");
    }

	async void OnCancelButtonClicked(object sender, EventArgs eventArgs)
	{
		await Shell.Current.GoToAsync("..");
	}
}