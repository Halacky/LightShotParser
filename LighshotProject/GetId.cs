using System;
using System.Collections.Generic;
using System.Text;

namespace LightShotProject
{
    public class GetId
    {
        private static string startId;
        private static int count;

        public static string StartId 
        {
            get { return startId; }

            set
            {
                if (value.Trim().Length == 6)
                {
                    startId = value;
                }
                else
                {
                    throw new ArgumentException("Неккоректный ввод");
                }
            }
        }
        public static int Count {
            get{ return count;}
            set
            {
                if (value > 0)
                {
                    count = value;
                }
                else
                {
                    throw new ArgumentException("Неккоректный ввод");
                }
            }
        }

        public List<string> MakeId()
        {
            var result = new List<string>();
            string input = startId;
            string output = default;
            for (int i = 0; i < count; i++)
            {
                foreach (char simvol in input) // цикл
                {
                    int ch = (int)simvol;
                    if (ch >= 122)
                    {
                        ch = 97 + (ch - 122);
                    }
                    if (ch >= 57 && char.IsDigit(simvol))
                    {
                        ch = 48 + (ch - 57);
                    }
                    char next = Convert.ToChar(ch + 1); // берем код символа и прибавляем к нему +1, и конвертируем в символ
                    output += next; // записываем следующий символ в результат
                }
                input = output;
                result.Add(output.ToLower());
                output = "";
            }
            return result;
        }
    }
}
