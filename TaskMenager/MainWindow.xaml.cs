using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MaterialDesignThemes.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskMenager.Services;
using TaskMenager.Data.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TaskMenager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ITaskService _taskService;
        private readonly IHost _host;
        private ObservableCollection<TaskItem> ToDoTasks = new ObservableCollection<TaskItem>();
        private ObservableCollection<TaskItem> InProgressTasks = new ObservableCollection<TaskItem>();
        private ObservableCollection<TaskItem> FinishedTasks = new ObservableCollection<TaskItem>();
        public MainWindow(ITaskService taskService, IHost host)
        {
            _host = host;
            _taskService = taskService;
            InitializeComponent();
            LoadTasks();
            //ToggleTheme(true);
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
            //ToggleTheme(true);
        }

        private async void LoadTasks()
        {
            try
            {
                var allTasks = await _taskService.GetAllTasksAsync();
                ToDoTasks.Clear();
                InProgressTasks.Clear();
                FinishedTasks.Clear();
                foreach (var task in allTasks)
                {
                    if(task.Column == 0)
                    ToDoTasks.Add(task);
                    else if(task.Column == 1)
                    InProgressTasks.Add(task);
                    else if (task.Column == 2)
                    FinishedTasks.Add(task);
                }
                ToDoList.ItemsSource = ToDoTasks;
                InProgressList.ItemsSource = InProgressTasks;
                FinishedList.ItemsSource = FinishedTasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom učitavanja zadataka: " + ex.Message);
            }
        }

        private async void TaskCompletedCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                // Ako je checkbox označen, postavlja se trenutni datum kao Finished_At
                

                // Sačuvaj promjene u bazi
                await _taskService.UpdateTaskAsync(task);
                LoadTasks(); // Ponovno učitavanje zadataka da bi se osvježio prikaz
            }
        }



        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var createTaskWindow = _host.Services.GetRequiredService<CreateTask>();
            createTaskWindow.TaskSaved += (s, args) => LoadTasks();
            createTaskWindow.Show();
        }

        

       

        private async void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var task = (sender as Button)?.DataContext as TaskItem;

            if (task != null)
            {
                // Show a confirmation dialog
                var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Delete the task (call the service method)
                        await _taskService.DeleteTaskAsync(task.Id);

                        // Refresh the task list
                        LoadTasks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender is TextBlock textBlock)
            {
                // Pretpostavlja se da je DataContext TaskItem
                var task = textBlock.DataContext as TaskItem;
                if (task != null)
                {
                    // Kreiraj DataObject sa TaskItem podacima
                    DragDrop.DoDragDrop(textBlock, new DataObject(typeof(TaskItem), task), DragDropEffects.Move);
                }
            }

        }


        private async void ToDoList_Drop(object sender, DragEventArgs e)
        {
           if(e.Data.GetDataPresent(typeof(TaskItem)) && e.Data.GetData(typeof(TaskItem)) is TaskItem taskItem)
           {
                if(sender is ListView targetListView)
                {
                    int targetColumn = -1;
                    if (targetListView == ToDoList)
                        targetColumn = 0;
                    else if (targetListView == InProgressList)
                        targetColumn = 1;
                    else if (targetListView == FinishedList)
                        targetColumn = 2;


                    if (targetColumn != -1 && taskItem.Column != targetColumn)
                    {
                        taskItem.Column = targetColumn;

                        try
                        {
                            await _taskService.UpdateTaskAsync(taskItem);
                            LoadTasks();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Greška prilikom premeštanja zadatka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }
                }
           }
        }

        private void ToDoList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is TaskItem selectedTask)
            {
                // Otvaranje novog prozora sa detaljima
                var detailsWindow = new TaskDetails(selectedTask, _taskService);
                detailsWindow.ShowDialog(); // Otvara prozor modalno
            }
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TaskItem)))
            {
                var task = (TaskItem)e.Data.GetData(typeof(TaskItem));

                // Ukloni zadatak iz odgovarajuće kolekcije
                if (ToDoTasks.Contains(task))
                    ToDoTasks.Remove(task);
                else if (InProgressTasks.Contains(task))
                    InProgressTasks.Remove(task);
                else if (FinishedTasks.Contains(task))
                    FinishedTasks.Remove(task);
                _taskService.DeleteTaskAsync(task.Id);

                // Eventualno ažuriraj UI
                MessageBox.Show($"Zadatak '{task.Name}' je obrisan.");
            }
            else
            {
                MessageBox.Show($"Greska prilikom brisanja zadatka!");
            }
        }


            

        private void ToggleTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            if (isDarkTheme)
            {
                theme.SetBaseTheme(BaseTheme.Dark);
            }
            else
            {
                theme.SetBaseTheme(BaseTheme.Light);
            }

            paletteHelper.SetTheme(theme);
        }

        private void DarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            ToggleTheme(true); // Aktiviraj tamnu temu
        }

        private void LightTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleTheme(false); // Aktiviraj svetlu temu
        }


    }
}