using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace weather_s
{
    public partial class Main_form : Form
    {
        public Main_form()
        {
            InitializeComponent();
        }
        List<Cities> city_list = new List<Cities>();
        string forecast_text;
        string data = "";
        bool city_f = false;
        class Cities
        {
            public int id { get; set; }
            public string Name { get; set; }
        }

        public void get_cities()
        {
            HtmlWindow window = webBrowser1.Document.Window;
            string str = window.Document.Body.OuterHtml;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(str);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@id='city_list']/option");

            foreach (var node in nodes)
            {
                string id_node = node.Attributes["value"].Value;
                city_list.Add(new Cities() { id = Convert.ToInt32(id_node), Name = node.NextSibling.InnerText });
            }
        }
        public void save_xml(int id)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("http://xml.meteoservice.ru/export/gismeteo/point/"+id+".xml", "forecast.xml");
        }
        public string forecast_f(string str1, string str2)
        {
            string str = null;

            if (str1 == "day")
            {
                str = "Число: " + str2 + " "; 
            }
            if (str1 == "month") {
                str = "Месяц: " + str2 + " ";
            }
            if (str1 == "year")
            {
                str = "Год: " + str2 + " ";
            }
            if (str1 == "hour")
            {
                str = "Время: " + str2+":00" + " ";
            }
            if (str1 == "tod")
            {
                if (str2 == "0")
                    str2 = "ночь";
                else if (str2 == "1")
                    str2 = "утро";
                else if (str2 == "2")
                    str2 = "день";
                else if (str2 == "3")
                    str2 = "вечер";
                str = "Время суток: " + str2 + "\n";
            }
            return str;
        }

        public string forecast_p(string str1, string str2)
        {
            string str = null;

            if (str1 == "cloudiness")
            {
                if (str2 == "0")
                    str2 = "ясно";
                else if (str2 == "1")
                    str2 = "малооблачно";
                else if (str2 == "2")
                    str2 = "облачно";
                else if (str2 == "3")
                    str2 = "пасмурно";
                str = "Облачность: " + str2 + " ";
            }
            if (str1 == "precipiation")
            {
                if (str2 == "4")
                    str2 = "дождь";
                else if (str2 == "5")
                    str2 = "ливень";
                else if (str2 == "6")
                    str2 = "снег";
                else if (str2 == "7")
                    str2 = "снег";
                else if (str2 == "8")
                    str2 = "гроза";
                else if (str2 == "9")
                    str2 = "нет данных";
                else if (str2 == "10")
                    str2 = "без осадков";

                str = "Тип осадков: " + str2 + " ";
            }
            if (str1 == "rpower")
            {
                if (str2 == "0")
                    str2 = "возможен дождь/снег";
                else if (str2 == "1")
                    str2 = "дождь/снег";
                str = "Интенсивность осадков: " + str2 + " " + " ";
            }
            if (str1 == "spower")
            {
                if (str2 == "0")
                    str2 = "возможна гроза";
                else if (str2 == "1")
                    str2 = "гроза";
                str = "Вероятность грозы: " + str2 + " " + "\n";
            }
            return str;
        }


        public string forecast_press(string str1, string str2)
        {
            string str = null;

            if (str1 == "max")
            {
                str = "Макс: " + str2;
            }
            if (str1 == "min")
            {
                str = "Мин: " + str2 + "\n";
            }
            return str;
            }
        public string forecast_temp(string str1, string str2)
        {
            string str = null;

            if (str1 == "max")
            {
                str = "Макс: " + str2;
            }
            if (str1 == "min")
            {
                str = "Мин: " + str2 + "\n";
            }
            return str;
        }
        public string forecast_w(string str1, string str2)
        {
            string str = null;

            if (str1 == "max")
            {
                str = "Макс: " + str2+"\n";
            }
            if (str1 == "min")
            {
                str = "Мин: " + str2;
            }
            if (str1 == "direction")
            {
                if (str2 == "0")
                    str2 = "северный";
                else if (str2 == "1")
                    str2 = "северо-восточный";
                else if (str2 == "2")
                    str2 = "восточное";
                else if (str2 == "3")
                    str2 = "юго-воточное";
                else if (str2 == "4")
                    str2 = "южное";
                else if (str2 == "5")
                    str2 = "юго-западное";
                else if (str2 == "6")
                    str2 = "западное";
                else if (str2 == "7")
                    str2 = "северо-западное";

                str = "Направление ветра в румбах: " + str2 + "\n";
            }
            return str;
        }

        public string forecast_r(string str1, string str2)
        {
            string str = null;

            if (str1 == "max")
            {
                str = "Макс: " + str2;
            }
            if (str1 == "min")
            {
                str = "Мин: " + str2 + "\n";
            }
            return str;
        }
        public string forecast_h(string str1, string str2)
        {
            string str = null;

            if (str1 == "max")
            {
                str = "Макс: " + str2 + "\n\n";
            }
            if (str1 == "min")
            {
                str = "Мин: " + str2;
            }
            return str;
        }
        public void forecast_v()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("forecast.xml");

            XmlNodeList nodeList = xDoc.GetElementsByTagName("TOWN");
            foreach (XmlElement town in nodeList)
            {
                if (town.Name == "TOWN")
                {
                    forecast_text = "Город: " + data + '\n';
                    //  Console.WriteLine("Компания: {0}", childnode.InnerText);
                }
                foreach (XmlElement forecast in town.ChildNodes)
                {
                    if (forecast.Name == "FORECAST")
                    {
                        foreach (XmlAttribute e in forecast.Attributes)
                        {
                            forecast_text += forecast_f(e.Name.ToString(), e.Value.ToString());
                        }
                    }
                    foreach (XmlElement item in forecast.ChildNodes)
                    {
                        if (item.Name == "PHENOMENA")
                        {
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                forecast_text += forecast_p(e.Name.ToString(),e.Value.ToString());
                            }
                        }

                        if (item.Name == "PRESSURE")
                        {
                            forecast_text += "Атмосферное давление, в мм.рт.ст: ";
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                forecast_text += forecast_press(e.Name.ToString(),e.Value.ToString());
                            }
                        }

                        if (item.Name == "TEMPERATURE")
                        {
                            forecast_text += "Температура воздуха, в градусах Цельсия: ";
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                forecast_text += forecast_temp(e.Name.ToString(),e.Value.ToString());
                            }
                        }

                        if (item.Name == "WIND")
                        {
                            forecast_text += "Приземный ветер: ";
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                forecast_text += forecast_w(e.Name.ToString(), e.Value.ToString());
                            }
                        }
                        if (item.Name == "RELWET")
                        {
                            forecast_text += "относительная влажность воздуха, в %: ";
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                forecast_text += forecast_r(e.Name.ToString(),e.Value.ToString());
                            }
                        }

                        if (item.Name == "HEAT")
                        {
                            forecast_text += "температура воздуха по ощущению";
                            foreach (XmlAttribute e in item.Attributes)
                            {
                                forecast_text += forecast_h(e.Name.ToString(), e.Value.ToString());
                            }
                        }
                    }
                    break;
                }
                break;
            }
           // richTextBox1.Text = forecast_text;
        }
        public void start_serv()
        {
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostByName(textBox1.Text);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, Convert.ToInt32(textBox2.Text));

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    richTextBox1.Text="Ожидаем соединение через порт {0}" + ipEndPoint;

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    if (data.IndexOf("Стоп") > -1)
                    {
                        richTextBox1.Text += ("Сервер завершил соединение с клиентом.");
                        byte[] msg1 = Encoding.UTF8.GetBytes(data + "Сервер завершил работу.");
                        handler.Send(msg1);
                            break;
                    }

                    foreach (var item in city_list)
                    {
                        if (data == item.Name)
                        {
                            save_xml(item.id);
                            city_f = true;
                            break;
                        }
                    }
                    string reply;
                    if (city_f)
                    {
                        forecast_v();
                        // Отправляем ответ клиенту\
                        reply = forecast_text;
                        city_f = false;
                    }
                    else
                    {
                        reply = "Данного города нет в списке";
                    }
                    //// Показываем данные на консоли
                    //richTextBox1.Text += ("Полученный текст: " + data + "\n\n");

                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    File.Delete("forecast.xml");

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                   
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text += (ex.ToString());
            }
        }
        private void Main_form_Load(object sender, System.EventArgs e)
        {
           // get_cities();
        }

        private void button_update_Click(object sender, System.EventArgs e)
        {
            get_cities();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
             get_cities();
             start_serv(); 
        }  
    }
}
