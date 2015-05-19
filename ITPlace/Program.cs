using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ITPlace
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 0;
            // Создадим первый html - файл
            using (StreamWriter htmlFile = new StreamWriter("index"+number+".html", false, System.Text.Encoding.Default))
            {
                htmlFile.WriteLine("<!DOCTYPE html>");
                htmlFile.WriteLine("<html> <body> <p>");
            }
            //Считывание файла с текстом
            int n = -1;
            using (StreamReader sText = new StreamReader("text.txt", System.Text.Encoding.Default))
            {
                string readText = "";
                while ((readText = sText.ReadLine()) != null)
                {
                    n++;
                    Console.WriteLine(readText);
                    //Cчитывание файла - словаря
                    using (StreamReader sWords = new StreamReader("words.txt", System.Text.Encoding.Default))
                    {
                        string readWords = "";
                        while ((readWords = sWords.ReadLine()) != null)
                        {
                            //Проверка на наличие слова из словаря в предложении
                            if (readText.Contains(readWords))
                            {
                                Console.WriteLine("Совпало  " + readWords);
                                //ищется начальный индекс слова, встречающегося в предложении
                                int position = readText.IndexOf(readWords);
                                Console.WriteLine(position);
                                //запись строки в новый файл
                                using (StreamWriter sw = new StreamWriter("index"+number+".html", true, System.Text.Encoding.Default))
                                {
                                    sw.Write(readText.Substring(0, position));
                                    sw.Write("<strong> <i>" + readText.Substring(position,readWords.Length) + "</i> </strong>");
                                    sw.Write(readText.Substring(position +readWords.Length));
                                }
                            }
                        }
                        sWords.Close();
                    }
                    // максимальное количество строк в файле - 90
                    if (n > 90) //Если количество строк в html-файле превышает число 90
                    {
                        //закрываем файл
                        using (StreamWriter sw = new StreamWriter("index"+number+".html", true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("</p> </body> </html>");
                            sw.Close();
                        }
                        //Увеличиваем счетчик файлов
                        number += 1;
                        //обнуляем количество строк для нового файла
                        n = 0;
                        //создаем новый html - файл
                        using (StreamWriter htmlFile = new StreamWriter("index" + number + ".html", false, System.Text.Encoding.Default))
                        {
                            htmlFile.WriteLine("<!DOCTYPE html>");
                            htmlFile.WriteLine("<html> <body> <p>");
                        }
                    }
               }
                //если не превышает, по окончанию цикла просто закрываем файл
                using (StreamWriter sw = new StreamWriter("index" + number + ".html", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("</p> </body> </html>");
                    sw.Close();
                }
                sText.Close();

            }
        }
    }
}
