using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS
{
    public class Folders
    {
        public static void CreateFolder(string folderName)
        {
            // Specify the path where folders will be created
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);

            try
            {
                // Check if the folder doesn't exist already
                if (!Directory.Exists(folderPath))
                {
                    // Create the folder
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine($"Folder '{folderName}' created successfully!", "Folder Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Console.WriteLine($"Folder '{folderName}' already exists!", "Folder Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions occurred during folder creation
                Console.WriteLine($"Error creating folder '{folderName}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateSubfolderWithDate(string parentFolder)
        {
            // Get current date
            DateTime currentDate = DateTime.Now;
            // Format the date as "Month-Day-Year"
            string dateString = currentDate.ToString("MM-dd-yyyy");

            // Specify the path of the parent folder
            string parentFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, parentFolder);

            try
            {
                // Create the subfolder with the date
                string subfolderPath = Path.Combine(parentFolderPath, dateString);
                Directory.CreateDirectory(subfolderPath);
                MessageBox.Show($"Subfolder '{dateString}' created successfully inside '{parentFolder}'", "Subfolder Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any exceptions occurred during subfolder creation
                MessageBox.Show($"Error creating subfolder inside '{parentFolder}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
