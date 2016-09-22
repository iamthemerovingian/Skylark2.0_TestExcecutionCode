using Prism.Unity;
using Skylark2_TestExecutionCode.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;

namespace Skylark2_TestExecutionCode
{
    /// <summary>
    /// This is the Initilization Class of the Entire Application.
    /// It uses a container called Unity 
    /// </summary>
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            ///This will show the shell of the application.
            ///This Shell is for the tyoe of application that has one shell and many controls that can be loaded to the shells regions independently.
            return Container.TryResolve<StartTestView>();
        }

        protected override void InitializeShell()
        {
            /// This will show the main window.
            /// The App.xaml.cs has a Startup URI. It should be deleted and the application shold run from the bootstrappper.
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// This is used to navigate from one set of contrls to another for navigation.
        /// Here I have just added the ErrorCodeView as a set of controls and it is names ErrorCodeView.
        /// 
        /// So if I wanted to show the controls then I will request to navigate and "ErrorCodeView" in the navigate function.
        /// 
        /// This will not work for showing another Window. I have to research more into seeing how to show another window.
        ///
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterTypeForNavigation<StartTestView>("StartTestView");
            Container.RegisterTypeForNavigation<ErrorCodeView>("ErrorCodeView");
            Container.RegisterTypeForNavigation<InputTextView>("InputTextView");
        }
    }
}
