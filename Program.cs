/*
   This is the code and compiled executable for Windows 11 of 
   Elevator management system implemented in C# and MVC (model, view, controller)
   
   author of code: Alexander Panov
   email: a_panov@mail.ru
   Tel(Bulgaria): +359 877 644011
   Tel(Malta): +356 99912151
   Tel(UK): +44 7741 397240

   Created 3 August 2025.

   You are free to use this project under GPL3 license. License itself you can find on a link below:
   https://www.gnu.org/licenses/gpl-3.0.html
   I am not responsuble for anything with this code, use on your own risk.
   
   Main source for C# programming languge: https://learn.microsoft.com/dotnet/csharp.
*/
using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        ElevatorModel model = new ElevatorModel();
        ElevatorController controller = new ElevatorController(model, null);
        ElevatorView view = new ElevatorView(controller);
        controller.SetView(view);

        Application.Run(view);
    }
}