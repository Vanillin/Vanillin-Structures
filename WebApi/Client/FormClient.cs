using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace Client
{
    public partial class FormClient : Form
    {
        static readonly HttpClient client = new HttpClient();
        public FormClient()
        {
            InitializeComponent();
        }
        private void ButtonGet_Click(object sender, EventArgs e)
        {
            ReloadListBox();
        }
        private async void ReloadListBox()
        {
            List<CatalogItem> items = await client.GetFromJsonAsync<List<CatalogItem>>("http://localhost:5000/Catalog");
            if (items != null)
            {
                ListBoxNumbers.Items.Clear();
                foreach (var item in items)
                {
                    ListBoxNumbers.Items.Add($"{item.Number} - {item.Name} {item.SecondName}");
                }
            }
        }
        private async void ButtonPost_Click(object sender, EventArgs e)
        {
            //Создание
            if (TextBoxNumber.Text != null && TextBoxName.Text != null && TextBoxSecondName.Text != null &&
               long.TryParse(TextBoxNumber.Text, out long a))
            {
                CatalogItem ci = new CatalogItem();
                ci.Number = long.Parse(TextBoxNumber.Text);
                ci.Name = TextBoxName.Text;
                ci.SecondName = TextBoxSecondName.Text;
                var response = await client.PostAsJsonAsync("http://localhost:5000/Catalog", ci);
                CatalogItem person = await response.Content.ReadFromJsonAsync<CatalogItem>();
                ListBoxNumbers.Items.Add($"Добавлен контакт: {person.Number} - {person.Name} {person.SecondName}");
                if (CheckBoxGet.Checked)
                {
                    ReloadListBox();
                }
            }
            else
            {
                ListBoxNumbers.Items.Add("Для создания контакта необходимо заполнить поля: Номер, Имя, Фамилия");
            }
        }
        private async void ButtonPut_Click(object sender, EventArgs e)
        {
            //Редактирование
            if (TextBoxNumber.Text != null && TextBoxName.Text != null && TextBoxSecondName.Text != null &&
               long.TryParse(TextBoxNumber.Text, out long a))
            {
                CatalogItem ci = new CatalogItem();
                ci.Number = long.Parse(TextBoxNumber.Text);
                ci.Name = TextBoxName.Text;
                ci.SecondName = TextBoxSecondName.Text;
                var response = await client.PutAsJsonAsync("http://localhost:5000/Catalog", ci);
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ListBoxNumbers.Items.Add("Контакт не найден");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    CatalogItem person = await response.Content.ReadFromJsonAsync<CatalogItem>();
                    ListBoxNumbers.Items.Add($"Контакт изменён на: {person.Number} - {person.Name} {person.SecondName}");
                    if (CheckBoxGet.Checked)
                    {
                        ReloadListBox();
                    }
                }
            }
            else
            {
                ListBoxNumbers.Items.Add("Для изменения контакта необходимо заполнить поля: Номер, Имя, Фамилия");
            }
        }
        private async void ButtonDelete_Click(object sender, EventArgs e)
        {
            //Удаление
            if (TextBoxNumber.Text != null && long.TryParse(TextBoxNumber.Text, out long a))
            {
                var response = await client.DeleteAsync("http://localhost:5000/Catalog/" + TextBoxNumber.Text.ToString());
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    ListBoxNumbers.Items.Add("Контакт не найден");
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    ListBoxNumbers.Items.Add($"Контакт успешно удалён");
                    if (CheckBoxGet.Checked)
                    {
                        ReloadListBox();
                    }
                }
            }
            else
            {
                ListBoxNumbers.Items.Add("Для удаления контакта необходимо заполнить поля: Номер");
            }
        }
    }
}
