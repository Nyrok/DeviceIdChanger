using Memory;
using System.Diagnostics;
using static System.Guid;

namespace DeviceIdChanger
{
    public partial class Form1 : Form
    {
        Mem memory = new Mem();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int processId = Process.GetProcessesByName("Minecraft.Windows").FirstOrDefault()?.Id ?? -1;
            if (processId >= 0)
            {
                this.memory.OpenProcess(processId);
                this.label3.Text = this.memory.ReadMemory<String>("Minecraft.Windows.exe+04973070,0,20,0")?.ToString();
                this.label3.ForeColor = System.Drawing.Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.memory.mProc.Process != null) goto Inject;
            int processId = Process.GetProcessesByName("Minecraft.Windows").FirstOrDefault()?.Id ?? -1;
            if (processId < 0)
            {
                MessageBox.Show("Je n'ai pas trouvé le client Minecraft.");
                return;
            }
            this.memory.OpenProcess(processId);
            Inject:
            string uuid4 = Guid.NewGuid().ToString();
            this.memory.WriteMemory("Minecraft.Windows.exe+04973070,0,20,0", "string", uuid4);
            this.label3.Text = this.memory.ReadMemory<String>("Minecraft.Windows.exe+04973070,0,20,0")?.ToString();
            this.label3.ForeColor = System.Drawing.Color.White;
        }

    }
}