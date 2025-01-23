using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskMenager.Data.Entities;
using TaskMenager.Services;

namespace TaskMenager
{
    /// <summary>
    /// Interaction logic for TaskDetails.xaml
    /// </summary>
    public partial class TaskDetails : Window
    {
        private readonly ITaskService _taskService;
        private readonly TaskItem _task;
        public TaskDetails(TaskItem taskItem, ITaskService taskService)
        {
            InitializeComponent();
            _task = taskItem;
            _taskService = taskService;
            DataContext = taskItem;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_task.Description != null && !_task.Description.Equals(TaskDescriptionTextBox.Text))
                {
                    _task.Description = TaskDescriptionTextBox.Text;

                    // Pozivamo servis za čuvanje ažuriranog zadatka
                    await _taskService.UpdateTaskAsync(_task);

                    MessageBox.Show("Promene su uspešno sačuvane!", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Zatvaranje prozora nakon čuvanja
                    Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške prilikom čuvanja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
