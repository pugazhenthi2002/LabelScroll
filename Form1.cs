namespace Practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.MouseWheel += Panel1_MouseWheel;
        }

        private void Panel1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if(e.Delta < 0)
            {
                WheelCount+=textBindScrollBar1.scrollJump;
                textBindScrollBar1.SetThumbRectangle(textBindScrollBar1.scrollJump);
            }
            else
            {
                WheelCount -= textBindScrollBar1.scrollJump;
                textBindScrollBar1.SetThumbRectangle(-textBindScrollBar1.scrollJump);
            }
            DisplayContent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBindScrollBar1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateString();
            var S = panel2.CreateGraphics().MeasureString(actualText, label1.Font);
            if(S.Height > panel2.Height)
            {
                textBindScrollBar1.Visible=true;
                actualText = "";
                GenerateString();
                S = panel2.CreateGraphics().MeasureString(actualText, label1.Font);
            }
            textBindScrollBar1.ContentHeight = (int)S.Height;
            DisplayContent();
        }

        private void DisplayContent()
        {
            actualText = "";
            for(int ctr=WheelCount; ctr<strings.Count && ctr>=0; ctr++)
            {
                 actualText += strings[ctr]+'\n';
            }
            label1.Text = actualText;
        }

        private void GenerateString()
        {
            string singleLineText = "";
            foreach(char Iter in richTextBox1.Text)
            {
                if (panel2.CreateGraphics().MeasureString(singleLineText + Iter, label1.Font).Width >= panel2.Width) 
                {
                    if (singleLineText.Length>0 && singleLineText[singleLineText.Length-1]!=' ')
                    {
                        actualText += singleLineText.Substring(0, singleLineText.Length - 1) +"-"+ "\n";
                        strings.Add(singleLineText.Substring(0, singleLineText.Length - 1) + "-");
                        singleLineText = singleLineText[singleLineText.Length - 1] + Iter + "";
                    }
                    else
                    {
                        actualText += singleLineText.Substring(0, singleLineText.Length - 1) + "\n";
                        strings.Add(singleLineText.Substring(0, singleLineText.Length - 1));
                        singleLineText = Iter + "";
                    }
                }
                else
                {
                    singleLineText += Iter;
                }
            }
        }
        private int WheelCount = 0;
        private List<string> strings = new List<string>();
        private string actualText = "";
    }
}
