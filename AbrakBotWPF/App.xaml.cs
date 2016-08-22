using AbrakBotWPF.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AbrakBotWPF
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // au démarrage de l'application on lance cette vue par défaut
            MainWindow win = new MainWindow();
            MainViewModel model = ServiceLocator.Current.GetInstance<MainViewModel>();
            model.window = win;
            model.globals = new Model.Services.Globals();
            model.initializeGlobals();
            foreach(string str in model.globals.getTrajetList())
            {
                win.trajetList.Items.Add(str);
            }
            foreach (string str in model.globals.getConfigList())
            {
                win.characterList.Items.Add(str);
            }
            win.Show();
        }
    }
}
