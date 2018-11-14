using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                ConnDatabase bd = new ConnDatabase();
                bd.createTablesUsers();
                bd.createTablesGallery();
                bd.createTablesPuzzle();
                bd.createTablesGame();
                bd.createTablesPuzzlePiece();
                bd.createTablesSave();
            } catch(Exception e)
            {

            }
            Application.Run(new Login());
        }
    }
}
