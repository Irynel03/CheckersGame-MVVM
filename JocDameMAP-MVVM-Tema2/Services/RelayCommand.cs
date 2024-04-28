using System;
using System.Windows.Input;

namespace JocDameMAP_MVVM_Tema2
{
    class RelayCommand<T> : ICommand
    {
        private Action<T> commandTask;
        private Predicate<T> canExecuteTask; 

        public RelayCommand(Action<T> workToDo, Predicate<T> canExecute)   // ceva problema aici ca nu imi ia canExecute ca Cell
        {
            commandTask = workToDo;
            canExecuteTask = canExecute;
        }

        public RelayCommand(Action<T> workToDo)
            : this(workToDo, DefaultCanExecute)
        {
            commandTask = workToDo;
        }

        private static bool DefaultCanExecute(T parameter)
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteTask != null && canExecuteTask((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                //+=asociaza un handler la un eveniment
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                //-=sterge un handler de la un eveniment
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            commandTask((T)parameter);
        }
    }
}
