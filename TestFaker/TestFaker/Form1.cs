using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using gma.System.Windows;

namespace TestFaker
{
    public partial class Form1 : Form
    {
        // =========================================================>> FOR ALL VERSIONS <<=========================================================
        public Form1()
        {
            InitializeComponent();
        }

        // VARS
        string TestFile; // Test File Name e.g. I2Psi18.txt!
        string TestyPath;
        private void Form1_Load(object sender, EventArgs e)
        {
            // -------->> PROGRAM MAP <<-------- //
            // --> Get Test Program from Config √
            // --> FIX ALL PATHS √
            // --> FIND TEST FILENAME √
            // --> COPY TEST FILENAME INTO FAKER FOLDER √
            // --> Mark correct answers with . √
            // --> AFTER FINISHED "FAKE" TEST - EDIT IT AND MOVE AS REAL √
            // --> Save my test results with Crypt Test √
            // --> MAKE READY FOR NEXT USE √
            // --------------------------------- //

            // ==================================================================== PLAY WITH FAKER ========================================================== //
            // ------------------ LOAD CONFIG ---------------- //
            TestyPath = File.ReadAllText("TestyProgramConf.txt");
            // ----------------------------------------------- //
            // ---------------- ACTIVITY HOOK ---------------- //
            UserActivityHook actHook;
            actHook = new UserActivityHook();
            actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);
            actHook.Start();
            // ----------------------------------------------- //
            // I'M INVISIBLE! :P
            //InitializeComponent();
            SuspendLayout();
            this.Opacity = 0.0;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            var hwnd = Handle;
            WindowsServices.SetWindowExTransparent(hwnd);
            AllowTransparency = true;
            // ///////////////////////// //
            noTest.Visible = true;
            Test.Visible = false;
            checker.Start();
        }

