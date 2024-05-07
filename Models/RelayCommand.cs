using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektTOWAM.Models
{
    // Link do zrodla: https://www.codeproject.com/Questions/5354908/How-to-pass-read-command-parameter-in-viewmodel

    /// <summary>
    /// RelayCommand umożliwia tworzenie prostych implementacji interfejsu ICommand do obsługi poleceń w WPF.
    /// </summary>
    /// <typeparam name="TValue">Typ parametru komendy.</typeparam>
    /// 
    // metody wywołane w momencie klikniecia, ta klasa to odpowiednik ButtonClick
    public class RelayCommand<TValue> : ICommand
    {
        #region Pola

        private readonly Action<TValue> _execute;
        private readonly Predicate<TValue>? _canExecute;
        private EventHandler? _internalCanExecuteChanged;

        #endregion

        #region Konstruktory

        /// <summary>
        /// Inicjuje nową instancję klasy RelayCommand.
        /// </summary>
        /// <param name="execute">Akcja do wykonania, gdy polecenie zostanie wywołane.</param>
        public RelayCommand(Action<TValue> execute)
            : this(execute, null) { /* pominięcie */ }

        /// <summary>
        /// Inicjuje nową instancję klasy RelayCommand.
        /// </summary>
        /// <param name="execute">Akcja do wykonania, gdy polecenie zostanie wywołane.</param>
        /// <param name="canExecute">Predykat określający, czy polecenie może być wykonane.</param>
        public RelayCommand(Action<TValue> execute, Predicate<TValue>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion

        #region Implementacja interfejsu ICommand

        /// <summary>
        /// Występuje, gdy zmienia się możliwość wykonania polecenia.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                _internalCanExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                _internalCanExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Ta metoda może być używana do wywołania obsługiwacza CanExecuteChanged.
        /// Spowoduje to ponowne zapytanie WPF o status tego polecenia.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (_canExecute != null)
                OnCanExecuteChanged();
        }

        /// <summary>
        /// Wykonuje akcję polecenia.
        /// </summary>
        /// <param name="parameter">Parametr przekazany do polecenia.</param>
        public void Execute(object? parameter)
            => _execute((parameter is null ? default : (TValue)parameter)!);

        /// <summary>
        /// Określa, czy polecenie może zostać wykonane.
        /// </summary>
        /// <param name="parameter">Parametr przekazany do polecenia.</param>
        /// <returns>True, jeśli polecenie może być wykonane, w przeciwnym razie false.</returns>
        public bool CanExecute(object? parameter)
            => _canExecute == null || (parameter is null ? _canExecute(default!) : _canExecute((TValue)parameter));

        /// <summary>
        /// Wirtualna metoda wywoływana podczas zmiany możliwości wykonania polecenia.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            EventHandler? eCanExecuteChanged = _internalCanExecuteChanged;
            if (eCanExecuteChanged == null)
                return;

            eCanExecuteChanged(this, EventArgs.Empty);
        }

        #endregion
    }
}
