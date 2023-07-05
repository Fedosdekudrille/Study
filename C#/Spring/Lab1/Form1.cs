using System.Security.Cryptography;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            operationSelector.SelectedIndex = 0;
            Calculator calculator = new();
            calculator.Calculate += Calculate;
        }
        readonly char[] vowels = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я', 'a', 'e', 'u', 'i', 'o' };
        readonly Regex consonantRegex = new(@"\W|\d");
        readonly Regex wordsRegex = new(@"\b");
        private class Calculator
        {
            public delegate void CalcEvent(object sender, EventArgs e);
            public event CalcEvent? Calculate;
        }
        private void Calculate(object sender, EventArgs e)
        {
            substringText.Text = "";
            subSubStringText.Text = "";
            mainSubstring.Visible = false;
            subSubString.Visible = false;
            switch(operationSelector.SelectedIndex)
            {
                case 0:
                    {
                        mainSubstring.Visible = true;
                        subSubString.Visible = true;
                        substringText.Text = "Введите подстроку, которую хотите заменить";
                        subSubStringText.Text = "Введите подстроку, которой хотите заменить предыдущую";
                        if (mainSubstring.Text.Length > 0)
                        {
                            resultString.Text = mainString.Text.Replace(mainSubstring.Text, subSubString.Text);
                        }
                        else
                        {
                            resultString.Text = mainString.Text;
                        }
                        break;
                    }
                case 1:
                    {
                        mainSubstring.Visible = true;
                        substringText.Text = "Введите подстроку, которую хотите удалить";
                        if (mainSubstring.Text.Length > 0)
                        {
                            resultString.Text = mainString.Text.Replace(mainSubstring.Text, "");
                        }
                        else
                        {
                            resultString.Text = mainString.Text;
                        }
                        break;
                    }
                case 2:
                    {
                        mainSubstring.Visible = true;
                        substringText.Text = "Введите интересующий индекс";
                        try
                        {
                            
                            resultString.Text = mainString.Text[int.Parse(mainSubstring.Text)].ToString();
                        }
                        catch
                        {
                            resultString.Text = "";
                            if(mainSubstring.Text != "")
                            {
                                resultString.Text = "Не существует элемента по индексу " + mainSubstring.Text;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        resultString.Text = mainString.Text.Length.ToString();
                        break;
                    }
                case 4:
                    {
                        int vowelsCount = 0;
                        for(int i = 0; i < mainString.Text.Length; i++)
                        {
                            if (vowels.Contains(mainString.Text[i]))
                            {
                                vowelsCount++;
                            }
                        }
                        resultString.Text = vowelsCount.ToString();
                        break;
                    }
                case 5:
                    {
                        int consonantsCount = 0;
                        for (int i = 0; i < mainString.Text.Length; i++)
                        {
                            if (!vowels.Contains(mainString.Text[i]) && !(consonantRegex.Matches(Convert.ToString(mainString.Text[i])).Count > 0))
                            {
                                consonantsCount++;
                            }
                        }
                        resultString.Text = consonantsCount.ToString();
                        break;
                    }
                case 6:
                    {
                        int dotsNum = mainString.Text.Where(ch => ch == '.').Count();
                        if (dotsNum == 0 || mainString.Text[mainString.Text.Length - 1] != '.')
                        {
                            dotsNum++;
                        }
                        resultString.Text = dotsNum.ToString();
                        break;
                    }
                case 7:
                    {
                        resultString.Text = (wordsRegex.Matches(mainString.Text).Count / 2).ToString();
                        break;
                    }
            }
        }
    }
}