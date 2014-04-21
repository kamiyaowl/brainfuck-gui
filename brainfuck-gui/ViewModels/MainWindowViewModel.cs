using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using Brainfuck;
using brainfuck_gui.Models;

namespace brainfuck_gui.ViewModels {
	public class MainWindowViewModel : ViewModel {


		#region FuckCode変更通知プロパティ
		private string _FuckCode;

		public string FuckCode {
			get { return _FuckCode; }
			set { 
				if (_FuckCode == value)
					return;
				_FuckCode = value;
				RaisePropertyChanged("FuckCode");
				ExecuteCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion


		#region RunnableCode変更通知プロパティ
		private string _RunnableCode;

		public string RunnableCode {
			get { return _RunnableCode; }
			set { 
				if (_RunnableCode == value)
					return;
				_RunnableCode = value;
				RaisePropertyChanged("RunnableCode");
			}
		}
		#endregion


		#region Result変更通知プロパティ
		private string _Result;

		public string Result {
			get { return _Result; }
			set { 
				if (_Result == value)
					return;
				_Result = value;
				RaisePropertyChanged("Result");
			}
		}
		#endregion


		#region ExecuteCommand
		private ViewModelCommand _ExecuteCommand;

		public ViewModelCommand ExecuteCommand {
			get {
				if (_ExecuteCommand == null) {
					_ExecuteCommand = new ViewModelCommand(Execute, CanExecute);
				}
				return _ExecuteCommand;
			}
		}

		public bool CanExecute() {
			return !string.IsNullOrWhiteSpace(FuckCode);
		}

		public void Execute() {
			string code, result;
			FuckCode.Brainfuck(out code, out result);
			RunnableCode = code;
			Result = result;
		}
		#endregion

		public void Initialize() {
			FuckCode = @">+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.[-]>++++++++[<++++>-]<.>+++++++++++[<+++++>-]<.>++++++++[<+++>-]<.+++.------.--------.[-]>++++++++[<++++>-]<+.[-]++++++++++.";
		}
	}
}
