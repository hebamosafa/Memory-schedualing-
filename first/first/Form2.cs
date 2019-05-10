using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace first
{
    public partial class Form2 : Form
    {
        Label cpu_time;
        Form1 f1;
        int i = 0;
      
        int No_of_processes = 0;
        public static Form2 Current;
        public Form2(Form1 frm1)
        {
           
            InitializeComponent();
            Current = this;
            this.f1 = frm1;
        }


        /////////////////////////////
        public List<Process> processes = new List<Process>();
        public List<string> process_draw = new List<string>();

         

        public void runProcess(Process process, int timeslice)
        {
            process.state = 'R';

            //form.updateProcess(process);
            //    if (process.contextSwitches == 0) { process.timeleft = process.cpu_burst; }

            if (process.timeleft >= timeslice)
            {
                process.contextSwitches++;
                process.timeleft -= timeslice;
                process.runtime += timeslice;
            }
            else
            {
                process.runtime += process.timeleft;
                process.timeleft -= process.timeleft;
            }
            if (process.timeleft <= 0)
            {
                process.state = 'T';
                // If a process is finished running, remove it from ready que
                //process.timeEnded = readyQue.simulation_time;
                //process.timeleft = 0;
                //readyQue.processes.RemoveAll(p => p.id == process.id);
                //readyQue.readyProcesses.RemoveAt(0);
                //form.updateProcess(process);
                //readyQue.processes_completed++;
            }
            // Process is currently running, freeze simulation for allocated timeslice
            //  Thread.Sleep(timeslice * 100);
            // Remove 10ms from total_time_running to makeup for dispatcher latency
            //readyQue.total_time_running += (timeslice * 100);
            // Add quantum slice to simulation time
            //readyQue.simulation_time += timeslice;
            if (process.timeleft > 0)
            {
                // If a process still needs more time, send to it to the back of the que
                //  readyQue.readyProcesses.Insert(readyQue.readyProcesses.Count, process);
                //readyQue.readyProcesses.RemoveAt(0);
                // MessageBox.Show("Running process: " + process.id + ", time left: " + process.timeleft);
                //process.state = "Ready";
                //form.updateProcess(process);
            }

        }
        void DRAW_Gantt(List<string> process)
        {
            int i = 1;
            foreach (var tt in process)
            {

                TextBox myprocess2 = new TextBox();
                myprocess2.Text = tt;
                myprocess2.BackColor = Color.CornflowerBlue;
                myprocess2.Top = 200;

                myprocess2.Left = i * myprocess2.Width;

                Form1.ActiveForm.Controls.Add(myprocess2);
                i++;
            }
        }
        int get_min(Process[] myprocess)
        {
            int min = 100;
            int index =0;
            No_of_processes = Convert.ToInt32(f1.textBox1.Text);
            for (int i = 0; i < No_of_processes; i++)
            {
                if ((myprocess[i].state != 'T')&&( myprocess[i].state != 'C'))
                {
                    if (myprocess[i].arrival < min)
                    {
                        min = myprocess[i].arrival;
                        index = i;
                    }
                    else if (myprocess[i].arrival == min) {
                        if (myprocess[i].cpu < myprocess[index].cpu)
                            index = i;
                       

                        

                    }
                }


            }

            return index;
        }

        int min(List<int> arr)
        {
            int min = arr[0];
            int f = 0; int flag = 0;
            for (; f < arr.Count(); f++)
            {
                if (arr[f] < min)
                {
                    min = arr[f];
                    flag = f;
                }

            }

            arr[flag] = 100;
            return flag;
        }
        void GET_DATA()
        {
            No_of_processes = Convert.ToInt32(f1.textBox1.Text);
            bool count = true;
            int j = 0;
            T[] InitializeArray<T>(int length) where T : new()
            {
                T[] array = new T[length];
                for (int o = 0; o < length; ++o)
                {
                    array[o] = new T();
                }

                return array;
            }
            Process[] myprocess = InitializeArray<Process>(No_of_processes);
            List<int> arrival = new List<int>();
            List<int> cpu = new List<int>();
            /////////////////set process arrival& cpu time

            foreach (Control c in Form1.ActiveForm.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    Int16 box_text = Int16.Parse(txt.Text);
                    if (count)
                    {
                        myprocess[j].arrival = box_text;
                        myprocess[j].id = j;
                        arrival.Add(myprocess[j].arrival);
                        count = false;
                    }
                    else
                    {
                        myprocess[j].cpu = box_text;
                        cpu.Add(myprocess[j].cpu);
                        count = true;
                        j++;
                    }

                }
            }

            if (f1.comboBox1.Text == "FCFS")
                FCFS_SCH(myprocess, arrival);
            else if (f1.comboBox1.Text == "SJF non-premeative")
                SJF_SCH_nonpre(myprocess, arrival, cpu);
            else 
                SJF_SCH_pre(myprocess, arrival, cpu);
        }
        void SJF_SCH_pre(Process[] myprocess, List<int> arrival, List<int> cpu)
        {
           
             int[] index = new int[No_of_processes];
             i = 0; int j = 0; 
            int minarr;
            List<int> ready = new List<int>();
            while (i < No_of_processes)
            {
                // ready is empty//////////////////////////////
                if (j == 0)
                {
                    index[j] = get_min(myprocess);
                    minarr = arrival[index[j]];
                    myprocess[index[j]].start = minarr;

                }

                else
                {
                    index[j] = cpu.IndexOf(ready.Min());
                    myprocess[index[j]].start = arrival[cpu.IndexOf(ready.Min())];
                }

                

                /*  process_draw.Add("process" + index[j] + " (" + myprocess[index[j]].start + ',' + myprocess[index[j]].finish + ")");
                  myprocess[index[j]].state = 'T';
                  i++;*/




                myprocess[index[j]].finish = myprocess[index[j]].start + myprocess[index[j]].cpu;
                    myprocess[index[j]].state = 'C';
                        int k = 0;
                        while (myprocess[index[j]].finish > arrival[get_min(myprocess)])
                        {
                        if ((myprocess[get_min(myprocess)].state != 'T') && (myprocess[get_min(myprocess)].state != 'C'))
                        {

                            ready.Add(myprocess[get_min(myprocess)].cpu);
                            myprocess[get_min(myprocess)].state = 'C';
                        }
                            k++;
                            if (k > No_of_processes - 1)
                                break;

                        }
                if (myprocess[index[j]].cpu > ready.Min() && arrival[cpu.IndexOf(ready.Min())]< myprocess[index[j]].finish)
                {
                    myprocess[index[j]].finish = arrival[cpu.IndexOf(ready.Min())];
                    ready.Remove(ready.Min());
                    myprocess[cpu.IndexOf(ready.Min())].state = 'R';
                }

                        process_draw.Add("process" + index[j] + " (" + myprocess[index[j]].start + ',' + myprocess[index[j]].finish + ")");
                        myprocess[index[j]].cpu = myprocess[index[j]].cpu - (myprocess[index[j]].finish - myprocess[index[j]].start);

                    
                    
                   
                   
                    
                j++;
                
            }



            //DRAW_Gantt(process_draw);


        }

        void SJF_SCH_nonpre(Process[] myprocess, List<int> arrival, List<int> cpu)
        {

            /////////////////////////////////////BE ATTENTION
            int[] index = new int[No_of_processes];
            int minarr;
            for (i = 0; i < No_of_processes; i++)
            {

                if (i == 0 || myprocess[index[i - 1]].finish <=arrival[ get_min(myprocess)])
                {
                    ///check two arrived at same time
                    
                    index[i] = get_min(myprocess);
                    minarr = arrival[index[i]];
                    myprocess[index[i]].start = minarr;
                    myprocess[index[i]].cpu = cpu[index[i]];
                    myprocess[index[i]].state = 'T';
                }
                else
                {
                    List<int> ready = new List<int>();
                    int k = 0;

                    while (myprocess[index[i - 1]].finish > arrival[k])
                    {
                        if (myprocess[k].state != 'T')

                            ready.Add(myprocess[arrival.IndexOf(get_min(myprocess))].cpu);
                        k++;
                        if (k > No_of_processes - 1)
                            break;

                    }

                    index[i] = cpu.IndexOf(ready.Min());
                    myprocess[index[i]].start = myprocess[index[i - 1]].finish;

                    ready.Remove(ready.Min());
                    myprocess[index[i]].state = 'T';

                }

                myprocess[index[i]].finish = myprocess[index[i]].start + myprocess[index[i]].cpu;
                process_draw.Add("process" + index[i] + " (" + myprocess[index[i]].start + ',' + myprocess[index[i]].finish + ")");

            }
            DRAW_Gantt(process_draw);
        }
        void Draw_FCFS()
        {
            Label arrival_time = new Label();
            arrival_time.Text = "Arrival Time";
            arrival_time.Top = 25;
            Form2.ActiveForm.Controls.Add(arrival_time);
            cpu_time = new Label();
            cpu_time.Text = "CPU Time";
            cpu_time.Top = 25;
            cpu_time.Left = arrival_time.Location.X + arrival_time.Width;
            Form2.ActiveForm.Controls.Add(cpu_time);

            int m = 1;
            No_of_processes = Convert.ToInt32(f1.textBox1.Text);
            for (int k = 1; k <= No_of_processes; k++)
            {

                TextBox arrival = new TextBox();
                arrival.Name = "Arrival" + k;
                arrival.Top = m * arrival.Height + arrival_time.Height;

                TextBox cpu = new TextBox();
                cpu.Top = m * arrival.Height + arrival_time.Height;
                cpu.Left = arrival.Width;

                m++;
                Form2.ActiveForm.Controls.Add(arrival);
                Form2.ActiveForm.Controls.Add(cpu);

            }
        }

        void FCFS_SCH(Process[] myprocess, List<int> arrival)
        {

            /////////////////////////////////////BE ATTENTION
            int[] index = new int[No_of_processes];

            for (i=0; i < No_of_processes; i++)
            {
                
                index[i] = get_min(myprocess);
                int minarr = arrival[index[i]];
                int cputime = myprocess[index[i]].cpu;
                if (i == 0)
                    myprocess[index[i]].start = 0;
                else if (minarr >= myprocess[index[i - 1]].finish)
                    myprocess[index[i]].start = minarr;
                else
                    myprocess[index[i]].start = myprocess[index[i - 1]].finish;
                myprocess[index[i]].finish = myprocess[index[i]].start + myprocess[index[i]].cpu;
                myprocess[index[i]].state ='T';
                process_draw.Add("process" + index[i] + " (" + myprocess[index[i]].start + ',' + myprocess[index[i]].finish + ")");
                //arrival[index[i]] = 100;
            }
            DRAW_Gantt(process_draw);
        }
        void Form2_Load(object sender, EventArgs e)
        {
            string Var = f1.comboBox1.Text;
            if (Var == "FCFS" || Var == "SJF non-premeative" || Var == "SJF premetive")
            {///////////////////////////////cann't understand
                textBox1.Visible = false;
                label1.Visible = false;
            }


        }


        void button1_Click(object sender, EventArgs e)
        {
            string Var = f1.comboBox1.Text;
            if (Var == "FCFS" || Var == "SJF non-premeative" || Var == "SJF premetive")
                GET_DATA();

        }

        void button2_Click(object sender, EventArgs e)
        {

            string var;

            string Var = f1.comboBox1.Text;
            if (Var == "FCFS" || Var == "SJF non-premeative" || Var == "SJF premetive")
            {

                Draw_FCFS();

            }


            else if (Var == "RoundRobin")
            {


                Label arrival_time = new Label();
                arrival_time.Text = "Arrival Time";
                arrival_time.Top = 25;
                Form2.ActiveForm.Controls.Add(arrival_time);
                cpu_time = new Label();
                cpu_time.Text = "CPU Time";
                cpu_time.Top = 25;
                cpu_time.Left = arrival_time.Location.X + arrival_time.Width;
                Form2.ActiveForm.Controls.Add(cpu_time);

                // Int32 timeslice = Convert.ToInt32(textBox1.Text);

                int m = 1;
                for (int k = 1; k <= No_of_processes; k++)
                {

                    TextBox arrival1 = new TextBox();
                    arrival1.Name = "Arrival" + k;
                    arrival1.Top = m * arrival1.Height + arrival_time.Height;

                    TextBox cpu1 = new TextBox();
                    cpu1.Top = m * arrival1.Height + arrival_time.Height;
                    cpu1.Left = arrival1.Width;

                    m++;
                    Form2.ActiveForm.Controls.Add(arrival1);
                    Form2.ActiveForm.Controls.Add(cpu1);

                }
            }



        }

        void groupBox3_Enter(object sender, EventArgs e)
        {

            /*string var;
              int m = 1;
              var = f1.comboBox1.Text;
               Label arrival_time = new Label();
                  arrival_time.Text = "Arrival Time";
                  arrival_time.Top = 25;
                  Form2.ActiveForm.Controls.Add(arrival_time);
                  cpu_time = new Label();
                  cpu_time.Text = "CPU Time";
                  cpu_time.Top = 25;
                  cpu_time.Left = arrival_time.Location.X + arrival_time.Width;
                  Form2.ActiveForm.Controls.Add(cpu_time);

                  string quantum = textBox1.Text;
                 // Int16 quantum_int = Int16.Parse(quantum);
                  //int quantum_int = Convert.ToInt32(textBox1.Text);
                  int No_of_processes = Convert.ToInt32(f1.textBox1.Text);
                  for (int k = 1; k <= No_of_processes; k++)
                  {

                      TextBox arrival1 = new TextBox();
                      arrival1.Name = "Arrival" + k;
                      arrival1.Top = m * arrival1.Height + arrival_time.Height;

                      TextBox cpu1 = new TextBox();
                      cpu1.Top = m * arrival1.Height + arrival_time.Height;
                      cpu1.Left = arrival1.Width;

                      m++;
                      Form2.ActiveForm.Controls.Add(arrival1);
                      Form2.ActiveForm.Controls.Add(cpu1);

                  }*/
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        void label1_Click(object sender, EventArgs e)
        {

        }

        void button3_Click(object sender, EventArgs e)
        {
            Int32 timeslice = Convert.ToInt32(textBox1.Text);
            int No_of_processes = Convert.ToInt32(f1.textBox1.Text);

            bool count = true;
            List<int> arrival = new List<int>();
            List<int> cpu_burst = new List<int>();

            foreach (Control c in Form2.ActiveForm.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    Int16 arr = Int16.Parse(txt.Text);
                    if (count)
                    {
                        arrival.Add(arr);
                        count = false;
                    }
                    else
                    {
                        cpu_burst.Add(arr);
                        count = true;
                    }
                }
            }

            //process = arrival & cpu 
            for (int k = 0; k < No_of_processes; k++)
            {

                Process process = new Process();
                process.arrival = arrival.Min();
                //process.arrivalTime = arrival[k];
                // process.cpu_burst = cpu_burst[k];
                process.cpu_burst = cpu_burst.ElementAt(arrival.IndexOf(arrival.Min()));
                processes.Add(process);
                cpu_burst.RemoveAt(arrival.IndexOf(arrival.Min()));
                arrival.RemoveAt(arrival.IndexOf(arrival.Min()));

            }


            //running processes
            int completed_process = 0; int counter = 0; bool repeated = false;
            List<int> index_completed_processes = new List<int>();

            //RR schedular code
            List<string> process_draw = new List<string>();
            bool flag = false;

            do
            {
                if (processes[counter].contextSwitches == 0) { processes[counter].timeleft = processes[counter].cpu_burst; }
                if (processes[counter].state == 'T')
                {
                    for (int k = 0; k < index_completed_processes.Count; k++)
                    {
                        if (index_completed_processes[k] == counter)
                        {
                            repeated = true;
                            break;
                        }
                        else repeated = false;
                    }

                    if (repeated == false)
                    {
                        int index_completed = new int();
                        index_completed = counter;
                        index_completed_processes.Add(index_completed);
                        completed_process += 1;
                        if (counter != 0)
                        {
                            processes[counter].start = processes[counter - 1].finish;
                            processes[counter].finish = processes[counter].start + timeslice;
                            process_draw.Add("process" + counter + " (" + processes[counter].start + ',' + processes[counter].finish + ")");
                            DRAW_Gantt(process_draw);
                            //                    processes.RemoveAt(counter);
                        }
                        else
                        {
                            processes[counter].start = processes[No_of_processes - 1].finish;
                            processes[counter].finish = processes[counter].start + timeslice;
                            process_draw.Add("process" + counter + " (" + processes[counter].start + ',' + processes[counter].finish + ")");
                            DRAW_Gantt(process_draw);
                            //                 processes.RemoveAt(counter);

                        }

                    }
                }


                runProcess(processes[counter], timeslice);


                if (counter > 0 ||
                  (processes[counter].contextSwitches < (Math.Ceiling(processes[counter].cpu_burst / (float)timeslice))
                  && timeslice != 0 && (processes[counter].state != 'T')))
                {

                    if (counter != 0)
                    {
                        processes[counter].start = processes[counter - 1].finish;
                        processes[counter].finish = processes[counter].start + timeslice;
                        process_draw.Add("process" + counter + " (" + processes[counter].start + ',' + processes[counter].finish + ")");
                        // arrival[counter] = 100;
                        DRAW_Gantt(process_draw);
                    }
                }
                if ((counter == 0 && flag == true && processes[counter].state != 'T') ||
                (processes[counter].contextSwitches < (Math.Ceiling(processes[counter].cpu_burst / (float)timeslice))
                && timeslice != 0
                && processes[counter].state != 'T'))
                {
                    processes[counter].start = processes[No_of_processes - 1].finish;
                    processes[counter].finish = processes[counter].start + timeslice;
                    process_draw.Add("process" + counter + " (" + processes[counter].start + ',' + processes[counter].finish + ")");
                    DRAW_Gantt(process_draw);
                }


                if ((flag == false && counter == 0))
                {
                    processes[0].start = processes[0].arrival;
                    processes[0].finish = processes[0].arrival + timeslice;
                    process_draw.Add("process" + counter + " (" + processes[counter].start + ',' + processes[counter].finish + ")");
                    flag = true;
                    // arrival[counter] = 100;
                    DRAW_Gantt(process_draw);

                }
                //////////////////////////////////////////////////

                counter += 1;

                if (counter == No_of_processes - 1)
                {

                    if (processes[counter].contextSwitches == 0) { processes[counter].timeleft = processes[counter].cpu_burst; }
                    runProcess(processes[counter], timeslice);
                    processes[counter].start = processes[counter - 1].finish;
                    processes[counter].finish = processes[counter].start + timeslice;
                    process_draw.Add("process" + counter + " (" + processes[counter].start + ',' + processes[counter].finish + ")");
                    //arrival[counter] = 100;
                    DRAW_Gantt(process_draw);
                    counter = 0;
                }


            } while (completed_process != No_of_processes - 1); //end round robbin function


        }//end btn RR





        // back btn
        void button4_Click(object sender, EventArgs e)
        {

            this.Hide();
            // what should i put here to show form1 again
            Form1.Current.ShowDialog();
        }
    }
}