        private void checker_Tick(object sender, EventArgs e)
        {
            // Get Test File Name
            var default_config_path = Path.Combine(@"K:\Aplikace\" + TestyPath + @"\", "Testy.ini");
            string line;
            using (StreamReader file = new StreamReader(default_config_path))
            {
                while ((line = file.ReadLine()) != null) {
                    if (line.Contains(".txt!")) {
                        TestFile = line;
                    }
                }
            }
            // Copy Test File Name into Fake test
            string sourceFile = @"K:\Aplikace\" + TestyPath + @"\Testy\" + TestFile;
            string TO_FAKE = @".\" + TestyPath + @"\Testy\" + TestFile;
            if (File.Exists(sourceFile)) {
                File.Copy(sourceFile, TO_FAKE, true);
                // Replace Test Filename in Fake Testy.ini
                string faker_path = Application.StartupPath; // .exe file path
                string reaplce_ini = File.ReadAllText(faker_path + @".\"+ TestyPath + @"\Testy.ini", Encoding.GetEncoding("iso-8859-1"));
                reaplce_ini = reaplce_ini.Replace("%TEST%", TestFile); // Replace Test FileName
                reaplce_ini = reaplce_ini.Replace("%PATH%", faker_path + @"\FAKE_TEST\"); // Replace Path (If faker path is changed)
                File.WriteAllText(faker_path + @".\" + TestyPath + @"\Testy.ini", reaplce_ini, Encoding.GetEncoding("iso-8859-1"));
                // Mark correct answers with . 
                var FAKE_TEST_PREPARE_PATH = Path.Combine(faker_path + @".\" + TestyPath + @"\Testy\", TestFile);
                string OUTPUT_PREPARED_TEXT = string.Empty;
                string Actual_Line;
                using (StreamReader file = new StreamReader(FAKE_TEST_PREPARE_PATH, Encoding.GetEncoding("iso-8859-1"))) {
                    while ((Actual_Line = file.ReadLine()) != null) {
                        if (Actual_Line.Contains("%")) {
                            OUTPUT_PREPARED_TEXT += Actual_Line + " 0" + Environment.NewLine;
                        } else {
                            OUTPUT_PREPARED_TEXT += Actual_Line + Environment.NewLine;
                        }
                    }
                }
                File.Delete(faker_path + @".\" + TestyPath + @"\Testy\" + TestFile); // Pre-delete test in Faker. (avoid overwrite issues)
                System.IO.File.WriteAllText(faker_path + @".\" + TestyPath + @"\Testy\" + TestFile, OUTPUT_PREPARED_TEXT, Encoding.GetEncoding("iso-8859-1"));
                noTest.Visible = false;
                Test.Visible = true;
                checker.Stop();
            }
        }

        // KEY HOOK
        public void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a' || e.KeyChar == 'A') { tester.Start(); } // || Caps-Lock protect
        }

        private void tester_Tick(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("Testy");
            var CAN_I = 0;
            if (pname.Length == 0 && CAN_I==0) {
                // Last part
                // Replace marked answers in finished test
                string faker_path = Application.StartupPath;
                string OUTPUT_FINISHED_TEST = string.Empty;
                string Actual_Line;
                using (StreamReader file = new StreamReader(Path.Combine(faker_path + @".\FAKE_TEST\", Environment.UserName + ".txt"), Encoding.GetEncoding("iso-8859-1"))) {
                    while ((Actual_Line = file.ReadLine()) != null) {
                        if (Actual_Line.Contains(" .")) { OUTPUT_FINISHED_TEST += Actual_Line.Replace(" .", "") + Environment.NewLine; }
                        else { OUTPUT_FINISHED_TEST += Actual_Line + Environment.NewLine; }
                    }
                    // Apply edit and change filename (+_final.txt)
                    System.IO.File.WriteAllText(faker_path + @".\FAKE_TEST\" + Environment.UserName + "_final.txt", OUTPUT_FINISHED_TEST, Encoding.GetEncoding("iso-8859-1"));
                    CAN_I++;
                    //try {
                        // Copy Fake Test into network drive as Real
                        File.Copy(faker_path + @".\FAKE_TEST\" + Environment.UserName + "_final.txt", @"K:\Aplikace\"+ TestyPath + @"\Vysl\" + Environment.UserName + ".txt", true);
                    //} catch (IOException iox) {
                        //MessageBox.Show("File.Copy ERROR  (line 153)");
                        // You're Fuckd..
                    //}

                }
            }

            if (CAN_I == 1) {
                string faker_path = Application.StartupPath;
                // ================================= READY FOR NEXT USE ================================= //
                // Rename file to resolve an error for next use (e.g. gruzaz.txt --> gruzaz_151984854.txt)
                var UniqueFileName = $@"{DateTime.Now.Ticks}.txt";
                File.Copy(faker_path + @".\" + TestyPath + @"\Testy\" + TestFile, faker_path + @".\FAKE_TEST\" + TestFile + "_" + UniqueFileName);
                File.Copy(faker_path + @".\FAKE_TEST\" + Environment.UserName + "_final.txt", faker_path + @".\FAKE_TEST\" + Environment.UserName + "_" + UniqueFileName);
                File.Delete(faker_path + @".\" + TestyPath + @"\Testy\" + TestFile);
                File.Delete(faker_path + @".\FAKE_TEST\" + Environment.UserName + ".txt");
                File.Delete(faker_path + @".\FAKE_TEST\" + Environment.UserName + "_final.txt");
                //Set back %TEST% && %PATH$
                string reaplce_ini = File.ReadAllText(faker_path + @".\" + TestyPath + @"\Testy.ini", Encoding.GetEncoding("iso-8859-1"));
                reaplce_ini = reaplce_ini.Replace(TestFile, "%TEST%"); // Replace Test FileName
                reaplce_ini = reaplce_ini.Replace(faker_path + @"\FAKE_TEST\", "%PATH%"); // Replace Path (If faker path is changed)
                File.WriteAllText(faker_path + @".\" + TestyPath + @"\Testy.ini", reaplce_ini, Encoding.GetEncoding("iso-8859-1"));
                // ====================================================================================== //
                Application.Exit();
            }
        }
    }

    public static class WindowsServices
    {
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static void SetWindowExTransparent(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }
    }

}
