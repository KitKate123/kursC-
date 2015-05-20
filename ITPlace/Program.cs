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
            Console.WriteLine("Пропишите путь, где хотите сохранить html - файлы");
            string path = Console.ReadLine();
            ///Console.WriteLine(path);
            // Создадим первый html - файл
            using (StreamWriter htmlFile = new StreamWriter(path+"index"+number+".html", false, System.Text.Encoding.Default))
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
                    //Console.WriteLine(readText);
                    //Cчитывание файла - словаря
                    using (StreamReader sWords = new StreamReader("words.txt", System.Text.Encoding.Default))
                    {
                        string readWords = "";
                        while ((readWords = sWords.ReadLine()) != null)
                        {
                            //Проверка на наличие слова из словаря в предложении
                            if (readText.Contains(readWords))
                            {
                                //Console.WriteLine("Совпало  " + readWords);
                                //ищется начальный индекс слова, встречающегося в предложении
                                int position = readText.IndexOf(readWords);
                                ///Console.WriteLine(position);
                                ///
                                //запись строки в новый файл
                                using (StreamWriter sw = new StreamWriter(path+"index"+number+".html", true, System.Text.Encoding.Default))
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
                        using (StreamWriter sw = new StreamWriter(path+"index"+number+".html", true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("</p> </body> </html>");
                            ///Проверим место на диске и сравним с размером файла
                            FileInfo f = new FileInfo(path + "index" + number + ".html");
                            double sizef = (f.Length / 1024) / 1024;
                            string Vol = "";
                            DriveInfo di = new DriveInfo(path);
                            double Ffree = (di.AvailableFreeSpace / 1024) / 1024;
                            if (sizef > Ffree)
                            {
                                Console.WriteLine("Нет места на диске!");
                                break;
                            }
                            Console.WriteLine(Ffree.ToString("#,##") + " MB");
                            Console.WriteLine(Vol);
                            sw.Close();
                        }
                        //Увеличиваем счетчик файлов
                        number += 1;
                        //обнуляем количество строк для нового файла
                        n = 0;
                        //создаем новый html - файл
                        using (StreamWriter htmlFile = new StreamWriter(path+"index" + number + ".html", false, System.Text.Encoding.Default))
                        {
                            htmlFile.WriteLine("<!DOCTYPE html>");
                            htmlFile.WriteLine("<html> <body> <p>");
                        }
                    }
               }
                //если не превышает, по окончанию цикла просто закрываем файл
                using (StreamWriter sw = new StreamWriter(path+"index" + number + ".html", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("</p> </body> </html>");
                    sw.Close();
                }
                sText.Close();

            }
            Console.WriteLine("Для выхода из программы нажмите Enter");
            Console.ReadKey();
        }
    }
}
