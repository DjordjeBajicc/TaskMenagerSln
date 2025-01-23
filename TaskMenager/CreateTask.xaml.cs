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
    /// Interaction logic for CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Window
    {
        private readonly ITaskService _taskService;
        public CreateTask(ITaskService taskService)
        {
            _taskService = taskService;
            InitializeComponent();
        }

        public event EventHandler TaskSaved;

        private async void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TaskNameTextBox.Text) )
            {
                MessageBox.Show("Molimo unesite potrebne podatke!");
                return;
            }
           
            DateTime timeDeadline = new DateTime();
            if (PresetTimePicker.SelectedTime.HasValue)
                timeDeadline = PresetTimePicker.SelectedTime.Value;

            var task = new TaskItem
            {
                Name = TaskNameTextBox.Text,
                Description = TaskDescriptionTextBox.Text,
                Deadline = TaskDeadlinePicker.SelectedDate.HasValue
                ? TaskDeadlinePicker.SelectedDate.Value.Add(timeDeadline.TimeOfDay)  // Dodajemo vreme 12:00:00
                : (DateTime?)null

            };

            try
            {
                await _taskService.AddTaskAsync(task);
                TaskSaved?.Invoke(this, EventArgs.Empty);
                MessageBox.Show("Zadatak je uspjesno sacuvan.");
                TaskNameTextBox.Clear();
                TaskDescriptionTextBox.Clear();
                TaskDeadlinePicker.SelectedDate = null;
                PresetTimePicker.SelectedTime = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
